using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels;
using System.Net.Http;
namespace birdclubsystem.Controllers
{
    public class MeetingController : Controller
    {
		private readonly ILogger<MeetingController> _logger;
        private readonly HttpClient _httpClient;
        private string MeetingAPI_URL = "";
        public MeetingController(ILogger<MeetingController> logger, IHttpClientFactory httpClientFactory)
        {
            _logger = logger;
            _httpClient = httpClientFactory.CreateClient();
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
    }
}
