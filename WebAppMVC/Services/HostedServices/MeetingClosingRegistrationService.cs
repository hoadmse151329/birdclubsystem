
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Manager;
using WebAppMVC.Models.Member;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Services.HostedServices
{
    public class MeetingClosingRegistrationService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MeetingClosingRegistrationService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private string MembershipAPI_URL = "/api/Meeting/All/Status/";
        private string MembershipUpdateAPI_URL = "/api/Meeting/Update/Status";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly BirdClubLibrary methcall = new();

        public MeetingClosingRegistrationService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<MeetingClosingRegistrationService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Meeting Closing Registration on Date Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12)); // Adjust the interval as needed

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _systemLoginService = scope.ServiceProvider.GetRequiredService<ISystemLoginService>();
                var today = DateTime.UtcNow;
                int meetingStatusUpdated = 0;

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.BaseAddress = new Uri(_config.GetSection("DefaultApiUrl:ConnectionString").Value);

                string? accToken = await _systemLoginService.GetTokenAsync();

                var listMeetingStatus = await methcall.CallMethodReturnObject<GetListMeetingStatus>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: Constants.Constants.GET_METHOD,
                                    url: MembershipAPI_URL,
                                    _logger: _logger,
                                    accessToken: accToken);
                if (listMeetingStatus == null || !listMeetingStatus.Status)
                {
                    _logger.LogError("Failed to retrieving list of meetings");
                    return;
                }
                _logger.LogInformation("Succeed Retrieved list of {Count} meetings via API.", listMeetingStatus.Data.Count);
                foreach (var meetingToUpdate in listMeetingStatus.Data)
                {
                    if (meetingToUpdate.OpenRegistration <= today && meetingToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_ON_HOLD))
                    {
                        meetingToUpdate.Status = Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION;
                        // Call the API to update the membership status
                        var memberStatusResponse = await methcall.CallMethodReturnObject<GetMemberStatusExpireResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: MembershipUpdateAPI_URL,
                                        inputType: meetingToUpdate,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (memberStatusResponse == null || !memberStatusResponse.Status || memberStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Member's membership status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                        else
                        {
                            meetingStatusUpdated += 1;
                            _logger.LogInformation("Succeed updating Member's membership status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                    }
                }
                _logger.LogInformation("Membership Expiry Service has updated {meetingStatusUpdated} memberships to 'Expired' status.", meetingStatusUpdated);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Meeting Closing Registration on Date Service is stopping.");

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
