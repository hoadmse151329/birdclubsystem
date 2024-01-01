using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using BAL.ViewModels;
using WebAppMVC.Models.Meeting;
using System.Dynamic;
using WebAppMVC.Constants;
using WebAppMVC.Models.Location;

namespace WebAppMVC.Controllers
{
	public class MeetingController : Controller
	{
		private readonly ILogger<MeetingController> _logger;
		private readonly HttpClient _httpClient = null;
		private string MeetingAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
		private MethodCaller methcall = new();
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
            MeetingAPI_URL += "/All";
			string LocationAPI_URL_All = "/api/Location/All";
			dynamic testmodel = new ExpandoObject();

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
				_httpClient: _httpClient,
				options: options,
				methodName: "GET",
				url: MeetingAPI_URL);

			if(listMeetResponse == null || listLocationResponse == null)
			{
				ViewBag.error =
					"Error while processing your request! (Get List Meeting!).";
                Redirect("~/Home/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;
			testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
		}
		public IActionResult MeetingPost()
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
