using Microsoft.AspNetCore.Mvc;
using BAL.ViewModels;
using Microsoft.AspNetCore.Http;
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
namespace WebAppMVC.Controllers
{
	[Route("Admin")]
	public class AdminController : Controller
	{
        private readonly ILogger<MeetingController> _logger;
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

        public AdminController(ILogger<MeetingController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            AdminAPI_URL = "/api/";
        }

        // GET: ManagerController
        [HttpGet("Index")]
        public IActionResult AdminIndex()
		{
			return View();
		}
	}
}
