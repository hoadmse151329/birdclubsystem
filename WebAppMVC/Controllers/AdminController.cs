using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;

namespace WebAppMVC.Controllers
{
	[Route("Admin")]
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;
		private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private string AdminAPI_URL = "";
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

        public AdminController(ILogger<AdminController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            AdminAPI_URL = "/api/";
        }

        [HttpGet("Index")]
        public async Task<IActionResult> AdminIndex()
		{
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));
            return View();
        }

        [HttpGet("List")]
        public async Task<IActionResult> AdminList()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));
            return View();
        }

        [HttpGet("Profile")]
        public async Task<IActionResult> AdminProfile()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));
            return View();
        }
	}
}
