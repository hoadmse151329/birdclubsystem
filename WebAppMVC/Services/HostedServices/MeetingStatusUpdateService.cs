﻿
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Manager;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Services.HostedServices
{
    public class MeetingStatusUpdateService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<MeetingStatusUpdateService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private string MeetingSearchAPI_URL = "/api/Meeting/Search?status=OnHold&status=OpenRegistration";
        private string MembershipUpdateAPI_URL = "/api/Meeting/";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private int meetingStatusUpdated = 0;
        private int meetingStatusUpdatedToOpen = 0;
        private int meetingStatusUpdatedToClosed = 0;
        private DateTime today = DateTime.UtcNow;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly BirdClubLibrary methcall = new();

        public MeetingStatusUpdateService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<MeetingStatusUpdateService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Meeting Status update on Date Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(12)); // Adjust the interval as needed

            return Task.CompletedTask;
        }
        private async void DoWork(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var _systemLoginService = scope.ServiceProvider.GetRequiredService<ISystemLoginService>();

                var client = _httpClientFactory.CreateClient();

                client.DefaultRequestHeaders.Accept.Add(contentType);
                client.BaseAddress = new Uri(_config.GetSection("DefaultApiUrl:ConnectionString").Value);

                string? accToken = await _systemLoginService.GetTokenAsync();

                if (accToken == null)
                {
                    _logger.LogError("Failed to login system account. Skipping Task...");
                    return;
                }

                var listMeetingStatus = await methcall.CallMethodReturnObject<GetListMeetingStatus>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: Constants.Constants.POST_METHOD,
                                    url: MeetingSearchAPI_URL,
                                    _logger: _logger,
                                    inputType: Constants.Constants.MANAGER,
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
                        var meetingStatusResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: MembershipUpdateAPI_URL + meetingToUpdate.MeetingId + "/Update",
                                        inputType: meetingToUpdate,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (meetingStatusResponse == null || !meetingStatusResponse.Status || meetingStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Meeting's status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                        else
                        {
                            meetingStatusUpdated += 1;
                            meetingStatusUpdatedToOpen += 1;
                            _logger.LogInformation("Succeed updating Meeting's status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                    }
                    if (meetingToUpdate.RegistrationDeadline <= today && meetingToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION))
                    {
                        meetingToUpdate.Status = Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION;
                        // Call the API to update the membership status
                        var meetingStatusResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: MembershipUpdateAPI_URL + meetingToUpdate.MeetingId + "/Update",
                                        inputType: meetingToUpdate,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (meetingStatusResponse == null || !meetingStatusResponse.Status || meetingStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Meeting's status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                        else
                        {
                            meetingStatusUpdated += 1;
                            meetingStatusUpdatedToClosed += 1;
                            _logger.LogInformation("Succeed updating Meeting's status with ID: {MeetingId} via API.", meetingToUpdate.MeetingId);
                        }
                    }
                }
                _logger.LogInformation("Meeting Status update on Date Service has updated {meetingStatusUpdated} meetings status with {meetingStatusUpdatedToOpen} meetings status updated to 'OpenRegistration' and {meetingStatusUpdatedToClosed} meetings status updated to 'ClosedRegistration'.", meetingStatusUpdated,meetingStatusUpdatedToOpen,meetingStatusUpdatedToClosed);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Meeting Status update on Date Service is stopping.");

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