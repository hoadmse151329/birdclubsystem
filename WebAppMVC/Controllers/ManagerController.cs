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
using WebAppMVC.Models.Manager;
using BAL.ViewModels.Manager;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    [Route("Manager")]
    public class ManagerController : Controller
    {

        private readonly ILogger<MeetingController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string ManagerAPI_URL = "";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
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

        public ManagerController(ILogger<MeetingController> logger, IConfiguration config)
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
        public IActionResult ManagerIndex()
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index","Home");

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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
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

            testmodel.CreateMeeting = methcall.GetValidationTempData<MeetingViewModel>(this, TempData, Constants.Constants.CREATE_MEETING_VALID, "createMeeting", options);
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

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
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var meetpartPostResponse = await methcall.CallMethodReturnObject<GetListMeetingParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
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
            meetingDetailBigModel.UpdateMeeting = methcall.GetValidationTempData<MeetingViewModel>(this,TempData, Constants.Constants.UPDATE_MEETING_VALID, "updateMeeting",options);
            meetingDetailBigModel.SelectListStatus = methcall.GetManagerEventStatusSelectableList(meetPostResponse.Data.Status);

            meetingDetailBigModel.MeetingDetails = meetPostResponse.Data;
            meetingDetailBigModel.MeetingParticipants = meetpartPostResponse.Data;

            return View(meetingDetailBigModel);
        }
        [HttpPost("Meeting/{id:int}/Update")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateMeetingDetail(
            [FromRoute] [Required] int id,
            [Required] MeetingViewModel updateMeeting
            )
        {
            ManagerAPI_URL += "Meeting/Update/" + id;

			if (!ModelState.IsValid)
			{
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, options);
				return RedirectToAction("ManagerMeetingDetail", new {id});
			}
			string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

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
                                options: options,
                                methodName: "PUT",
                                url: ManagerAPI_URL,
                                inputType: updateMeeting,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, options);

                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }
            if (!meetPostResponse.Status)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_MEETING_VALID, updateMeeting, options);

                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeetingDetail", new { id });
            }
            return RedirectToAction("ManagerMeetingDetail",new { id });
        }
        [HttpPost("Meeting/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateMeeting([Required] MeetingViewModel createMeeting)
        {
            ManagerAPI_URL += "Meeting/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.CREATE_MEETING_VALID, createMeeting, options);
                return RedirectToAction("ManagerMeeting");
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

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
                                options: options,
                                methodName: "POST",
                                url: ManagerAPI_URL,
                                inputType: createMeeting,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, options);
                TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                string validJson = JsonSerializer.Serialize(createMeeting, options);
                TempData[Constants.Constants.CREATE_MEETING_VALID] = validJson;
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            return RedirectToAction("ManagerMeeting");
        }

        [HttpPost("Meeting/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelMeeting(
            [FromRoute] [Required] int id)
        {
            ManagerAPI_URL += "Meeting/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

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
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
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

            fieldtripIndexVM.CreateFieldTrip = methcall.GetValidationTempData<FieldTripViewModel>(this,TempData, Constants.Constants.CREATE_FIELDTRIP_VALID, "createFieldTrip",options);
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var fieldtrippartPostResponse = await methcall.CallMethodReturnObject<GetListFieldTripParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
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
            fieldtripDetailVM.UpdateFieldTrip = methcall.GetValidationTempData<FieldTripViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, "updateTrip", options);
            fieldtripDetailVM.UpdateFieldTripGettingThere = methcall.GetValidationTempData<FieldtripGettingThereViewModel>(this, TempData, Constants.Constants.UPDATE_FIELDTRIP_GETTHERE_VALID, "updateGettingThere", options);
            fieldtripDetailVM.CreateFieldTripDayByDay = methcall.GetValidationTempData<FieldtripDaybyDayViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_DAYBYDAY_VALID, "createDayByDay", options);
            fieldtripDetailVM.CreateFieldTripInclusion = methcall.GetValidationTempData<FieldtripInclusionViewModel>(this, TempData, Constants.Constants.CREATE_FIELDTRIP_INCLUSION_VALID, "createInclusion", options);
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
            ManagerAPI_URL += "FieldTrip/Update/" + id;
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_VALID, updateTrip, options);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
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
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/{id:int}/Update/GettingThere")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripGettingThereDetail(
            [FromRoute][Required] int id,
            [Required] FieldtripGettingThereViewModel updateGettingThere
            )
        {
            ManagerAPI_URL += "FieldTrip/" + id + "/Update/GettingThere";
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_FIELDTRIP_GETTHERE_VALID, updateGettingThere, options);
                return RedirectToAction("ManagerFieldTripDetail", new { id });
            }
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var ftGettingThereResponse = await methcall.CallMethodReturnObject<GetFieldTripGettingThereResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
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
            return RedirectToAction("ManagerFieldTripDetail", new { id });
        }
        [HttpPost("FieldTrip/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTrip(FieldTripViewModel createFieldTrip)
        {
            ManagerAPI_URL += "FieldTrip/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_VALID, createFieldTrip, options);
                return RedirectToAction("ManagerFieldtrip");
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
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
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_DAYBYDAY_VALID, createDayByDay, options);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var ftDayByDayResponse = await methcall.CallMethodReturnObject<GetFieldTripDayByDayResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
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
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.CREATE_FIELDTRIP_INCLUSION_VALID, createInclusion, options);
                return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var ftInclusionResponse = await methcall.CallMethodReturnObject<GetFieldTripInclusionResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
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
            return RedirectToAction("ManagerFieldTripDetail", new { id = tripId });
        }
        [HttpPost("FieldTrip/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelFieldTrip(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "FieldTrip/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
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
            testmodel3.CreateContest = methcall.GetValidationTempData<ContestViewModel>(this, TempData, Constants.Constants.CREATE_CONTEST_VALID, "createContest", options);
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                _logger: _logger);
            var contestpartPostResponse = await methcall.CallMethodReturnObject<GetListContestParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
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
            contestDetailBigModel.UpdateContest = methcall.GetValidationTempData<ContestViewModel>(this, TempData, Constants.Constants.UPDATE_CONTEST_VALID, "updateContest", options);
            contestDetailBigModel.SelectListStatus = methcall.GetManagerEventStatusSelectableList(contestPostResponse.Data.Status);
            contestDetailBigModel.ContestDetails = contestPostResponse.Data;
            contestDetailBigModel.ContestParticipants = contestpartPostResponse.Data;

            return View(contestDetailBigModel);
        }
        [HttpPost("Contest/{id:int}/Update")]
        /*[Route("Manager/Contest/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateContestDetail(
            [FromRoute][Required] int id,
            [Required] ContestViewModel updateContest
            )
        {
            ManagerAPI_URL += "Contest/Update/" + id;
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.UPDATE_CONTEST_VALID, updateContest, options);
                return RedirectToAction("ManagerContestDetail", "Manager", new { id });
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
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
            return RedirectToAction("ManagerContestDetail", "Manager", new { id });
        }
        [HttpPost("Contest/Create")]
        /*[Route("Manager/Contest/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateContest(ContestViewModel createContest)
        {
            ManagerAPI_URL += "Contest/Create";
            if (!ModelState.IsValid)
            {
                TempData = methcall.GetValidationTempData(TempData, Constants.Constants.CREATE_CONTEST_VALID, createContest, options);
                return RedirectToAction("ManagerContest");
            }

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
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
            return RedirectToAction("ManagerContest");
        }

        [HttpPost("Contest/{id:int}/Cancel")]
        public async Task<IActionResult> ManagerCancelContest(
            [FromRoute][Required] int id)
        {
            ManagerAPI_URL += "Contest/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
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
            return RedirectToAction("ManagerContest");
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> ManagerProfile()
        {
            ManagerAPI_URL += "Manager/Profile";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: ManagerAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (memberDetails == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!";
                return RedirectToAction("Index");
            }
            else
            if (!memberDetails.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!"
                + memberDetails.ErrorMessage;
                return RedirectToAction("Index");
            }
            return View(memberDetails.Data);
        }
        [HttpPost("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ManagerProfileUpdate(MemberViewModel memberDetail)
        {
            ManagerAPI_URL += "Manager/Profile/Update";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            memberDetail.MemberId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "PUT",
                url: ManagerAPI_URL,
                _logger: _logger,
                inputType: memberDetail,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("ManagerProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("ManagerProfile");
            }
            return RedirectToAction("ManagerProfile");
        }
		[HttpPost("ChangePassword")]
		//[Authorize(Roles = "Member")]
		public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
		{
			string ManagerChangePasswordAPI_URL = "/api/User/ChangePassword";

			string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
			if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

			string? role = HttpContext.Session.GetString("ROLE_NAME");
			if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
			else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

			string? usrId = HttpContext.Session.GetString("USER_ID");
			if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

			string? usrname = HttpContext.Session.GetString("USER_NAME");
			if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

			string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

			TempData["ROLE_NAME"] = role;
			TempData["USER_NAME"] = usrname;
			TempData["IMAGE_PATH"] = imagepath;

			memberPassword.userId = usrId;

			var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
				_httpClient: _httpClient,
				options: options,
				methodName: "PUT",
				url: ManagerChangePasswordAPI_URL,
				_logger: _logger,
				inputType: memberPassword,
				accessToken: accToken);
			if (memberDetailupdate == null)
			{
				ViewBag.error =
					"Error while processing your request! (Getting Manager Profile!).\n Manager Details Not Found!";
				return RedirectToAction("ManagerProfile");
			}
			else
			if (!memberDetailupdate.Status)
			{
				ViewBag.error =
					"Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
				+ memberDetailupdate.ErrorMessage;
				return RedirectToAction("ManagerProfile");
			}
			return RedirectToAction("ManagerProfile");
		}
		[HttpGet("Feedback")]
        public IActionResult ManagerFeedBack()
        {
            return View();
        }
        [HttpGet("MemberStatus")]
        public async Task<IActionResult> ManagerMemberStatus([FromQuery] string search)
        {
            _logger.LogInformation(search);

            /*if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                ManagerAPI_URL += "Manager/Search?meetingName=" + search;
            }
            else */
                ManagerAPI_URL += "Manager/MemberStatus";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListMemberStatus>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: ManagerAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listMemberStatusResponse == null )
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
            return View(listMemberStatusResponse.Data);
        }
        [HttpPost("MemberStatus/Update")]
        public async Task<IActionResult> ManagerUpdateMemberStatus(List<GetMemberStatus> listRequest)
        {
            ManagerAPI_URL += "Manager/MemberStatus/Update";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListMemberStatusUpdate>(
                _httpClient: _httpClient,
                options: options,
                methodName: "PUT",
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
            return RedirectToAction("ManagerMemberStatus");
        }
        [HttpGet("Statistical")]
        public IActionResult ManagerStatistical()
        {
            return View();
        }
        [HttpGet("Blog")]
        public IActionResult ManagerBlog()
        {
            return View();
        }
        [HttpGet("News")]
        public IActionResult ManagerNews()
        {
            return View();
        }
        [HttpGet("Notification")]
        public IActionResult ManagerNotification()
        {
            return View();
        }
    }
}
