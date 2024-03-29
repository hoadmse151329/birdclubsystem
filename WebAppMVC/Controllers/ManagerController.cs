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
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    [Route("Manager")]
    public class ManagerController : Controller
    {

        private readonly ILogger<MeetingController> _logger;
        private readonly HttpClient _httpClient = null;
        private string ManagerAPI_URL = "";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private MethodCaller methcall = new();

        public ManagerController(ILogger<MeetingController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
            ManagerAPI_URL = "/api/";
        }

        // GET: ManagerController
        [HttpGet("Index")]
        public IActionResult ManagerIndex()
        {
            return View();
        }
        [HttpGet("Meeting")]
        public async Task<IActionResult> ManagerMeeting([FromQuery] string search)
        {
            _logger.LogInformation(search);
            string LocationAPI_URL_All = ManagerAPI_URL + "Location/All";
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
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: ManagerAPI_URL,
                _logger: _logger);

            if (listMeetResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse);
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listMeetResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
        }
        public IActionResult ManagerNotification()
        {
            return View();
        }
        [HttpGet("Meeting/{id:int}")]
        /*[Route("Manager/Meeting/{id:int}")]*/
        public async Task<IActionResult> ManagerMeetingDetail(int id)
        {
            string ManagerMeetingDetailAPI_URL = ManagerAPI_URL + "Meeting/AllParticipants/" + id;
            ManagerAPI_URL += "Meeting/" + id;
            dynamic meetingDetailBigModel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

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
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
               return RedirectToAction("ManagerMeeting");
            }
            meetingDetailBigModel.MeetingDetails = meetPostResponse.Data;
            meetingDetailBigModel.MeetingParticipants = meetpartPostResponse.Data;
            return View(meetingDetailBigModel);
        }
        [HttpPost("Meeting/Update/{id:int}")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateMeetingDetail(
            int id,
            MeetingViewModel meetView
            )
        {
            ManagerAPI_URL += "Meeting/Update/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
                                url: ManagerAPI_URL,
                                inputType: meetView,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            return RedirectToAction("ManagerMeetingDetail", "Manager",new { id = id });
        }
        [HttpPost("Meeting/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateMeeting(MeetingViewModel meetView)
        {
            ManagerAPI_URL += "Meeting/Create";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
                                url: ManagerAPI_URL,
                                inputType: meetView,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Create Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("ManagerMeeting");
            }
            return RedirectToAction("ManagerMeeting");
        }

        [HttpPost("Meeting/Update/Cancel/{id:int}")]
        public async Task<IActionResult> ManagerCancelMeeting(
            int id)
        {
            ManagerAPI_URL += "Meeting/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("ManagerMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
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
            string LocationAPI_URL_All = ManagerAPI_URL + "Location/All";
            if (search != null || !string.IsNullOrEmpty(search))
            {
                search = search.Trim();
                ManagerAPI_URL += "FieldTrip/Search?tripName=" + search;
            }
            else ManagerAPI_URL += "FieldTrip/All";

            dynamic testmodel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: ManagerAPI_URL,
                _logger: _logger);

            if (listFieldTripResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List FieldTrip!). List was Empty!: " + listFieldTripResponse);
                ViewBag.error =
                    "Error while processing your request! (Getting List FieldTrip!).\n List was Empty!";
                return View("ManagerIndex");
            }
            else
            if (!listFieldTripResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List FieldTrip!).\n"
                    + listFieldTripResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                return View("ManagerIndex");
            }
            testmodel.FieldTrips = listFieldTripResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
        }
        [HttpGet("FieldTrip/{id:int}")]
        /*[Route("Manager/FieldTrip/{id:int}")]*/
        public async Task<IActionResult> ManagerFieldTripDetail(int id)
        {
            string ManagerFieldTripDetailAPI_URL = ManagerAPI_URL + "FieldTrip/AllParticipants/" + id;
            ManagerAPI_URL += "FieldTrip/" + id;
            dynamic fieldtripDetailBigModel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");


            TempData["ROLE_NAME"] = role;

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
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Field Trip!).\n Field Trip Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Field Trip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            fieldtripDetailBigModel.FieldTripDetails = fieldtripPostResponse.Data;
            fieldtripDetailBigModel.FieldTripParticipants = fieldtrippartPostResponse.Data;
            return View(fieldtripDetailBigModel);
        }
        [HttpPost("FieldTrip/Update/{id:int}")]
        /*[Route("Manager/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerUpdateFieldTripDetail(
            int id,
            FieldTripViewModel fieldtripView
            )
        {
            ManagerAPI_URL += "FieldTrip/Update/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
                                url: ManagerAPI_URL,
                                inputType: fieldtripView,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            return RedirectToAction("ManagerFieldTripDetail", "Manager", new { id = id });
        }
        [HttpPost("FieldTrip/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> ManagerCreateFieldTrip(FieldTripViewModel fieldtripView)
        {
            ManagerAPI_URL += "FieldTrip/Create";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
                                url: ManagerAPI_URL,
                                inputType: fieldtripView,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            return RedirectToAction("ManagerFieldTrip");
        }

        [HttpPost("FieldTrip/Update/Cancel/{id:int}")]
        public async Task<IActionResult> ManagerCancelFieldTrip(
            int id)
        {
            ManagerAPI_URL += "FieldTrip/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("ManagerFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("ManagerFieldTrip");
            }
            return RedirectToAction("ManagerFieldTrip");
        }
        public IActionResult ManagerBirdContest()
        {
            return View();
        }
        public IActionResult ManagerBirdContestDetail()
        {
            return View();
        }
        public IActionResult ManagerProfile()
        {
            return View();
        }
        public IActionResult ManagerFeedBack()
        {
            return View();
        }
        public IActionResult ManagerStatical()
        {
            return View();
        }
        public IActionResult ManagerHistoryEventsDetail()
        {
            return View();
        }
        public IActionResult ManagerHistoryEvents()
        {
            return View();
        }
    }
}
