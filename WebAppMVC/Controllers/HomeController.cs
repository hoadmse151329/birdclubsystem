using WebAppMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;
using WebAppMVC.Constants;
using System.Net.Http.Headers;
using System.Dynamic;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Contest;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Http.Json;
using System;
using WebAppMVC.Models.Notification;
using BAL.ViewModels.Event;

namespace WebAppMVC.Controllers
{
	public class HomeController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string HomeAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private BirdClubLibrary methcall = new();

        public HomeController(ILogger<HomeController> logger, IConfiguration config)
		{
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            HomeAPI_URL = config.GetSection("DefaultApiUrl:ApiConnectionString").Value;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            string MeetingAPI_URL = HomeAPI_URL + "Meeting/All";
            string FieldTripAPI_URL_All = HomeAPI_URL + "FieldTrip/All";
            string ContestAPI_URL_All = HomeAPI_URL + "Contest/All";
            dynamic testmodel = new ExpandoObject();
            if(_httpClient.DefaultRequestHeaders.Authorization != null)
            {
                var token = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
            }
            /*string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Logout", "Home");
            
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");*/
            string? usrId = HttpContext.Session.GetString("USER_ID");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (role == null) role = "Guest";

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationTitleAPI_URL = "/api/Notification/Unread";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);
                
                var notificationTitle = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationTitleAPI_URL,
                inputType: usrId,
                _logger: _logger);
                
                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationTitle = notificationTitle.Data.ToList();
            }

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: FieldTripAPI_URL_All,
                inputType: role,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

			var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
				_httpClient: _httpClient,
				options: options,
				methodName: "POST",
				url: ContestAPI_URL_All,
                inputType: role,
				_logger: _logger);

			if (listMeetResponse == null || listFieldTripResponse == null || listContestResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n List was Empty!";
                Redirect("~/Home/Error");
            }
            else
            if (!listMeetResponse.Status || !listFieldTripResponse.Status || !listContestResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listFieldTripResponse.ErrorMessage;
                Redirect("~/Home/Error");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.FieldTrips = listFieldTripResponse.Data;
            testmodel.Contests = listContestResponse.Data;
            return View(testmodel);
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}