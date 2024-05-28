using BAL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Contest;
using System.Text.Encodings.Web;
using static Org.BouncyCastle.Math.EC.ECCurve;
using WebAppMVC.Models.Member;
using BAL.ViewModels.Member;
using WebAppMVC.Models.Manager;
using BAL.ViewModels.Manager;
using System.ComponentModel.DataAnnotations;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs.Specialized;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc;
using WebAppMVC.Models.ViewModels;
using System.Data;
using WebAppMVC.Models.Feedback;
using WebAppMVC.Models.News;
using BAL.ViewModels.Admin;
using BAL.ViewModels.News;
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    [Route("Manager")]
    public class ManagerController : Controller
    {
        private readonly ILogger<ManagerController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string ManagerAPI_URL = "";
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        private BirdClubLibrary methcall = new();

        public ManagerController(ILogger<ManagerController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            ManagerAPI_URL = "/api/";
        }

        // GET: ManagerController
        [HttpGet("Index")]
        public async Task<IActionResult> ManagerIndex()
        {
            if(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            ManagerAPI_URL += "Manager/Index";

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var dashboardResponse = await methcall.CallMethodReturnObject<GetManagerDashboardResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            return View(dashboardResponse.Data);
        }
        [HttpGet("Meeting")]
        public async Task<IActionResult> ManagerMeeting([FromQuery] string search)
        {
            _logger.LogInformation(search);

            string LocationAPI_URL_All = ManagerAPI_URL + "Location/AllAddresses";

            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                ManagerAPI_URL += "Meeting/Search?meetingName=" + search;
            }
            else ManagerAPI_URL += "Meeting/All";

            dynamic testmodel = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ManagerAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listMeetResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listMeetResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("ManagerIndex");
            }

            testmodel.CreateMeeting = methcall.GetValidationTempData<MeetingViewModel>(this, TempData, Constants.Constants.CREATE_MEETING_VALID, "createMeeting", jsonOptions);
            testmodel.Locations = listLocationResponse.Data;
            testmodel.Meetings = listMeetResponse.Data;
            return View(testmodel);
        }
        [HttpGet("Meeting/{id:int}")]
        /*[Route("Manager/Meeting/{id:int}")]*/
        public async Task<IActionResult> ManagerMeetingDetail([FromRoute][Required] int id)
        {
            string ManagerMeetingDetailAPI_URL = ManagerAPI_URL + "Meeting/AllParticipants/" + id;

            ManagerAPI_URL += "Meeting/" + id;

            dynamic meetingDetailBigModel = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var meetpartPostResponse = await methcall.CallMethodReturnObject<GetListMeetingParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerMeetingDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            meetingDetailBigModel.UpdateMeeting = methcall.GetValidationTempData<MeetingViewModel>(this, TempData, Constants.Constants.UPDATE_MEETING_VALID, "updateMeeting", jsonOptions);
            meetingDetailBigModel.SelectListStatus = methcall.GetManagerEventStatusSelectableList(meetPostResponse.Data.Status);

            meetingDetailBigModel.CreateMeetingMedia = methcall.GetValidationTempData<MeetingMediaViewModel>(this, TempData, Constants.Constants.CREATE_MEETING_MEDIA_VALID, "createMedia", jsonOptions);

            meetingDetailBigModel.MeetingDetails = meetPostResponse.Data;
            meetingDetailBigModel.MeetingParticipants = meetpartPostResponse.Data;

            return View(meetingDetailBigModel);
        }
        [HttpPost("Meeting/{id:int}/Update")]
        public async Task<IActionResult> ManagerUpdateMeetingDetail(
            [FromRoute][Required] int id,
            [Required] MeetingViewModel updateMeeting
            )
        {
            ManagerAPI_URL += "Meeting/" + id + "/Update";

            if (updateMeeting.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateMeeting.NumberOfParticipantsLimit - updateMeeting.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateMeeting.Status", "Error while processing your request (Updating Meeting). Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateMeeting,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);

                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }
            if (!meetPostResponse.Status)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);

                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }
            TempData["Success"] = meetPostResponse.SuccessMessage;
            return RedirectToAction("ManagerMeetingDetail", new { id });
        }
        [HttpPost("Meeting/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateMeeting([Required] MeetingViewModel createMeeting)
        {
            ManagerAPI_URL += "Meeting/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_MEETING_VALID, createMeeting, jsonOptions);
                return RedirectToAction("ManagerMeeting");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createMeeting,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, jsonOptions);
                TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, jsonOptions);
                TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            TempData["Success"] = meetPostResponse.SuccessMessage;
            return RedirectToAction("ManagerMeeting");
        }

        [HttpPost("Meeting/{meetingId:int}/Create/Media")]
        public async Task<IActionResult> ManagerCreateMeetingMedia(
            [Required][FromRoute] int meetingId,
            [Required] MeetingMediaViewModel createMedia)
        {
            ManagerAPI_URL += "Meeting/" + meetingId + "/Create/Media";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_MEETING_MEDIA_VALID, createMedia, jsonOptions);
                return RedirectToAction("ManagerMeetingDetail", new { id = meetingId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            IFormFile photo = createMedia.ImageUpload;

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"meeting/{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;

                createMedia.Image = image;
            }

            createMedia.ImageUpload = null;

            var meetMediaResponse = await methcall.CallMethodReturnObject<GetMeetingMediaResponse>(
                    _httpClient: _httpClient,
                    options: jsonOptions,
                    methodName: Constants.Constants.POST_METHOD,
                    url: ManagerAPI_URL,
                    inputType: createMedia,
                    accessToken: accToken,
                    _logger: _logger);
            if (meetMediaResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Media!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeetingDetail", new { id = meetingId });
            }
            if (!meetMediaResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetMediaResponse.Status + " , Error Message: " + meetMediaResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Media!).\n"
                    + meetMediaResponse.ErrorMessage;
                return RedirectToAction("ManagerMeetingDetail", new { id = meetingId });
            }
            TempData["Success"] = meetMediaResponse.SuccessMessage;
            return RedirectToAction("ManagerMeetingDetail", new {id = meetingId});
        }

        [HttpPost("Meeting/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelMeeting(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "Meeting/" + id + "/Cancel";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Media!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Media!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            TempData["Success"] = meetPostResponse.SuccessMessage; 
            return RedirectToAction("ManagerMeeting");
        }
        [HttpGet("FieldTrip")]
        public async Task<IActionResult> ManagerFieldtrip([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = ManagerAPI_URL + "Location/AllAddresses";

            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                ManagerAPI_URL += "FieldTrip/Search?tripName=" + search;
            }
            else ManagerAPI_URL += "FieldTrip/All";

            dynamic fieldtripIndexVM = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);


            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ManagerAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listFieldTripResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List FieldTrip!). List was Empty!: " + listLocationResponse + ",\n" + listFieldTripResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List FieldTrip!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listFieldTripResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List FieldTrip!).\n"
                    + listFieldTripResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("ManagerIndex");
            }

            fieldtripIndexVM.CreateFieldTrip = methcall.GetValidationTempData<FieldTripViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_VALID, "createFieldTrip", jsonOptions);
            fieldtripIndexVM.FieldTrips = listFieldTripResponse.Data;
            fieldtripIndexVM.Locations = listLocationResponse.Data;
            return View(fieldtripIndexVM);
        }
        [HttpGet("FieldTrip/{id:int}")]
        /*[Route("Manager/FieldTrip/{id:int}")]*/
        public async Task<IActionResult> ManagerFieldTripDetail(int id)
        {
            string ManagerFieldTripDetailAPI_URL = ManagerAPI_URL + "FieldTrip/AllParticipants/" + id;
            ManagerAPI_URL += "FieldTrip/" + id;
            dynamic fieldtripDetailVM = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var fieldtrippartPostResponse = await methcall.CallMethodReturnObject<GetListFieldTripParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerFieldTripDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            fieldtripDetailVM.UpdateFieldTrip = methcall.GetValidationTempData<FieldTripViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, "updateTrip", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripGettingThere = methcall.GetValidationTempData<FieldtripGettingThereViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_GETTHERE_VALID, "updateGettingThere", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripDayByDayErrors = methcall.GetValidationModelStateErrorMessageList<FieldtripDaybyDayViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_DAYBYDAY_VALID, "updateDayByDay", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripDayByDays = methcall.GetValidationTempDataList<FieldtripDaybyDayViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_DAYBYDAY_VALID, "updateDayByDay", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripInclusionErrors = methcall.GetValidationModelStateErrorMessageList<FieldtripInclusionViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_INCLUSION_VALID, "updateInclusion", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripInclusions = methcall.GetValidationTempDataList<FieldtripInclusionViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_INCLUSION_VALID, "updateInclusion", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripTourFeatureErrors = methcall.GetValidationModelStateErrorMessageList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_TOURFEATURES_VALID, "updateTourFeature", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripTourFeatures = methcall.GetValidationTempDataList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_TOURFEATURES_VALID, "updateTourFeature", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripImportantErrors = methcall.GetValidationModelStateErrorMessageList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID, "updateImportant", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripImportants = methcall.GetValidationTempDataList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID, "updateImportant", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripActAndTrasErrors = methcall.GetValidationModelStateErrorMessageList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID, "updateActAndTras", jsonOptions);
            fieldtripDetailVM.UpdateFieldTripActAndTrass = methcall.GetValidationTempDataList<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID, "updateActAndTras", jsonOptions);


            fieldtripDetailVM.CreateFieldTripDayByDay = methcall.GetValidationTempData<FieldtripDaybyDayViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_DAYBYDAY_VALID, "createDayByDay", jsonOptions);
            fieldtripDetailVM.CreateFieldTripInclusion = methcall.GetValidationTempData<FieldtripInclusionViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_INCLUSION_VALID, "createInclusion", jsonOptions);
            fieldtripDetailVM.CreateFieldTripTourFeatures = methcall.GetValidationTempData<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_TOURFEATURES_VALID, "createTourFeatures", jsonOptions);
            fieldtripDetailVM.CreateFieldTripImportant = methcall.GetValidationTempData<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_IMPORTANTTOKNOW_VALID, "createImportant", jsonOptions);
            fieldtripDetailVM.CreateFieldTripActAndTras = methcall.GetValidationTempData<FieldTripAdditionalDetailViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID, "createActAndTras", jsonOptions);
            fieldtripDetailVM.CreateFieldTripMedia = methcall.GetValidationTempData<FieldtripMediaViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_MEDIA_VALID, "createMedia", jsonOptions);

            fieldtripDetailVM.SelectListStatus = methcall.GetManagerEventStatusSelectableList(fieldtripPostResponse.Data.Status);
            fieldtripDetailVM.SelectListInclusionTypes = methcall.GetManagerFieldTripInclusionTypeSelectableList(Constants.Constants.FIELDTRIP_INCLUSION_TYPE_INCLUDED);

            fieldtripDetailVM.FieldTripDetails = fieldtripPostResponse.Data;
            fieldtripDetailVM.FieldTripTourFeatures = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("tour_features")).ToList();
            fieldtripDetailVM.FieldTripImportantToKnows = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("important_to_know")).ToList();
            fieldtripDetailVM.FieldTripActivitiesAndTransportation = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("activities_and_transportation")).ToList();
            fieldtripDetailVM.FieldTripParticipants = fieldtrippartPostResponse.Data;

            return View(fieldtripDetailVM);
        }
        [HttpPost("FieldTrip/{id:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripDetail(
            [FromRoute][Required] int id,
            [Required] FieldTripViewModel updateTrip
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/Update";
            if (updateTrip.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateTrip.NumberOfParticipantsLimit - updateTrip.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateTrip.Status", "Error while processing your request (Updating FieldTrip). Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, updateTrip, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, updateTrip, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateTrip,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = fieldtripPostResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/GettingThere/{getId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripGettingThereDetail(
            [FromRoute][Required] int id,
            [FromRoute][Required] int getId,
            [Required] FieldtripGettingThereViewModel updateGettingThere
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/GettingThere/" + getId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_GETTHERE_VALID, updateGettingThere, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftGettingThereResponse = await methcall.CallMethodReturnObject<GetFieldTripGettingThereResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateGettingThere,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftGettingThereResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftGettingThereResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftGettingThereResponse.Status + " , Error Message: " + ftGettingThereResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftGettingThereResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftGettingThereResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/DayByDay/{dayId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripDayByDay(
            [FromRoute][Required] int id,
            [FromRoute][Required] int dayId,
            [Required] FieldtripDaybyDayViewModel updateDayByDay
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/DayByDay/" + dayId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempDataWithId(TempData, Constants.Constants.UPDATE_FIELDTRIP_DAYBYDAY_VALID, dayId, updateDayByDay, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftDayByDayResponse = await methcall.CallMethodReturnObject<GetFieldTripDayByDayResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateDayByDay,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftDayByDayResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftDayByDayResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftDayByDayResponse.Status + " , Error Message: " + ftDayByDayResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftDayByDayResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftDayByDayResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/Inclusion/{incId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripInclusion(
            [FromRoute][Required] int id,
            [FromRoute][Required] int incId,
            [Required] FieldtripInclusionViewModel updateInclusion
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/Inclusion/" + incId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempDataWithId(TempData, Constants.Constants.UPDATE_FIELDTRIP_INCLUSION_VALID, incId, updateInclusion, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftInclusionResponse = await methcall.CallMethodReturnObject<GetFieldTripInclusionResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateInclusion,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftInclusionResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftInclusionResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftInclusionResponse.Status + " , Error Message: " + ftInclusionResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftInclusionResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftInclusionResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/TourFeature/{addDeId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripTourFeature(
            [FromRoute][Required] int id,
            [FromRoute][Required] int addDeId,
            [Required] FieldTripAdditionalDetailViewModel updateTourFeature
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/AdditionalDetail/" + addDeId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempDataWithId(TempData, Constants.Constants.UPDATE_FIELDTRIP_TOURFEATURES_VALID, addDeId, updateTourFeature, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftTourFeaturesResponse = await methcall.CallMethodReturnObject<GetFieldTripAdditionalDetailResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateTourFeature,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftTourFeaturesResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftTourFeaturesResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftTourFeaturesResponse.Status + " , Error Message: " + ftTourFeaturesResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftTourFeaturesResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftTourFeaturesResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/Important/{addDeId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripImportant(
            [FromRoute][Required] int id,
            [FromRoute][Required] int addDeId,
            [Required] FieldTripAdditionalDetailViewModel updateImportant
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/AdditionalDetail/" + addDeId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempDataWithId(TempData, Constants.Constants.UPDATE_FIELDTRIP_IMPORTANTTOKNOW_VALID, addDeId, updateImportant, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftImportantResponse = await methcall.CallMethodReturnObject<GetFieldTripAdditionalDetailResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateImportant,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftImportantResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftImportantResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftImportantResponse.Status + " , Error Message: " + ftImportantResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftImportantResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftImportantResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/ActAndTras/{addDeId:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripActAndTras(
            [FromRoute][Required] int id,
            [FromRoute][Required] int addDeId,
            [Required] FieldTripAdditionalDetailViewModel updateActAndTras
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/AdditionalDetail/" + addDeId + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempDataWithId(TempData, Constants.Constants.UPDATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID, addDeId, updateActAndTras, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftActAndTrasResponse = await methcall.CallMethodReturnObject<GetFieldTripAdditionalDetailResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateActAndTras,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftActAndTrasResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            if (!ftActAndTrasResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftActAndTrasResponse.Status + " , Error Message: " + ftActAndTrasResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + ftActAndTrasResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            TempData["Success"] = ftActAndTrasResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTrip(FieldTripViewModel createFieldTrip)
        {
            ManagerAPI_URL += "FieldTrip/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_VALID, createFieldTrip, jsonOptions);
                return RedirectToAction("ManagerFieldtrip");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createFieldTrip,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            TempData["Success"] = fieldtripPostResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTrip");
        }

        [HttpPost("FieldTrip/{tripId:int}/Create/DayByDay")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTripDayByDay(
            [FromRoute][Required] int tripId,
            [Required] FieldtripDaybyDayViewModel createDayByDay
            )
        {
            ManagerAPI_URL += "FieldTrip/" + tripId + "/Create/DayByDay";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_DAYBYDAY_VALID, createDayByDay, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftDayByDayResponse = await methcall.CallMethodReturnObject<GetFieldTripDayByDayResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createDayByDay,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftDayByDayResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            if (!ftDayByDayResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftDayByDayResponse.Status + " , Error Message: " + ftDayByDayResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + ftDayByDayResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            TempData["Success"] = ftDayByDayResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }

        [HttpPost("FieldTrip/{tripId:int}/Create/Inclusion")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTripInclusion(
            [FromRoute][Required] int tripId,
            [Required] FieldtripInclusionViewModel createInclusion
            )
        {
            ManagerAPI_URL += "FieldTrip/" + tripId + "/Create/Inclusion";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_INCLUSION_VALID, createInclusion, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftInclusionResponse = await methcall.CallMethodReturnObject<GetFieldTripInclusionResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createInclusion,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftInclusionResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            if (!ftInclusionResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftInclusionResponse.Status + " , Error Message: " + ftInclusionResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + ftInclusionResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            TempData["Success"] = ftInclusionResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }

        [HttpPost("FieldTrip/{tripId:int}/Create/TourFeatures")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTripTourFeatures(
            [FromRoute][Required] int tripId,
            [Required] FieldTripAdditionalDetailViewModel createTourFeatures
            )
        {
            ManagerAPI_URL += "FieldTrip/" + tripId + "/Create/AdditionalDetail";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_TOURFEATURES_VALID, createTourFeatures, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftDayByDayResponse = await methcall.CallMethodReturnObject<GetFieldTripAdditionalDetailResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createTourFeatures,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftDayByDayResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            if (!ftDayByDayResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftDayByDayResponse.Status + " , Error Message: " + ftDayByDayResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + ftDayByDayResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            TempData["Success"] = ftDayByDayResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }

        [HttpPost("FieldTrip/{tripId:int}/Create/Important")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTripImportant(
            [FromRoute][Required] int tripId,
            [Required] FieldTripAdditionalDetailViewModel createImportant
            )
        {
            ManagerAPI_URL += "FieldTrip/" + tripId + "/Create/AdditionalDetail";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_INCLUSION_VALID, createImportant, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var ftImportantResponse = await methcall.CallMethodReturnObject<GetFieldTripDayByDayResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createImportant,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftImportantResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            if (!ftImportantResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftImportantResponse.Status + " , Error Message: " + ftImportantResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + ftImportantResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            TempData["Success"] = ftImportantResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }

        [HttpPost("FieldTrip/{tripId:int}/Create/ActAndTran")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTripActAndTran(
            [FromRoute][Required] int tripId,
            [Required] FieldTripAdditionalDetailViewModel createActAndTras
            )
        {
            ManagerAPI_URL += "FieldTrip/" + tripId + "/Create/AdditionalDetail";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_ACTIVITIESANDTRANSPORTATION_VALID, createActAndTras, jsonOptions);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var ftActAndTrasResponse = await methcall.CallMethodReturnObject<GetFieldTripDayByDayResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createActAndTras,
                                accessToken: accToken,
                                _logger: _logger);
            if (ftActAndTrasResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            if (!ftActAndTrasResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + ftActAndTrasResponse.Status + " , Error Message: " + ftActAndTrasResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + ftActAndTrasResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }
            TempData["Success"] = ftActAndTrasResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }

        [HttpPost("FieldTrip/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelFieldTrip(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/Cancel";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            TempData["Success"] = fieldtripPostResponse.SuccessMessage;
            return RedirectToAction("ManagerFieldTrip");
        }
        [HttpGet("Contest")]
        public async Task<IActionResult> ManagerContest([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = ManagerAPI_URL + "Location/AllAddresses";

            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                ManagerAPI_URL += "Contest/Search?contestName=" + search;
            }
            else ManagerAPI_URL += "Contest/All";

            dynamic testmodel3 = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ManagerAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listContestResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listContestResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listContestResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            testmodel3.CreateContest = methcall.GetValidationTempData<ContestViewModel>(this, TempData, Constants.Constants.CREATE_CONTEST_VALID, "createContest", jsonOptions);
            testmodel3.Contests = listContestResponse.Data;
            testmodel3.Locations = listLocationResponse.Data;
            return View(testmodel3);
        }
        [HttpGet("Contest/{id:int}")]
        /*[Route("Manager/Contest/{id:int}")]*/
        public async Task<IActionResult> ManagerContestDetail(
            [FromRoute][Required] int id
            )
        {
            string ManagerContestDetailAPI_URL = ManagerAPI_URL + "Contest/AllParticipants/" + id;
            ManagerAPI_URL += "Contest/" + id;
            dynamic contestDetailBigModel = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var contestpartPostResponse = await methcall.CallMethodReturnObject<GetListContestParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerContestDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return RedirectToAction("ManagerContest");
            }
            if (!contestPostResponse.Status || contestpartPostResponse.Data == null)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("ManagerContest");
            }
            contestPostResponse.Data.ContestParticipants = contestpartPostResponse.Data;
            contestDetailBigModel.UpdateContest = methcall.GetValidationTempData<ContestViewModel>(this, TempData, Constants.Constants.UPDATE_CONTEST_VALID, "updateContest", jsonOptions);
            contestDetailBigModel.CreateContestMedia = methcall.GetValidationTempData<ContestMediaViewModel>(this, TempData, Constants.Constants.CREATE_CONTEST_MEDIA_VALID, "createMedia", jsonOptions);

            contestDetailBigModel.SelectListStatus = methcall.GetManagerEventStatusSelectableList(contestPostResponse.Data.Status);
            contestDetailBigModel.ContestDetails = contestPostResponse.Data;
            contestDetailBigModel.ContestParticipants = contestpartPostResponse.Data;

            return View(contestDetailBigModel);
        }
        [HttpPost("Contest/{id:int}/Update")]
        public async Task<IActionResult> ManagerUpdateContestDetail(
            [FromRoute][Required] int id,
            [Required] ContestViewModel updateContest
            )
        {
            string ManagerContestDetailAPI_URL = ManagerAPI_URL + "Contest/AllParticipants/" + id;
            ManagerAPI_URL += "Contest/Update/" + id;
            if(updateContest.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateContest.NumberOfParticipantsLimit - updateContest.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateContest.Status", "Error while processing your request (Updating Contest). Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateContest, jsonOptions);
                TempData["Error"] = "Not enough people to close registration";
                return RedirectToAction("ManagerContestDetail", new { id });
            }
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateContest, jsonOptions);
                return RedirectToAction("ManagerContestDetail", "Manager", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateContest,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating Contest!).\n Contest Not Found!";
                return RedirectToAction("ManagerContestDetail", "Manager", new { id });
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("ManagerContestDetail", "Manager", new { id });
            }
            if (contestPostResponse.Data.Status.Equals(Constants.Constants.EVENT_STATUS_ENDED))
            {
                var contestpartPostResponse = await methcall.CallMethodReturnObject<GetListContestParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerContestDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
                if (contestpartPostResponse == null)
                {
                    ViewBag.Error =
                        "Error while processing your request! (Updating Contest!).\n Contest Not Found!";
                    return RedirectToAction("ManagerContestDetail", "Manager", new { id });
                }
                if (!contestpartPostResponse.Status)
                {
                    _logger.LogInformation("Error while processing your request: " + contestpartPostResponse.Status + " , Error Message: " + contestpartPostResponse.ErrorMessage);
                    ViewBag.Error =
                        "Error while processing your request! (Updating Contest Post!).\n"
                        + contestpartPostResponse.ErrorMessage;
                    return RedirectToAction("ManagerContestDetail", "Manager", new { id });
                }
                var contestToUpdate = contestPostResponse.Data;
                contestToUpdate.ContestParticipants = contestpartPostResponse.Data;

                string ManagerContestEndedAPI_URL = "/api/Manager/Contest/" + id + "/Participant/All/Score/Update";

                var contestLastUpdateResponse = await methcall.CallMethodReturnObject<GetContestEndedUpdateResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerContestEndedAPI_URL,
                                inputType: contestToUpdate.ContestParticipants,
                                accessToken: accToken,
                                _logger: _logger);
                if (contestLastUpdateResponse == null)
                {
                    _logger.LogInformation(
                        "Error while processing your request! (Getting List Contest Participant Score!). List was Empty!: " + contestLastUpdateResponse);
                    ViewBag.Error =
                        "Error while processing your request! (Getting List Contest Participant Score!).\n List was Empty!";
                    return RedirectToAction("ManagerContestDetail", "Manager", new { id });
                }
                else
                if (!contestLastUpdateResponse.Status)
                {
                    ViewBag.Error =
                        "Error while processing your request! (Getting List Contest Participant Score!).\n"
                        + contestLastUpdateResponse.ErrorMessage;
                    return RedirectToAction("ManagerContestDetail", "Manager", new { id });
                }
            }
            TempData["Success"] = contestPostResponse.SuccessMessage;
            return RedirectToAction("ManagerContestDetail", "Manager", new { id });
        }
        [HttpPost("Contest/Create")]
        /*[Route("Manager/Contest/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateContest(ContestViewModel createContest)
        {
            ManagerAPI_URL += "Contest/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_CONTEST_VALID, createContest, jsonOptions);
                return RedirectToAction("ManagerContest");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createContest,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Contest!).\n Contest Not Found!";
                return RedirectToAction("ManagerContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("ManagerContest");
            }
            TempData["Success"] = contestPostResponse.SuccessMessage;
            return RedirectToAction("ManagerContest");
        }

        [HttpPost("Contest/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelContest(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "Contest/Update/Cancel/" + id;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating Contest!).\n Contest Not Found!";
                return RedirectToAction("ManagerContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("ManagerContest");
            }
            TempData["Success"] = contestPostResponse.SuccessMessage;
            return RedirectToAction("ManagerContest");
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> ManagerProfile()
        {
            ManagerAPI_URL += "Manager/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? imagePath = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var managerInvalids = new ManagerProfileVM();

            var managerInvalidDetails = methcall.GetValidationTempData<MemberViewModel>(this, TempData, Constants.Constants.UPDATE_MANAGER_DETAILS_VALID, "managerDetail", jsonOptions);
            if (managerInvalidDetails != null)
            {
                managerInvalidDetails.ImagePath = imagePath;
                managerInvalidDetails.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(managerInvalidDetails.Gender);
                managerInvalids.managerDetail = managerInvalidDetails;
                return View(managerInvalids);
            }

            var managerDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ManagerAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (managerDetails == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!";
                return RedirectToAction("Index");
            }
            else
            if (!managerDetails.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!"
                + managerDetails.ErrorMessage;
                return RedirectToAction("Index");
            }
            var managerInvalidPasswordUpdate = methcall.GetValidationTempData<UpdateMemberPassword>(this, TempData, Constants.Constants.UPDATE_MANAGER_PASSWORD_VALID, "managerPassword", jsonOptions);
            if (managerInvalidPasswordUpdate != null)
            {
                managerInvalids.managerPassword = managerInvalidPasswordUpdate;
            }
            managerDetails.Data.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(managerDetails.Data.Gender);
            managerInvalids.managerDetail = managerDetails.Data;
            return View(managerInvalids);
        }
        [HttpPost("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ManagerProfileUpdate(MemberViewModel managerDetail)
        {
            ManagerAPI_URL += "Manager/Profile/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MANAGER_DETAILS_VALID, managerDetail, jsonOptions);
                return RedirectToAction("ManagerProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            managerDetail.MemberId = usrId;

            var managerDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: ManagerAPI_URL,
                _logger: _logger,
                inputType: managerDetail,
                accessToken: accToken);
            if (managerDetailupdate == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("ManagerProfile");
            }
            else
            if (!managerDetailupdate.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + managerDetailupdate.ErrorMessage;
                return RedirectToAction("ManagerProfile");
            }
            TempData["Success"] = "Successfully updated profile!";
            return RedirectToAction("ManagerProfile");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword managerPassword)
        {
            string ManagerChangePasswordAPI_URL = "/api/User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MANAGER_PASSWORD_VALID, managerPassword, jsonOptions);
                return RedirectToAction("ManagerProfile");
            }

            managerPassword.userId = usrId;

            var managerDetailupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: ManagerChangePasswordAPI_URL,
                _logger: _logger,
                inputType: managerPassword,
                accessToken: accToken);
            if (managerDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!";
                return RedirectToAction("ManagerProfile");
            }
            else
            if (!managerDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + managerDetailupdate.ErrorMessage;
                return RedirectToAction("ManagerProfile");
            }
            TempData["Success"] = "Successfully updated password!";
            return RedirectToAction("ManagerProfile");
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string ManagerAvatarAPI_URL = "/api/User/Upload";

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"avatar/{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;
                dynamic imageUpload = new ExpandoObject();
                imageUpload.ImagePath = image;
                imageUpload.MemberId = usrId;

                var getMemberAvatar = await methcall.CallMethodReturnObject<GetMemberAvatarResponse>(
                    _httpClient: _httpClient,
                    options: jsonOptions,
                    methodName: Constants.Constants.POST_METHOD,
                    url: ManagerAvatarAPI_URL,
                    _logger: _logger,
                    inputType: imageUpload,
                    accessToken: accToken);
                if (getMemberAvatar == null)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!";
                }
                else
                if (!getMemberAvatar.Status)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!"
                    + getMemberAvatar.ErrorMessage;
                }
                return RedirectToAction("ManagerProfile");
            }
            TempData["Success"] = "Successfully changed profile pic!";
            return RedirectToAction("ManagerProfile");
        }
        [HttpGet("Feedback")]
        public async Task<IActionResult> ManagerFeedBack()
        {
            ManagerAPI_URL += "Feedback/All";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var listFeedbackResponse = await methcall.CallMethodReturnObject<GetListFeedbackResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listFeedbackResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Feedback!). List was Empty!: " + listFeedbackResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Feedback!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listFeedbackResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Feedback!).\n"
                    + listFeedbackResponse.ErrorMessage;
                return View("ManagerIndex");
            }

            dynamic listFeedback = new ExpandoObject();
            listFeedback.Feedbacks = listFeedbackResponse.Data;

            return View(listFeedback);
        }
        [HttpGet("MemberStatus")]
        public async Task<IActionResult> ManagerMemberStatus([FromQuery] string? search)
        {
            ManagerAPI_URL += "Manager/MemberStatus";
            if (!string.IsNullOrEmpty(search))
            {
                ManagerAPI_URL += "?memberusername=" + search;
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            ManagerMemberStatusIndexVM managerMemberStatusListVM = new();

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListMemberStatus>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listMemberStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Member Status!). List was Empty!: " + listMemberStatusResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n"
                    + listMemberStatusResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            managerMemberStatusListVM.MemberStatuses = listMemberStatusResponse.Data;
            foreach(var status in managerMemberStatusListVM.MemberStatuses)
            {
                status.DefaultMemberStatusSelectList = methcall.GetMemberStatusSelectableList(status.Status);
            }
            return View(managerMemberStatusListVM);
        }
        [HttpPost("MemberStatus/Update")]
        public async Task<IActionResult> ManagerUpdateMemberStatus(List<GetMemberStatus> listRequest)
        {
            ManagerAPI_URL += "Manager/MemberStatus/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListMemberStatusUpdate>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                inputType: listRequest,
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listMemberStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Member Status!). List was Empty!: " + listMemberStatusResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n"
                    + listMemberStatusResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            TempData["Success"] = listMemberStatusResponse.SuccessMessage;
            return RedirectToAction("ManagerMemberStatus");
        }
        [HttpGet("Statistical")]
        public IActionResult ManagerStatistical()
        {
            return View();
        }
        [HttpGet("Blog")]
        public IActionResult ManagerBlog([FromQuery] string search)
        {
            ManagerAPI_URL += "Blog/All";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            return View();
        }
        [HttpGet("News")]
        public async Task<IActionResult> ManagerNews([FromQuery] string? search)
        {
            ManagerAPI_URL += "News/All";

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            ManagerNewsIndexVM managerNewsListVM = new();

            var listNewsResponse = await methcall.CallMethodReturnObject<GetListNews>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listNewsResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List News Status!). List was Empty!: " + listNewsResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List News Status!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listNewsResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List News Status!).\n"
                    + listNewsResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            managerNewsListVM.News = listNewsResponse.Data;
            managerNewsListVM.createNews = methcall.GetValidationTempData<CreateNewNews>(this, TempData, Constants.Constants.CREATE_NEWS_VALID, "createNews", jsonOptions);
            if (managerNewsListVM.createNews == null)
            {
                managerNewsListVM.createNews = new CreateNewNews();
            }
            return View(managerNewsListVM);
        }
        [HttpPost("News/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateNews([Required] CreateNewNews createNews)
        {
            ManagerAPI_URL += "News/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_NEWS_VALID, createNews, jsonOptions);
                return RedirectToAction("ManagerNews");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            createNews.MemberId = usrId;

            IFormFile photo = createNews.ImageUpload;

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"news/{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;

                createNews.Picture = image;
            }

            createNews.ImageUpload = null;

            var managerCreateNewsPostVM = await methcall.CallMethodReturnObject<GetNewsPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: ManagerAPI_URL,
                                inputType: createNews,
                                accessToken: accToken,
                                _logger: _logger);
            if (managerCreateNewsPostVM == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create News!).\n News Not Found!";
                TempData["Error"] = managerCreateNewsPostVM.ErrorMessage;
                return RedirectToAction("ManagerNews");
            }
            if (!managerCreateNewsPostVM.Status)
            {
                TempData["Error"] = managerCreateNewsPostVM.ErrorMessage;
                _logger.LogInformation("Error while processing your request: " + managerCreateNewsPostVM.Status + " , Error Message: " + managerCreateNewsPostVM.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create News Post!).\n"
                    + managerCreateNewsPostVM.ErrorMessage;
                return RedirectToAction("ManagerNews");
            }
            TempData["Success"] = "Successfully create News!";
            return RedirectToAction("ManagerNews");
        }
        [HttpGet("News/{id:int}")]
        /*[Route("Manager/Contest/{id:int}")]*/
        public async Task<IActionResult> ManagerNewsDetail(
            [FromRoute][Required] int id
            )
        {
            ManagerAPI_URL += "News/" + id;
            ManagerNewsDetailsVM contestDetailBigModel = new();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            var managerNewsPostVM = await methcall.CallMethodReturnObject<GetNewsPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                _logger: _logger);
            if (managerNewsPostVM == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return RedirectToAction("ManagerNews");
            }
            if (!managerNewsPostVM.Status)
            {
                _logger.LogInformation("Error while processing your request: " + managerNewsPostVM.Status + " , Error Message: " + managerNewsPostVM.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + managerNewsPostVM.ErrorMessage;
                return RedirectToAction("ManagerNews");
            }
            contestDetailBigModel.updateNews = methcall.GetValidationTempData<NewsViewModel>(this, TempData, Constants.Constants.UPDATE_NEWS_VALID, "updateNews", jsonOptions);
            contestDetailBigModel.News = managerNewsPostVM.Data;

            return View(contestDetailBigModel);
        }
        [HttpPost("News/{id:int}/Update")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateNewsDetail(
            [FromRoute][Required] int id,
            [Required] NewsViewModel updateNews
            )
        {
            ManagerAPI_URL += "News/" + id + "/Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, updateNews, jsonOptions);
                return RedirectToAction("ManagerNewsDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var managerUpdateNewsPostVM = await methcall.CallMethodReturnObject<GetNewsPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: ManagerAPI_URL,
                                inputType: updateNews,
                                accessToken: accToken,
                                _logger: _logger);
            if (managerUpdateNewsPostVM == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating News!).\n News Not Found!";
                return RedirectToAction("ManagerNewsDetail", new { id });
            }
            if (!managerUpdateNewsPostVM.Status)
            {
                _logger.LogInformation("Error while processing your request: " + managerUpdateNewsPostVM.Status + " , Error Message: " + managerUpdateNewsPostVM.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating News Post!).\n"
                    + managerUpdateNewsPostVM.ErrorMessage;
                return RedirectToAction("ManagerNewsDetail", new { id });
            }
            TempData["Success"] = "Successfully update News details";
            return RedirectToAction("ManagerNewsDetail", new { id });
        }
        [HttpPost("News/{id:int}/Disable")]
        public async Task<IActionResult> ManagerDisableNews(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "News/" + id + "/Disable";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MANAGER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetNewsPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Disabling News Post!).\n Post Not Found!";
                return RedirectToAction("ManagerNews");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Disabling News Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerNews");
            }
            TempData["Success"] = "Successfully disabled News post";
            return RedirectToAction("ManagerNews");
        }
        [HttpGet("Notification")]
        public IActionResult ManagerNotification()
        {
            return View();
        }
    }
}
