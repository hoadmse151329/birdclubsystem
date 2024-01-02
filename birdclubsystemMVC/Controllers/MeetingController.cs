using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels;
using System.Net.Http;
using DAL.Models;
using BAL.ViewModels.Meeting;
namespace birdclubsystem.Controllers
{
    public class MeetingController : Controller
    {
		private readonly ILogger<MeetingController> _logger;
        private readonly HttpClient _httpClient = null;
        private string MeetingAPI_URL = "";
        
        public MeetingController(ILogger<MeetingController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
            MeetingAPI_URL = "/api/Meeting";
        }
		public IActionResult Index()
        {
            return View();
        }
        public IActionResult MeetingPost()
        {
            return View();
        }
        public IActionResult MeetingRegister()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> In()
        {
            // Call the API endpoint
            HttpResponseMessage response = await _httpClient.GetAsync("1"); // Replace '1' with the actual meeting ID
            if (response.IsSuccessStatusCode)
            {
                var meeting = await response.Content.ReadAsAsync<MeetingViewModel>();
                return View(meeting);
            }
            else
            {
                // Handle error
                _logger.LogError($"API request failed with status code {response.StatusCode}");
                return View("Error");
            }
        }
        [HttpPost]
        public async Task<IActionResult> RegisterMeeting(RegisterMeeting register)
        {
            var _register = new RegisterMeeting
            {
                UserName = register.UserName,
                FullName = register.FullName,
                Gender = register.Gender,
                Email = register.Email,
                PhoneNumber = register.PhoneNumber,
            };
            if ((register.UserName != null) && (register.FullName != null) && (register.Gender != null) && (register.Email != null) && (register.PhoneNumber != null))
            {
                return RedirectToAction("MeetingPost");
            }
            else
            {
                return View(_register);
            }
        }
    }
}
