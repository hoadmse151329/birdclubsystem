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
namespace WebAppMVC.Controllers
{    
	[Route("Staff")]
	public class StaffController : Controller
	{
		private readonly ILogger<MeetingController> _logger;
		private readonly HttpClient _httpClient = null;
		private string StaffAPI_URL = "";
		private readonly JsonSerializerOptions options = new JsonSerializerOptions
		{
			Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
			PropertyNameCaseInsensitive = true
		};
		private BirdClubLibrary methcall = new();

		public StaffController(ILogger<MeetingController> logger)
		{
			_logger = logger;
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			_httpClient.BaseAddress = new Uri("https://localhost:7022");
			StaffAPI_URL = "/api/";
		}

		// GET: StaffController
		[HttpGet("Index")]
		public IActionResult StaffIndex()
		{
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

			TempData["ROLE_NAME"] = role;
			TempData["USER_NAME"] = usrname;

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
				url: StaffAPI_URL,
				_logger: _logger);

			if (listMeetResponse == null || listLocationResponse == null)
			{
				_logger.LogInformation(
					"Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse);
				ViewBag.error =
					"Error while processing your request! (Getting List Meeting!).\n List was Empty!";
				return View("StaffIndex");
			}
			else
			if (!listMeetResponse.Status || !listLocationResponse.Status)
			{
				ViewBag.error =
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

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                _logger: _logger);
            var meetpartPostResponse = await methcall.CallMethodReturnObject<GetListMeetingParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffMeetingDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeeting");
            }
            meetingDetailBigModel.MeetingDetails = meetPostResponse.Data;
            meetingDetailBigModel.MeetingParticipants = meetpartPostResponse.Data;
            return View(meetingDetailBigModel);
        }
        [HttpPost("Meeting/Update/{id:int}")]
        /*[Route("Staff/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> StaffUpdateMeetingDetail(
            int id,
            MeetingViewModel meetView
            )
        {
            StaffAPI_URL += "Meeting/Update/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
                                url: StaffAPI_URL,
                                inputType: meetView,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeeting");
            }
            return RedirectToAction("StaffMeetingDetail", "Staff", new { id = id });
        }
        [HttpPost("Meeting/Create")]
        /*[Route("Staff/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> StaffCreateMeeting(MeetingViewModel meetView)
        {
            StaffAPI_URL += "Meeting/Create";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
                                url: StaffAPI_URL,
                                inputType: meetView,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Create Meeting!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeeting");
            }
            return RedirectToAction("StaffMeeting");
        }

        [HttpPost("Meeting/Update/Cancel/{id:int}")]
        public async Task<IActionResult> StaffCancelMeeting(
            int id)
        {
            StaffAPI_URL += "Meeting/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting!).\n Meeting Not Found!";
                return RedirectToAction("StaffMeeting");
            }
            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                return RedirectToAction("StaffMeeting");
            }
            return RedirectToAction("StaffMeeting");
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

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
                url: StaffAPI_URL,
                _logger: _logger);

            if (listFieldTripResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List FieldTrip!). List was Empty!: " + listFieldTripResponse);
                ViewBag.error =
                    "Error while processing your request! (Getting List FieldTrip!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!listFieldTripResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                _logger: _logger);
            var fieldtrippartPostResponse = await methcall.CallMethodReturnObject<GetListFieldTripParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffFieldTripDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("StaffFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTrip");
            }
            fieldtripDetailBigModel.FieldTripDetails = fieldtripPostResponse.Data;
            fieldtripDetailBigModel.FieldTripParticipants = fieldtripPostResponse.Data;
            return View(fieldtripDetailBigModel);
        }
        [HttpPost("FieldTrip/Update/{id:int}")]
        /*[Route("Staff/FieldTrip/Update/{id:int}")]*/
        public async Task<IActionResult> StaffUpdateFieldTripDetail(
            int id,
            FieldTripViewModel fieldtripView
            )
        {
            StaffAPI_URL += "FieldTrip/Update/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
                                url: StaffAPI_URL,
                                inputType: fieldtripView,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("StaffFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTrip");
            }
            return RedirectToAction("StaffFieldTripDetail", "Staff", new { id = id });
        }
        [HttpPost("FieldTrip/Create")]
        /*[Route("Staff/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> StaffCreateFieldTrip(FieldTripViewModel fieldtripView)
        {
            StaffAPI_URL += "FieldTrip/Create";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
                                url: StaffAPI_URL,
                                inputType: fieldtripView,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Create FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("StaffFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Create Meeting Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTrip");
            }
            return RedirectToAction("StaffFieldTrip");
        }

        [HttpPost("FieldTrip/Update/Cancel/{id:int}")]
        public async Task<IActionResult> StaffCancelFieldTrip(
            int id)
        {
            StaffAPI_URL += "FieldTrip/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip!).\n Meeting Not Found!";
                return RedirectToAction("StaffFieldTrip");
            }
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("StaffFieldTrip");
            }
            return RedirectToAction("StaffFieldTrip");
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: StaffAPI_URL,
                _logger: _logger);

            if (listContestResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse);
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                return View("StaffIndex");
            }
            else
            if (!listContestResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
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
        public async Task<IActionResult> StaffContestDetail(int id)
        {
            string StaffContestDetailAPI_URL = StaffAPI_URL + "Contest/AllParticipants/" + id;
            StaffAPI_URL += "Contest/" + id;
            dynamic contestDetailBigModel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                _logger: _logger);
            var contestpartPostResponse = await methcall.CallMethodReturnObject<GetListContestParticipation>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffContestDetailAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return RedirectToAction("StaffContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContest");
            }
            contestDetailBigModel.ContestDetails = contestPostResponse.Data;
            contestDetailBigModel.ContestParticipants = contestpartPostResponse.Data;
            return View(contestDetailBigModel);
        }
        [HttpPost("Contest/Update/{id:int}")]
        /*[Route("Staff/Contest/Update/{id:int}")]*/
        public async Task<IActionResult> StaffUpdateContestDetail(
            int id,
            ContestViewModel meetView
            )
        {
            StaffAPI_URL += "Contest/Update/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "PUT",
                                url: StaffAPI_URL,
                                inputType: meetView,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Contest!).\n Contest Not Found!";
                return RedirectToAction("StaffContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContest");
            }
            return RedirectToAction("StaffContestDetail", "Staff", new { id = id });
        }
        [HttpPost("Contest/Create")]
        /*[Route("Staff/Contest/Update/{id:int}")]*/
        public async Task<IActionResult> StaffCreateContest(ContestViewModel contestView)
        {
            StaffAPI_URL += "Contest/Create";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "POST",
                                url: StaffAPI_URL,
                                inputType: contestView,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Create Contest!).\n Contest Not Found!";
                return RedirectToAction("StaffContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Create Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContest");
            }
            return RedirectToAction("StaffContest");
        }

        [HttpPost("Contest/Update/Cancel/{id:int}")]
        public async Task<IActionResult> StaffCancelContest(
            int id)
        {
            StaffAPI_URL += "Contest/Update/Cancel/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Staff")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: StaffAPI_URL,
                                accessToken: accToken,
                                _logger: _logger);
            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Contest!).\n Contest Not Found!";
                return RedirectToAction("StaffContest");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Updating Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("StaffContest");
            }
            return RedirectToAction("StaffContest");
        }
        [HttpGet("ContestPoints")]
        public IActionResult StaffContestPoints()
		{
			return View();
		}
        [HttpGet("Profile")]
        public IActionResult StaffProfile()
		{
			return View();
		}
	}
}
