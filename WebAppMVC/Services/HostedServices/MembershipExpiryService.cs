
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using Microsoft.Extensions.Options;
using System;
using WebAppMVC.Models.Member;
using BAL.ViewModels.Authenticates;
using WebAppMVC.Models.Manager;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Services.HostedServices
{
    public class MembershipExpiryService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MembershipExpiryService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private readonly string MembershipAPI_URL = "/api/Member/All/Role/Member";
        private readonly string MembershipUpdateAPI_URL = "/api/Member/Update/Status";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly BirdClubLibrary methcall = new();
        public MembershipExpiryService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<MembershipExpiryService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Membership Expiry Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12)); // Adjust the interval as needed

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _systemLoginService = scope.ServiceProvider.GetRequiredService<ISystemLoginService>();
                var today = DateTime.UtcNow;
                int accStatusUpdateExpired = 0;

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.BaseAddress = new Uri(_config.GetSection("DefaultApiUrl:ConnectionString").Value);

                string? accToken = await _systemLoginService.GetTokenAsync();

                var listMembership = await methcall.CallMethodReturnObject<GetListMemberResponse>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: Constants.Constants.GET_METHOD,
                                    url: MembershipAPI_URL,
                                    _logger: _logger,
                                    accessToken: accToken);
                if (listMembership == null || !listMembership.Status)
                {
                    _logger.LogError("Failed to retrieving list of members");
                    return;
                }
                _logger.LogInformation("Succeed Retrieved list of {Count} members via API.", listMembership.Data.Count);
                foreach (var membership in listMembership.Data)
                {
                    if (membership.ExpiryDate <= today && membership.Status.Equals(Constants.Constants.MEMBER_STATUS_ACTIVE))
                    {
                        membership.Status = Constants.Constants.MEMBER_STATUS_EXPIRED;
                        membership.ExpiryDate = null;
                        // Call the API to update the membership status
                        var memberStatusResponse = await methcall.CallMethodReturnObject<GetMemberStatusExpireResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: MembershipUpdateAPI_URL,
                                        inputType: membership,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (memberStatusResponse == null || !memberStatusResponse.Status || memberStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Member's membership status with ID: {MemberId} via API.", membership.MemberId);
                        }
                        else
                        {
                            accStatusUpdateExpired += 1;
                            _logger.LogInformation("Succeed updating Member's membership status with ID: {MemberId} via API.", membership.MemberId);
                        }
                    }
                }
                _logger.LogInformation("Membership Expiry Service has updated {accStatusUpdateExpired} memberships to 'Expired' status.", accStatusUpdateExpired);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Membership Expiry Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }
        public void Dispose()
        {
            _timer?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
