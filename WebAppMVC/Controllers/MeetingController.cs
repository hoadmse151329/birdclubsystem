using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels;
using System.Net.Http;
using Microsoft.Extensions.Options;
using WebAppMVC.Models.Auth;
using WebAppMVC.Models.Meeting;

namespace WebAppMVC.Controllers
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
        [HttpGet]
		public async Task<IActionResult> Index()
		{
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true,
            };
            MeetingAPI_URL += "/All";
            HttpResponseMessage response = await _httpClient.GetAsync(MeetingAPI_URL);
            if (!response.IsSuccessStatusCode)
            {
                ViewBag.error = "Error while processing your request! (Get List Meeting!).";
                return Redirect("~/Home/Index");
            }
            string jsonResponse = await response.Content.ReadAsStringAsync();
            var listmeetResponse = JsonSerializer.Deserialize<GetMeetingResponseByList>(jsonResponse, options);
            var responsemeetlist = listmeetResponse.Data;
            return View(responsemeetlist);
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
	}
}
