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
using BAL.ViewModels.Member;
using WebAppMVC.Models.Auth;
using WebAppMVC.Models.Blog;
using WebAppMVC.Models.ViewModels;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using BAL.ViewModels.News;
using System.ComponentModel.DataAnnotations;
using BAL.ViewModels.Blog;
using static Org.BouncyCastle.Math.EC.ECCurve;
using static WebAppMVC.Models.ViewModels.GuestIndexVM;
using Microsoft.Extensions.Configuration;

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
            GuestIndexVM GuestIndexDetailsVM = new();

            methcall.SetUserDefaultData(this);

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);
            if(accToken == null)
            {
                var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: HomeAPI_URL + "User/CreateGuestUser",
                _logger: _logger);
                if(authenResponse == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                if (!authenResponse.Status || authenResponse.Data == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, accToken = authenResponse.Data.AccessToken);
            }
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
                accessToken: accToken,
                inputType: usrId,
                _logger: _logger);
                
                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationUnreadAPI_URL,
                accessToken: accToken,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationReadAPI_URL,
                accessToken: accToken,
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

			if (
                listMeetResponse == null || 
                listFieldTripResponse == null || 
                listContestResponse == null
                )
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n List was Empty!";
                Redirect("~/Home/Error");
            }
            else
            if (
                !listMeetResponse.Status || 
                !listFieldTripResponse.Status || 
                !listContestResponse.Status ||
                !listMeetResponse.Data.Any() ||
                !listFieldTripResponse.Data.Any() ||
                !listContestResponse.Data.Any()
                )
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listFieldTripResponse.ErrorMessage;
                Redirect("~/Home/Error");
            }
            GuestIndexDetailsVM.Banners = _config.GetSection("BannerImages").Get<List<string>>();
            GuestIndexDetailsVM.Features = _config.GetSection("Features").Get<List<Feature>>();
            GuestIndexDetailsVM.WelcomeMess = _config.GetSection("WelcomeMessage").Get<WelcomeMessage>();
            GuestIndexDetailsVM.About = _config.GetSection("aboutUs").Get<AboutUs>();
            GuestIndexDetailsVM.TeamMembers = _config.GetSection("team").Get<List<TeamMember>>();
            GuestIndexDetailsVM.Services = _config.GetSection("services").Get<List<Service>>();
            GuestIndexDetailsVM.FooterBlock = _config.GetSection("footer").Get<Footer>();
            GuestIndexDetailsVM.Meetings = listMeetResponse.Data;
            GuestIndexDetailsVM.FieldTrips = listFieldTripResponse.Data;
            GuestIndexDetailsVM.Contests = listContestResponse.Data;
            return View(GuestIndexDetailsVM);
		}
        [HttpGet("News")]
        public async Task<IActionResult> News()
		{
            methcall.SetUserDefaultData(this);
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            if (accToken == null)
            {
                var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: HomeAPI_URL + "User/CreateGuestUser",
                _logger: _logger);
                if (authenResponse == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                if (!authenResponse.Status || authenResponse.Data == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, accToken = authenResponse.Data.AccessToken);
            }

            string NewsAPI_URL = HomeAPI_URL + "News/SearchForMemberOrGuest";
            var listNewsResponse = await methcall.CallMethodReturnObject<GetListNews>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                accessToken: accToken,
                url: NewsAPI_URL,
                _logger: _logger);
            if (listNewsResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List News!).\n List was Empty!";
                RedirectToAction("Index");
            }
            if (!listNewsResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List News!).\n" + 
                    listNewsResponse.ErrorMessage;
                RedirectToAction("Index");
            }
            return View(listNewsResponse.Data);
		}
        [HttpGet("News/{newsId:int}")]
        public async Task<IActionResult> NewsDetail(
            [Required][FromRoute]int newsId
            )
        {
            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            if (accToken == null)
            {
                var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: HomeAPI_URL + "User/CreateGuestUser",
                _logger: _logger);
                if (authenResponse == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                if (!authenResponse.Status || authenResponse.Data == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, accToken = authenResponse.Data.AccessToken);
            }
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
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting News!).\n News was empty or not found!";
                RedirectToAction("News");
            }
            if (!newsResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting News!).\n" + newsResponse.ErrorMessage;
                RedirectToAction("News");
            }
            return View(newsResponse.Data);
        }
        [Route("Gallery")]
        public IActionResult Gallery()
        {
            methcall.SetUserDefaultData(this);
            return View();
        }
        [HttpGet("Blog")]
        public async Task<IActionResult> Blog()
        {
            methcall.SetUserDefaultData(this);
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            if (accToken == null)
            {
                var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: HomeAPI_URL + "User/CreateGuestUser",
                _logger: _logger);
                if (authenResponse == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                if (!authenResponse.Status || authenResponse.Data == null)
                {
                    _logger.LogError("Error while processing your request! (Create Guest User)");
                }
                HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, accToken = authenResponse.Data.AccessToken);
            }
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);
            MemberBlogIndexVM memberBlogIndexVM = new();

            string BlogAPI_URL = HomeAPI_URL + "Blog/SearchForMemberOrGuest";

            var listBlogResponse = await methcall.CallMethodReturnObject<GetListBlogResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                accessToken: accToken,
                url: BlogAPI_URL,
                _logger: _logger);
            if (listBlogResponse == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List Blogs!).\n List was Empty!";
                RedirectToAction("Index");
            }
            if (!listBlogResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while processing your request! (Getting List Blogs!).\n" +
                    listBlogResponse.ErrorMessage;
                RedirectToAction("Index");
            }
            memberBlogIndexVM.Blogs = listBlogResponse.Data;
            if (role.Equals(Constants.Constants.MEMBER))
            {
                memberBlogIndexVM.isGuest = false;
                memberBlogIndexVM.createBlog = methcall.GetValidationTempData<CreateNewBlog>(this, TempData, Constants.Constants.CREATE_BLOG_VALID, "createBlog", jsonOptions);
                if (memberBlogIndexVM.createBlog == null)
                {
                    memberBlogIndexVM.createBlog = new CreateNewBlog()
                    {
                        Fullname = HttpContext.Session.GetString(Constants.Constants.USR_NAME),
                        MemberAvatar = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE)
                    };
                }
            }
            return View(memberBlogIndexVM);
        }
        [HttpPost("Blog/Create")]
        /*[Route("Manager/Meeting/Update/{id:int}")]*/
        public async Task<IActionResult> MemberCreateBlog([Required] CreateNewBlog createBlog)
        {
            HomeAPI_URL += "Blog/Create";
            if(createBlog.Status == null)
            {
                createBlog.Status = "Draft";
            }
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_BLOG_VALID, createBlog, jsonOptions);
                return RedirectToAction("Blog");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            createBlog.MemberId = usrId;

            IFormFile photo = createBlog.ImageUpload;

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_CONNECTION_STRING);
                string defaultUrl = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_URL);
                string containerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_NAME);
                string blogContainerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_BLOB_BLOG_FOLDER_URL);

                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = blogContainerName + $"{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = defaultUrl + uniqueBlobName;

                createBlog.Image = image;
            }

            createBlog.ImageUpload = null;

            var memberCreateBlogPostVM = await methcall.CallMethodReturnObject<GetBlogPostResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: HomeAPI_URL,
                                inputType: createBlog,
                                accessToken: accToken,
                                _logger: _logger);
            if (memberCreateBlogPostVM == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Create Blog!).\n Blog Not Found!";
                return RedirectToAction("Blog");
            }
            if (!memberCreateBlogPostVM.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberCreateBlogPostVM.Status + " , Error Message: " + memberCreateBlogPostVM.ErrorMessage);
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Create Blog Post!).\n"
                    + memberCreateBlogPostVM.ErrorMessage;
                return RedirectToAction("Blog");
            }
            TempData[Constants.Constants.ALERT_DEFAULT_SUCCESS_NAME] = Constants.Constants.ALERT_MANAGER_CREATE_NEWS_SUCCESS;
            return RedirectToAction("Blog");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}