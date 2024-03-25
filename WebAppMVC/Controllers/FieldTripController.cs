using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAppMVC.Constants;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;

namespace WebAppMVC.Controllers
{
	public class FieldTripController : Controller
	{
		private readonly ILogger<FieldTripController> _logger;
		private readonly HttpClient _httpClient = null;
		private string FieldTripAPI_URL = "";
		private readonly JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		};
		private MethodCaller methcall = new();
		public FieldTripController(ILogger<FieldTripController> logger)
		{
			_logger = logger;
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
			FieldTripAPI_URL = "/api/FieldTrip";
        }

		[HttpGet]
		[Route("FieldTrip/Index")]
		public async Task<IActionResult> Index()
		{
			FieldTripAPI_URL += "/All";
			string LocationAPI_URL_All = "/api/Location/All";
            dynamic testmodel = new ExpandoObject();
            TempData["ROLE_NAME"] = HttpContext.Session.GetString("ROLE_NAME");

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: FieldTripAPI_URL,
                _logger: _logger);

			if (listTripResponse == null || listLocationResponse == null)
			{
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Field Trip!). List was Empty!: " + listTripResponse + " , Error Message: " + listTripResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Field Trip!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listTripResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Field Trip!).\n"
                    + listTripResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.FieldTrips = listTripResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
        }
		public IActionResult FieldTripRegister()
		{
			return View();
		}
		public IActionResult FieldTripPayment()
		{
			return View();
		}
        [HttpGet("{id:int}")]
        [Route("FieldTrip/FieldTripPost/{id:int}")]
        public async Task<IActionResult> FieldTripPost(int id)
		{
            FieldTripAPI_URL += "/";
            string? role = HttpContext.Session.GetString("ROLE_NAME");
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? usrId = HttpContext.Session.GetString("USER_ID");

            TempData["ROLE_NAME"] = role;

            GetFieldTripPostResponse? fieldtripPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId))
            {
                FieldTripAPI_URL += "Participant/" + id;
                fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: "POST",
                                   url: FieldTripAPI_URL,
                                   _logger: _logger,
                                   inputType: usrId,
                                   accessToken: accToken);
            }
            else
            {
                FieldTripAPI_URL += id;
                fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: "GET",
                                   url: FieldTripAPI_URL,
                                   _logger: _logger);
            }
            if (fieldtripPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                View("Index");
            }

            var fieldtripmodel = fieldtripPostResponse.Data;
            if (!fieldtripPostResponse.Status)
            {
                _logger.LogInformation("Username or Password is invalid: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                View("Index");
            }
            return View(fieldtripmodel);
        }
		public IActionResult FieldTripPostGettingThere()
		{
			return View();
		}
		public IActionResult FieldTripPostInclusion()
		{
			return View();
		}
		public IActionResult FieldTripPostRate()
		{
			return View();
		}
	}
}
