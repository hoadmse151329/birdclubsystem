using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Manager;
using WebAppMVC.Services.Interfaces;
using WebAppMVC.Models.FieldTrip;
using BAL.ViewModels.Manager;

namespace WebAppMVC.Services.HostedServices
{
    public class FieldTripStatusUpdateService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<FieldTripStatusUpdateService> _logger;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _config;
        private Timer _timer;
        private string FieldTripSearchAPI_URL = "/api/FieldTrip/Search?status=OnHold&status=OpenRegistration";
        private string FieldTripUpdateAPI_URL = "/api/FieldTrip/";
        private readonly MediaTypeWithQualityHeaderValue contentType = new("application/json");
        private int fieldtripStatusUpdated = 0;
        private int fieldtripStatusUpdatedToOpen = 0;
        private int fieldtripStatusUpdatedToClosed = 0;
        private int fieldtripStatusUpdatedToCancelled = 0;
        private DateTime today = DateTime.UtcNow;
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly BirdClubLibrary methcall = new();

        public FieldTripStatusUpdateService(IConfiguration configuration, IServiceScopeFactory scopeFactory, ILogger<FieldTripStatusUpdateService> logger, IHttpClientFactory httpClientFactory)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _httpClientFactory = httpClientFactory;
            _config = configuration;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fieldtrip Status update on Date Service is starting.");

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

                var listFieldtripStatus = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                                    _httpClient: client,
                                    options: jsonOptions,
                                    methodName: Constants.Constants.POST_METHOD,
                                    url: FieldTripSearchAPI_URL,
                                    _logger: _logger,
                                    inputType: Constants.Constants.MANAGER,
                                    accessToken: accToken);
                if (listFieldtripStatus == null || !listFieldtripStatus.Status)
                {
                    _logger.LogError("Failed to retrieving list of fieldtrips");
                    return;
                }
                _logger.LogInformation("Succeed Retrieved list of {Count} fieldtrips via API.", listFieldtripStatus.Data.Count);
                foreach (var fieldtripToUpdate in listFieldtripStatus.Data)
                {
                    if (
                        fieldtripToUpdate.OpenRegistration <= today && 
                        fieldtripToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_ON_HOLD)
                        )
                    {
                        UpdateFieldtripStatusVM fieldtripToUpdateVM = new()
                        {
                            TripId = fieldtripToUpdate.TripId,
                            NumberOfParticipants = fieldtripToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION
                        };
                        // Call the API to update the membership status
                        var fieldtripStatusResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: FieldTripUpdateAPI_URL + fieldtripToUpdate.TripId + "/Status/Update",
                                        inputType: fieldtripToUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (fieldtripStatusResponse == null || !fieldtripStatusResponse.Status || fieldtripStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                        else
                        {
                            fieldtripStatusUpdated += 1;
                            fieldtripStatusUpdatedToOpen += 1;
                            _logger.LogInformation("Succeed updating Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                    }
                    if (
                        (
                        fieldtripToUpdate.RegistrationDeadline <= today || 
                        fieldtripToUpdate.NumberOfParticipants >= fieldtripToUpdate.NumberOfParticipantsLimit
                        ) && 
                        fieldtripToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_OPEN_REGISTRATION)
                        )
                    {
                        UpdateFieldtripStatusVM fieldtripToUpdateVM = new()
                        {
                            TripId = fieldtripToUpdate.TripId,
                            NumberOfParticipants = fieldtripToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION
                        };
                        // Call the API to update the membership status
                        var fieldtripStatusResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: FieldTripUpdateAPI_URL + fieldtripToUpdate.TripId + "/Status/Update",
                                        inputType: fieldtripToUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (fieldtripStatusResponse == null || !fieldtripStatusResponse.Status || fieldtripStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                        else
                        {
                            fieldtripStatusUpdated += 1;
                            fieldtripStatusUpdatedToClosed += 1;
                            _logger.LogInformation("Succeed updating Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                    }
                    if (
                        fieldtripToUpdate.RegistrationDeadline <= today &&
                        fieldtripToUpdate.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) &&
                        fieldtripToUpdate.NumberOfParticipants < fieldtripToUpdate.NumberOfParticipantsMinReq
                        )
                    {
                        UpdateFieldtripStatusVM fieldtripToUpdateVM = new()
                        {
                            TripId = fieldtripToUpdate.TripId,
                            NumberOfParticipants = fieldtripToUpdate.NumberOfParticipants,
                            Status = Constants.Constants.EVENT_STATUS_CANCELLED
                        };
                        // Call the API to update the membership status
                        var fieldtripStatusResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                        _httpClient: client,
                                        options: jsonOptions,
                                        methodName: Constants.Constants.PUT_METHOD,
                                        url: FieldTripUpdateAPI_URL + fieldtripToUpdate.TripId + "/Status/Update",
                                        inputType: fieldtripToUpdateVM,
                                        _logger: _logger,
                                        accessToken: accToken);
                        if (fieldtripStatusResponse == null || !fieldtripStatusResponse.Status || fieldtripStatusResponse.Data == null)
                        {
                            _logger.LogError("Failed to update Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                        else
                        {
                            fieldtripStatusUpdated += 1;
                            fieldtripStatusUpdatedToCancelled += 1;
                            _logger.LogInformation("Succeed updating Fieldtrip's status with ID: {TripId} via API.", fieldtripToUpdate.TripId);
                        }
                    }
                }
                _logger.LogInformation("Fieldtrip Status update on Date Service has updated {fieldtripStatusUpdated} fieldtrips status with:\n " +
                    "{fieldtripStatusUpdatedToOpen} fieldtrips status updated to \'OpenRegistration\'\n" +
                    "{fieldtripStatusUpdatedToClosed} fieldtrips status updated to \'ClosedRegistration\'\n" +
                    "{fieldtripStatusUpdatedToCancelled} fieldtrips status updated to \'Cancelled\'", 
                    fieldtripStatusUpdated, 
                    fieldtripStatusUpdatedToOpen, 
                    fieldtripStatusUpdatedToClosed,
                    fieldtripStatusUpdatedToCancelled);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Fieldtrip Status update on Date Service is stopping.");

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
