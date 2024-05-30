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
using WebAppMVC.Models.News;
using Org.BouncyCastle.Ocsp;

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
            _httpClient.BaseAddress = new Uri(config.GetValue<string>("DefaultApiUrl:ConnectionString"));
            HomeAPI_URL = config.GetValue<string>("DefaultApiUrl:ApiConnectionString");
        }
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            string MeetingAPI_URL = HomeAPI_URL + "Meeting/All";
            string FieldTripAPI_URL_All = HomeAPI_URL + "FieldTrip/All";
            string ContestAPI_URL_All = HomeAPI_URL + "Contest/All";
            dynamic testmodel = new ExpandoObject();
            /*if(_httpClient.DefaultRequestHeaders.Authorization != null)
            {
                var token = _httpClient.DefaultRequestHeaders.Authorization.Parameter;
            }*/
            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            if(string.IsNullOrWhiteSpace(role) || string.IsNullOrEmpty(role))
            {
                role = Constants.Constants.GUEST;
                HttpContext.Session.SetString(Constants.Constants.ROLE_NAME, role);
            }
            else if (role.Equals(Constants.Constants.MEMBER))
                TempData[Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS_NAME] = Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS;

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = HomeAPI_URL + "Notification/Count";
                string NotificationUnreadAPI_URL = HomeAPI_URL + "Notification/Unread";
                string NotificationReadAPI_URL = HomeAPI_URL + "Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);
                
                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
                methodName: Constants.Constants.POST_METHOD,
                url: FieldTripAPI_URL_All,
                inputType: role,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

			var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
				_httpClient: _httpClient,
				options: jsonOptions,
				methodName: Constants.Constants.POST_METHOD,
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
        [HttpGet("News")]
        public async Task<IActionResult> News()
		{
            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrEmpty(role))
            {
                role = Constants.Constants.GUEST;
                HttpContext.Session.SetString(Constants.Constants.ROLE_NAME, role);
            }
            else if (role.Equals(Constants.Constants.MEMBER))
                TempData[Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS_NAME] = Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS;

            string NewsAPI_URL = HomeAPI_URL + "News/Search";
            var listNewsResponse = await methcall.CallMethodReturnObject<GetListNews>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NewsAPI_URL,
                inputType: role,
                _logger: _logger);
            if (listNewsResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List News!).\n List was Empty!";
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List News!).\n List was Empty!";
                Redirect("Index");
            }
            if (!listNewsResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List News!).\n"
                    + listNewsResponse.ErrorMessage;
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List News!).\n" + listNewsResponse.ErrorMessage;
                Redirect("Index");
            }
            return View(listNewsResponse.Data);
		}
        [HttpGet("News/{newsId:int}")]
        public async Task<IActionResult> NewsDetail(int newsId)
        {
            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            if (string.IsNullOrWhiteSpace(role) || string.IsNullOrEmpty(role))
            {
                role = Constants.Constants.GUEST;
                HttpContext.Session.SetString(Constants.Constants.ROLE_NAME, role);
            }
            else if (role.Equals(Constants.Constants.MEMBER))
                TempData[Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS_NAME] = Constants.Constants.ALERT_MEMBER_LOGIN_SUCCESS;

            string NewsAPI_URL = HomeAPI_URL + "News/" + newsId;
            var newsResponse = await methcall.CallMethodReturnObject<GetNewsPostResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NewsAPI_URL,
                inputType: role,
                _logger: _logger);
            if (newsResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting News!).\n News was empty or not found!";
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting News!).\n News was empty or not found!";
                RedirectToAction("Index");
            }
            if (!newsResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting News!).\n"
                    + newsResponse.ErrorMessage;
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting News!).\n" + newsResponse.ErrorMessage;
                Redirect("Index");
            }
            return View(newsResponse.Data);
        }
        [Route("Gallery")]
        public IActionResult Gallery()
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