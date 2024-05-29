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
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions
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
            else TempData["NotificationMessage"] = "Logged in as Member!";

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);
                
                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: FieldTripAPI_URL_All,
                inputType: role,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

			var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
				_httpClient: _httpClient,
				options: jsonOptions,
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
        [Route("News")]
        public IActionResult News()
		{
			return View();
		}
        [Route("NewsDetail")]
        public IActionResult NewsDetail()
        {
            return View();
        }
        [Route("Gallary")]
        public IActionResult Gallary()
        {
            return View();
        }
        [Route("Blog")]
        public IActionResult Blog()
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