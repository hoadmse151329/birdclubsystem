using BAL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Notification;
using WebAppMVC.Services;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC.Controllers
{
    public class SharedController : Controller
    {
        private readonly ILogger<SharedController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string NotificationAPI_URL = "";
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        private BirdClubLibrary methcall = new();

        public SharedController(ILogger<SharedController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            NotificationAPI_URL = "/api/Notification";
        }
        public IActionResult Index()
        {
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var notificationCount = methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

            ViewBag.NotificationCount = notificationCount;

            return View();
        }
    }
}
