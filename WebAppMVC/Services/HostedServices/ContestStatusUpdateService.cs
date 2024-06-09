using BAL.ViewModels.Manager;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Contest;
using WebAppMVC.Models.Manager;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Services.Interfaces;

namespace WebAppMVC.Services.HostedServices
{
    public class ContestStatusUpdateService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<ContestStatusUpdateService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private string ContestSearchAPI_URL = "/webapi/api/Contest/Search?status=OnHold&status=OpenRegistration";
        private string ContestStatusUpdateAPI_URL = "/webapi/api/Contest/";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private int contestStatusUpdated = 0;
        private int contestStatusUpdatedToOpen = 0;
        private int contestStatusUpdatedToClosed = 0;
        private int contestStatusUpdatedToCancelled = 0;
        private DateTime today = DateTime.UtcNow;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly BirdClubLibrary methcall = new();

        public ContestStatusUpdateService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<ContestStatusUpdateService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Contest Status update on Date Service is starting.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromHours(1)); // Adjust the interval as needed

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

                var listContestStatus = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: Constants.Constants.POST_METHOD,
                                    url: ContestSearchAPI_URL,
                                    _logger: _logger,
                                    inputType: Constants.Constants.MANAGER,
                                    accessToken: accToken);
                if (listContestStatus == null || !listContestStatus.Status)
                {
                    _logger.LogError("Failed to retrieving list of contests");
                    return;
                }
                _logger.LogInformation("Succeed Retrieved list of {Count} contests via API.", listContestStatus.Data.Count);
                foreach (var contestToUpdate in listContestStatus.Data)
                {
                    if (
                        contestToUpdate.OpenRegistration <= today && 
                        contestToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_ON_HOLD))
                    {
                        UpdateContestStatusVM contesttoUpdateVM = new()
                        {
                            ContestId = contestToUpdate.ContestId,
                            NumberOfParticipants = contestToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION
                        };
                        // Call the API to update the membership status
                        var contestStatusResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: ContestStatusUpdateAPI_URL + contestToUpdate.ContestId + "/Status/Update",
                                        inputType: contesttoUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (contestStatusResponse == null || !contestStatusResponse.Status || contestStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                        else
                        {
                            contestStatusUpdated += 1;
                            contestStatusUpdatedToOpen += 1;
                            _logger.LogInformation("Succeed updating Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                    }
                    if (
                        (
                        contestToUpdate.RegistrationDeadline <= today || 
                        contestToUpdate.NumberOfParticipants >= contestToUpdate.NumberOfParticipantsLimit
                        ) && 
                        contestToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION)
                        )
                    {
                        UpdateContestStatusVM contesttoUpdateVM = new()
                        {
                            ContestId = contestToUpdate.ContestId,
                            NumberOfParticipants = contestToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION
                        };
                        // Call the API to update the membership status
                        var contestStatusResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: ContestStatusUpdateAPI_URL + contestToUpdate.ContestId + "/Status/Update",
                                        inputType: contesttoUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (contestStatusResponse == null || !contestStatusResponse.Status || contestStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                        else
                        {
                            contestStatusUpdated += 1;
                            contestStatusUpdatedToClosed += 1;
                            _logger.LogInformation("Succeed updating Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                    }
                    if (
                        contestToUpdate.RegistrationDeadline <= today &&
                        contestToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) &&
                        contestToUpdate.NumberOfParticipants < Constants.Constants.EVENT_CONTEST_MIN_PART_REQ
                        )
                    {
                        UpdateContestStatusVM contesttoUpdateVM = new()
                        {
                            ContestId = contestToUpdate.ContestId,
                            NumberOfParticipants = contestToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_CANCELLED
                        };
                        // Call the API to update the membership status
                        var contestStatusResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: ContestStatusUpdateAPI_URL + contestToUpdate.ContestId + "/Status/Update",
                                        inputType: contesttoUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (contestStatusResponse == null || !contestStatusResponse.Status || contestStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                        else
                        {
                            contestStatusUpdated += 1;
                            contestStatusUpdatedToCancelled += 1;
                            _logger.LogInformation("Succeed updating Contest's status with ID: {ContestId} via API.", contestToUpdate.ContestId);
                        }
                    }
                }
                _logger.LogInformation("Contest Status update on Date Service has updated {contestStatusUpdated} contests status with:\n" +
                    "{contestStatusUpdatedToOpen} contests status updated to \'OpenRegistration\'\n" +
                    "{contestStatusUpdatedToClosed} contests status updated to \'ClosedRegistration\'\n" +
                    "{contestStatusUpdatedToCancelled} contests status updated to \'Cancelled\'\n", 
                    contestStatusUpdated, 
                    contestStatusUpdatedToOpen, 
                    contestStatusUpdatedToClosed,
                    contestStatusUpdatedToCancelled);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Contest Status update on Date Service is stopping.");

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
