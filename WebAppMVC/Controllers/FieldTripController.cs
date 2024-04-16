﻿using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAppMVC.Constants;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC.Controllers
{
    [Route("FieldTrip")]
    public class FieldTripController : Controller
	{
		private readonly ILogger<FieldTripController> _logger;
        private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private string FieldTripAPI_URL = "";
		private readonly JsonSerializerOptions options = new JsonSerializerOptions
		{
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
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

		[HttpGet("Index")]
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

            var listLocationRoadResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All_Road,
                _logger: _logger);
            var listLocationDistrictResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All_District,
                _logger: _logger);
            var listLocationCityResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
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

            List<SelectListItem> roads = new();
            foreach (var road in listLocationRoadResponse.Data)
            {
                roads.Add(new SelectListItem(text: road, value: road));
            }
            testmodel.Roads = roads;

            List<SelectListItem> districts = new();
            foreach (var district in listLocationDistrictResponse.Data)
            {
                districts.Add(new SelectListItem(text: district, value: district));
            }
            testmodel.Districts = districts;

            List<SelectListItem> cities = new();
            foreach (var city in listLocationCityResponse.Data)
            {
                cities.Add(new SelectListItem(text: city, value: city));
            }
            testmodel.Cities = cities;

            testmodel.FieldTrips = listTripResponse.Data;
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
        [HttpGet("FieldTripPost/{id:int}")]
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

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
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

        [HttpPost("FieldTripRegister/{tripId:int}")]
        public async Task<IActionResult> FieldTripRegister(int tripId)
        {
            FieldTripAPI_URL += "/Register/" + tripId;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var participationNo = await methcall.CallMethodReturnObject<GetFieldTripParticipationNo>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: FieldTripAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Registering Field Trip Participation!): Field Trip Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Field Trip Participation!).\n Field Trip Not Found!";
                RedirectToAction("FieldTripPost", new { id = tripId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Field Trip Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Field Trip Participation!).\n"
                    + participationNo.ErrorMessage;
                RedirectToAction("FieldTripPost", new { id = tripId });
            }

            return RedirectToAction("FieldTripPost", new { id = tripId });
        }
        [HttpPost("FieldTripDeRegister/{tripId:int}")]
        public async Task<IActionResult> FieldTripDeRegister(int tripId)
        {
            FieldTripAPI_URL += "/RemoveParticipant/" + tripId;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var participationNo = await methcall.CallMethodReturnObject<GetFieldTripPostDeRegister>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: FieldTripAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Remove Field Trip Participation Registration!): Field Trip Participation Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Remove Field Trip Participation Registration!).\n Field Trip Participation Not Found!";
                RedirectToAction("FieldTripPost", new { id = tripId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Remove Field Trip Participation Registration!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Remove Field Trip Participation Registration!).\n"
                    + participationNo.ErrorMessage;
                RedirectToAction("FieldTripPost", new { id = tripId });
            }

            return RedirectToAction("FieldTripPost", new { id = tripId });
        }
    }
}
