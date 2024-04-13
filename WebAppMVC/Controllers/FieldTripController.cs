using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAppMVC.Constants;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC.Controllers
{
	public class FieldTripController : Controller
	{
		private readonly ILogger<FieldTripController> _logger;
        private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private string FieldTripAPI_URL = "";
		private readonly JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true,
		};
		private MethodCaller methcall = new();
		public FieldTripController(ILogger<FieldTripController> logger, IConfiguration config)
		{
			_logger = logger;
            _config = config;
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
			FieldTripAPI_URL = "/api/FieldTrip";
        }

		[HttpGet]
		[Route("FieldTrip/Index")]
		public async Task<IActionResult> Index()
		{
			FieldTripAPI_URL += "/All";
            string LocationAPI_URL_All_Road = "/api/Location/AllAddressRoads";
            string LocationAPI_URL_All_District = "/api/Location/AllAddressDistricts";
            string LocationAPI_URL_All_City = "/api/Location/AllAddressCities";
            dynamic testmodel = new ExpandoObject();

            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var listLocationRoadResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All_Road,
                _logger: _logger);
            var listLocationDistrictResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All_District,
                _logger: _logger);
            var listLocationCityResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All_City,
                _logger: _logger);

            var listTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: FieldTripAPI_URL,
                _logger: _logger);

			if (listTripResponse == null || listLocationRoadResponse == null || listLocationDistrictResponse == null || listLocationCityResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Field Trip!). List was Empty!: " + listTripResponse + " , Error Message: " + listTripResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Field Trip!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listTripResponse.Status || !listLocationRoadResponse.Status || !listLocationDistrictResponse.Status || !listLocationCityResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Field Trip!).\n"
                    + listTripResponse.ErrorMessage + "\n" + listLocationRoadResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.FieldTrips = listTripResponse.Data;
            testmodel.Roads = listLocationRoadResponse.Data;
            testmodel.Districts = listLocationDistrictResponse.Data;
            testmodel.Cities = listLocationCityResponse.Data;
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

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

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
