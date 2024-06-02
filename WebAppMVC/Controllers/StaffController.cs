using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
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
using Azure;
using Microsoft.DotNet.MSIdentity.Shared;
using System.Security.Policy;
using BAL.ViewModels.Member;
using BAL.ViewModels.Staff;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using WebAppMVC.Models.Staff;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DAL.Models;
using Microsoft.AspNetCore.Http.Json;
using WebAppMVC.Models.ViewModels;

namespace WebAppMVC.Controllers
{
    [Route("Staff")]
    public class StaffController : Controller
    {
        private readonly ILogger<MeetingController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string StaffAPI_URL = "";
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
        private BirdClubLibrary methcall = new();

        public StaffController(ILogger<MeetingController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            StaffAPI_URL = "/api/";
        }

        // GET: StaffController
        [HttpGet("Index")]
        public IActionResult StaffIndex()
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            return View();
        }
        [HttpGet("Meeting")]
        public async Task<IActionResult> StaffMeeting([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = StaffAPI_URL + "Location/All";
            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                StaffAPI_URL += "Meeting/Search?meetingName=" + search;
            }
            else StaffAPI_URL += "Meeting/All";

            dynamic testmodel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: StaffAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listMeetResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!listMeetResponse.Status || !listLocationResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("StaffIndex");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
        }
        [HttpGet("Meeting/{id:int}")]
        /*[Route("Staff/Meeting/{id:int}")]*/
        public async Task<IActionResult> StaffMeetingDetail(int id)
        {
            string StaffMeetingDetailAPI_URL = StaffAPI_URL + "Meeting/AllParticipants/" + id;
            StaffAPI_URL += "Meeting/" + id;
            dynamic meetingDetailBigModel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffAPI_URL,
                                _logger: _logger);
            var meetpartPostResponse = await methcall.CallMethodReturnObject<GetListMeetingParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffMeetingDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeeting");
            }
            meetingDetailBigModel.UpdateMeeting = methcall.GetValidationTempData<MeetingViewModel>(this, TempData, Constants.Constants.UPDATE_MEETING_VALID, "updateMeeting", jsonOptions);
            meetingDetailBigModel.SelectListStatus = methcall.GetStaffEventStatusSelectableList(meetPostResponse.Data.Status);
            meetingDetailBigModel.SelectListParticipationStatus = methcall.GetStaffEventParticipationStatusSelectableList(meetPostResponse.Data.Status);
            meetingDetailBigModel.MeetingDetails = meetPostResponse.Data;
            meetingDetailBigModel.MeetingParticipants = meetpartPostResponse.Data;
            return View(meetingDetailBigModel);
        }
        [HttpPost("Meeting/{id:int}/Update")]
        public async Task<IActionResult> StaffUpdateMeetingStatus(
            [FromRoute][Required] int id,
            [Required] MeetingViewModel updateMeeting)
        {
            StaffAPI_URL += "Meeting/" + id + "/Update";
            if (updateMeeting.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateMeeting.NumberOfParticipantsLimit - updateMeeting.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateMeeting.Status", "Error while processing your request (Updating Meeting).\n Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateMeeting, jsonOptions);
                TempData["Error"] = "Not enough participants to close registration!";
                return RedirectToAction("StaffMeetingDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: updateMeeting,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);

                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating Meeting Status!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeetingDetail", new { id });
            }
            if (!meetPostResponse.Status)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, jsonOptions);

                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating Meeting Status!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeetingDetail", new { id });
            }
            return RedirectToAction("StaffMeetingDetail", new { id });
        }

        [HttpPost("Meeting/UpdateStatus/{id:int}")]
        public async Task<IActionResult> StaffUpdateMeetingPartStatus(
            int id,
            List<MeetingParticipantViewModel> meetPartView)
        {
            StaffAPI_URL += "Staff/MeetingStatus/Update/" + id;
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var meetPartStatusResponse = await methcall.CallMethodReturnObject<GetCheckInStatusUpdate>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: meetPartView,
                                accessToken: accToken,
                                _logger: _logger);

            if (meetPartStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting Participant Status!). List was Empty!: " + meetPartStatusResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting Participant Status!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!meetPartStatusResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting Participant Status!).\n"
                    + meetPartStatusResponse.ErrorMessage;
                return View("StaffIndex");
            }
            TempData["Success"] = meetPartStatusResponse.SuccessMessage;
            return RedirectToAction("StaffMeetingDetail", "Staff", new { id });
        }
        [HttpGet("FieldTrip")]
        public async Task<IActionResult> StaffFieldtrip([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = StaffAPI_URL + "Location/All";
            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                StaffAPI_URL += "FieldTrip/Search?tripName=" + search;
            }
            else StaffAPI_URL += "FieldTrip/All";

            dynamic testmodel2 = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: StaffAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listFieldTripResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List FieldTrip!). List was Empty!: " + listLocationResponse + ",\n" + listFieldTripResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List FieldTrip!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!listFieldTripResponse.Status || !listLocationResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List FieldTrip!).\n"
                    + listFieldTripResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("StaffIndex");
            }
            testmodel2.FieldTrips = listFieldTripResponse.Data;
            testmodel2.Locations = listLocationResponse.Data;
            return View(testmodel2);
        }
        [HttpGet("FieldTrip/{id:int}")]
        /*[Route("Staff/FieldTrip/{id:int}")]*/
        public async Task<IActionResult> StaffFieldTripDetail(int id)
        {
            string StaffFieldTripDetailAPI_URL = StaffAPI_URL + "FieldTrip/AllParticipants/" + id;
            StaffAPI_URL += "FieldTrip/" + id;
            dynamic fieldtripDetailBigModel = new ExpandoObject();
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffAPI_URL,
                                _logger: _logger);
            var fieldtrippartPostResponse = await methcall.CallMethodReturnObject<GetListFieldTripParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffFieldTripDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("StaffFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTrip");
            }
            fieldtripDetailBigModel.FieldTripDetails = fieldtripPostResponse.Data;
            fieldtripDetailBigModel.UpdateFieldTrip = methcall.GetValidationTempData<FieldTripViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, "updateTrip", jsonOptions);
            fieldtripDetailBigModel.FieldTripTourFeatures = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("tour_features")).ToList();
            fieldtripDetailBigModel.FieldTripImportantToKnows = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("important_to_know")).ToList();
            fieldtripDetailBigModel.FieldTripActivitiesAndTransportation = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type.Equals("activities_and_transportation")).ToList();
            fieldtripDetailBigModel.FieldTripParticipants = fieldtrippartPostResponse.Data;
            fieldtripDetailBigModel.SelectListParticipationStatus = methcall.GetStaffEventParticipationStatusSelectableList(fieldtripPostResponse.Data.Status);
            fieldtripDetailBigModel.SelectListStatus = methcall.GetStaffEventStatusSelectableList(fieldtripPostResponse.Data.Status);

            return View(fieldtripDetailBigModel);
        }
        [HttpPost("FieldTrip/{id:int}/Update")]
        public async Task<IActionResult> StaffUpdateFieldTripStatus(
            [FromRoute][Required] int id,
            [Required] FieldTripViewModel updateTrip)
        {
            StaffAPI_URL += "FieldTrip/" + id + "/Update";

            if (updateTrip.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateTrip.NumberOfParticipantsLimit - updateTrip.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateTrip.Status", "Error while processing your request (Updating FieldTrip).\n Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, updateTrip, jsonOptions);
                TempData["Error"] = "Not enough participants to close registration!";
                return RedirectToAction("StaffFieldTripDetail", new { id });
            }
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: updateTrip,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating FieldTrip Status!).\n FieldTrip Not Found!";
                return RedirectToAction("StaffFieldTripDetail", new { id });
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating FieldTrip Status!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTripDetail", new { id });
            }
            return RedirectToAction("StaffFieldTripDetail", new { id });
        }

        [HttpPost("FieldTrip/UpdateStatus/{id:int}")]
        public async Task<IActionResult> StaffUpdateFieldTripPartStatus(
            int id,
            List<FieldTripParticipantViewModel> tripPartView)
        {
            StaffAPI_URL += "Staff/FieldTripStatus/Update/" + id;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var tripPartStatusResponse = await methcall.CallMethodReturnObject<GetCheckInStatusUpdate>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: tripPartView,
                                accessToken: accToken,
                                _logger: _logger);

            if (tripPartStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Field Trip Participant Status!). List was Empty!: " + tripPartStatusResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Field Trip Participant Status!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!tripPartStatusResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Field Trip Participant Status!).\n"
                    + tripPartStatusResponse.ErrorMessage;
                return View("StaffIndex");
            }
            TempData["Success"] = tripPartStatusResponse.SuccessMessage;
            return RedirectToAction("StaffFieldTripDetail", "Staff", new { id });
        }

        [HttpGet("Contest")]
        public async Task<IActionResult> StaffContest([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = StaffAPI_URL + "Location/All";
            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                StaffAPI_URL += "Contest/Search?contestName=" + search;
            }
            else StaffAPI_URL += "Contest/All";

            dynamic testmodel3 = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: StaffAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listContestResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!listContestResponse.Status || !listLocationResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listContestResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("StaffIndex");
            }
            testmodel3.Contests = listContestResponse.Data;
            testmodel3.Locations = listLocationResponse.Data;
            return View(testmodel3);
        }
        [HttpGet("Contest/{id:int}")]
        /*[Route("Staff/Contest/{id:int}")]*/
        public async Task<IActionResult> StaffContestDetail(
            [FromRoute][Required] int id
            )
        {
            string StaffContestDetailAPI_URL = StaffAPI_URL + "Contest/AllParticipants/" + id;
            StaffAPI_URL += "Contest/" + id;
            dynamic contestDetailBigModel = new ExpandoObject();

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffAPI_URL,
                                _logger: _logger);
            var contestpartPostResponse = await methcall.CallMethodReturnObject<GetListContestParticipation>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.GET_METHOD,
                                url: StaffContestDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return RedirectToAction("StaffContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContest");
            }

            contestDetailBigModel.UpdateContest = methcall.GetValidationTempData<ContestViewModel>(this, TempData, Constants.Constants.UPDATE_CONTEST_VALID, "updateContest", jsonOptions);
            contestDetailBigModel.ContestDetails = contestPostResponse.Data;
            contestDetailBigModel.ContestParticipants = contestpartPostResponse.Data;
            contestDetailBigModel.SelectListParticipationStatus = methcall.GetStaffEventParticipationStatusSelectableList(contestPostResponse.Data.Status);
            contestDetailBigModel.SelectListStatus = methcall.GetStaffEventStatusSelectableList(contestPostResponse.Data.Status);
            return View(contestDetailBigModel);
        }
        [HttpPost("Contest/{id:int}/Status/Update")]
        public async Task<IActionResult> StaffUpdateContestStatus(
            [FromRoute][Required] int id,
            [Required] ContestViewModel updateContest
            )
        {
            StaffAPI_URL += "Contest/Update/" + id;

            if (updateContest.Status.Equals(Constants.Constants.EVENT_STATUS_CLOSED_REGISTRATION) && (updateContest.NumberOfParticipantsLimit - updateContest.NumberOfParticipants) < 10)
            {
                ModelState.AddModelError("updateContest.Status", "Error while processing your request (Updating Contest).\n Not enough people to closed registration");
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateContest, jsonOptions);
                TempData["Error"] = "Not enough participants to close registration!";
                return RedirectToAction("StaffContestDetail", new { id });
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateContest, jsonOptions);
                return RedirectToAction("StaffContestDetail", new { id });
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: updateContest,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating Contest Status!).\n Contest Not Found!";
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Updating Contest Status!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            return RedirectToAction("StaffContestDetail", "Staff", new { id });
        }

        [HttpPost("Contest/{id:int}/Participant/All/Status/Update")]
        public async Task<IActionResult> StaffUpdateContestPartStatus(
            [FromRoute][Required] int id,
            [Required] List<ContestParticipantViewModel> contestPartView)
        {
            StaffAPI_URL += "Staff/Contest/" + id + "/Participant/All/Status/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            if (contestPartView.Count == 0)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List Contest Participant Status!).\n List was Empty!";
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }

            var contestPartStatusResponse = await methcall.CallMethodReturnObject<GetCheckInStatusUpdate>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: contestPartView,
                                accessToken: accToken,
                                _logger: _logger);

            if (contestPartStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest Participant Status!). List was Empty!: " + contestPartStatusResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Contest Participant Status!).\n List was Empty!";
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            else
            if (!contestPartStatusResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Contest Participant Status!).\n"
                    + contestPartStatusResponse.ErrorMessage;
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            TempData["Success"] = contestPartStatusResponse.SuccessMessage;
            return RedirectToAction("StaffContestDetail", "Staff", new { id });
        }
        [HttpPost("Contest/{id:int}/Participant/All/Score/Update")]
        public async Task<IActionResult> StaffUpdateContestPartScore(
            [FromRoute][Required] int id,
            [Required] List<ContestParticipantViewModel> contestPartView)
        {
            StaffAPI_URL += "Staff/Contest/" + id + "/Participant/All/Score/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            if (contestPartView.Count == 0)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List Contest Participant Status!).\n List was Empty!";
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }

            var contestPartScoresResponse = await methcall.CallMethodReturnObject<GetCheckInStatusUpdate>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.PUT_METHOD,
                                url: StaffAPI_URL,
                                inputType: contestPartView,
                                accessToken: accToken,
                                _logger: _logger);

            if (contestPartScoresResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest Participant Score!). List was Empty!: " + contestPartScoresResponse);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Contest Participant Score!).\n List was Empty!";
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            else
            if (!contestPartScoresResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Contest Participant Score!).\n"
                    + contestPartScoresResponse.ErrorMessage;
                return RedirectToAction("StaffContestDetail", "Staff", new { id });
            }
            return RedirectToAction("StaffContestDetail", "Staff", new { id });
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> StaffProfile()
        {
            StaffAPI_URL += "Staff/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);
            string? imagePath = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE);

            var staffInvalids = new StaffProfileVM();
            var staffInvalidDetails = methcall.GetValidationTempData<MemberViewModel>(this, TempData, Constants.Constants.UPDATE_STAFF_DETAILS_VALID, "staffDetail", jsonOptions);
            if (staffInvalidDetails != null)
            {
                staffInvalidDetails.ImagePath = imagePath;
                staffInvalidDetails.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(staffInvalidDetails.Gender);
                staffInvalids.staffDetail = staffInvalidDetails;
                return View(staffInvalids);
            }

            var staffDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: StaffAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (staffDetails == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!";
                return RedirectToAction("Index");
            }
            else
            if (!staffDetails.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!"
                + staffDetails.ErrorMessage;
                return RedirectToAction("Index");
            }
            var staffInvalidPasswordUpdate = methcall.GetValidationTempData<UpdateMemberPassword>(this, TempData, Constants.Constants.UPDATE_STAFF_PASSWORD_VALID, "staffPassword", jsonOptions);
            if (staffInvalidPasswordUpdate != null)
            {
                staffInvalids.staffPassword = staffInvalidPasswordUpdate;
            }
            staffDetails.Data.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(staffDetails.Data.Gender);
            staffInvalids.staffDetail = staffDetails.Data;
            return View(staffInvalids);
        }
        [HttpPost("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> StaffProfileUpdate(MemberViewModel staffDetail)
        {
            StaffAPI_URL += "Staff/Profile/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_STAFF_DETAILS_VALID, staffDetail, jsonOptions);
                return RedirectToAction("StaffProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            staffDetail.MemberId = usrId;

            var staffDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: StaffAPI_URL,
                _logger: _logger,
                inputType: staffDetail,
                accessToken: accToken);
            if (staffDetailupdate == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("StaffProfile");
            }
            else
            if (!staffDetailupdate.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + staffDetailupdate.ErrorMessage;
                return RedirectToAction("StaffProfile");
            }
            TempData["Success"] = "Successfully updated profile!";
            return RedirectToAction("StaffProfile");
        }
        [HttpPost("ChangePassword")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword staffPassword)
        {
            string MemberChangePasswordAPI_URL = StaffAPI_URL + "User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_STAFF_PASSWORD_VALID, staffPassword, jsonOptions);
                return RedirectToAction("StaffProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            staffPassword.userId = usrId;

            var staffPasswordupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: MemberChangePasswordAPI_URL,
                _logger: _logger,
                inputType: staffPassword,
                accessToken: accToken);
            if (staffPasswordupdate == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!";
                return RedirectToAction("StaffProfile");
            }
            else
            if (!staffPasswordupdate.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!"
                + staffPasswordupdate.ErrorMessage;
                return RedirectToAction("StaffProfile");
            }
            TempData["Success"] = "Successfully updated password!";
            return RedirectToAction("StaffProfile");
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.STAFF));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string StaffAvatarAPI_URL = StaffAPI_URL + "User/Upload";

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_CONNECTION_STRING);
                string defaultUrl = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_URL);
                string containerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_NAME);
                string avatarContainerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_BLOB_AVATAR_FOLDER_URL);

                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = avatarContainerName + $"{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = defaultUrl + uniqueBlobName;
                UpdateMemberAvatar imageUpload = new(memberId: usrId, imagePath: image);

                var getMemberAvatar = await methcall.CallMethodReturnObject<GetMemberAvatarResponse>(
                    _httpClient: _httpClient,
                    options: jsonOptions,
                    methodName: Constants.Constants.POST_METHOD,
                    url: StaffAvatarAPI_URL,
                    _logger: _logger,
                    inputType: imageUpload,
                    accessToken: accToken);
                if (getMemberAvatar == null)
                {
                    TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                        "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!";
                }
                else if (!getMemberAvatar.Status)
                {
                    TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                        "Error while processing your request! (Getting Staff Profile!).\n Staff Details Not Found!"
                    + getMemberAvatar.ErrorMessage;
                }
                TempData[Constants.Constants.ALERT_DEFAULT_SUCCESS_NAME] = Constants.Constants.ALERT_USER_AVATAR_IMAGE_UPDATE_SUCCESS;
                HttpContext.Session.SetString(Constants.Constants.USR_IMAGE, getMemberAvatar.Data.ImagePath);
                return RedirectToAction("StaffProfile");
            }
            return RedirectToAction("StaffProfile");
        }
    }
}