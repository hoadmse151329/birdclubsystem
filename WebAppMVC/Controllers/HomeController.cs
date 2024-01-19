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

namespace WebAppMVC.Controllers
{
	public class HomeController : Controller
	{
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient _httpClient = null;
        private string HomeAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private MethodCaller methcall = new();

        public HomeController(ILogger<HomeController> logger)
		{
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
            HomeAPI_URL = "/api/";
        }
        [HttpGet]
        public async Task<IActionResult> Index()
		{
            string MeetingAPI_URL = HomeAPI_URL + "Meeting/All";
            string FieldTripAPI_URL_All = HomeAPI_URL + "FieldTrip/All";
            dynamic testmodel = new ExpandoObject();
            TempData["ROLE_NAME"] = HttpContext.Session.GetString("ROLE_NAME");

            var listFieldTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: FieldTripAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: MeetingAPI_URL,
                _logger: _logger);

            if (listMeetResponse == null || listFieldTripResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n List was Empty!";
                Redirect("~/Home/Error");
            }
            else
            if (!listMeetResponse.Status || !listFieldTripResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting Or Fieldtrip!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listFieldTripResponse.ErrorMessage;
                Redirect("~/Home/Error");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.FieldTrips = listFieldTripResponse.Data;
            return View(testmodel);
		}

		public IActionResult Privacy()
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