using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using BAL.ViewModels;
using WebAppMVC.Models.Meeting;
using System.Dynamic;
using WebAppMVC.Constants;
using WebAppMVC.Models.Location;
using BAL.Services.Interfaces;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using static Org.BouncyCastle.Math.EC.ECCurve;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text.Encodings.Web;

namespace WebAppMVC.Controllers
{
    [Route("Meeting")]
	public class MeetingController : Controller
	{
		private readonly ILogger<MeetingController> _logger;
        private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private string MeetingAPI_URL = "";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
		private BirdClubLibrary methcall = new();
        public MeetingController(ILogger<MeetingController> logger, IConfiguration config)
		{
			_logger = logger;
            _config = config;
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			_httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
			MeetingAPI_URL = "/api/Meeting";
		}

		[HttpGet("Index")]
		public async Task<IActionResult> Index(
            [FromQuery] string meetingName, 
            [FromQuery] string locationAddress)
		{
            if (string.IsNullOrEmpty(meetingName) && string.IsNullOrEmpty(locationAddress)) MeetingAPI_URL += "/All";
            else MeetingAPI_URL += "/Search?";

            _logger.LogInformation(locationAddress);

            if (!string.IsNullOrEmpty(meetingName))
            {
                meetingName = meetingName.Trim();
                MeetingAPI_URL += "meetingName=" + meetingName + "&";
            }
            if (!string.IsNullOrEmpty(locationAddress))
            {
                locationAddress = locationAddress.Trim();
                MeetingAPI_URL += "locationAddress=" + locationAddress + "&";
            }
            if(MeetingAPI_URL.Contains("Search")) MeetingAPI_URL.Substring(0,MeetingAPI_URL.Length - 1);
            
            string LocationAPI_URL_All_Road = "/api/Location/AllAddressRoads";
            string LocationAPI_URL_All_District = "/api/Location/AllAddressDistricts";
            string LocationAPI_URL_All_City = "/api/Location/AllAddressCities";
            dynamic testmodel = new ExpandoObject();

            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

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


            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
				_httpClient: _httpClient,
				options: options,
				methodName: "GET",
				url: MeetingAPI_URL,
                _logger: _logger);

			if(listMeetResponse == null || listLocationRoadResponse == null || listLocationDistrictResponse == null || listLocationCityResponse == null)
			{
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse + " , Error Message: " + listMeetResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
			else
			if(!listMeetResponse.Status || !listLocationRoadResponse.Status || !listLocationDistrictResponse.Status || !listLocationCityResponse.Status)
			{
				ViewBag.error =
					"Error while processing your request! (Getting List Meeting!).\n"
					+ listMeetResponse.ErrorMessage + "\n" + listLocationRoadResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;

            List<SelectListItem> roads = new();
            foreach(var road in listLocationRoadResponse.Data)
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

            return View(testmodel);
		}


		[HttpGet("MeetingPost/{id:int}")]
		public async Task<IActionResult> MeetingPost(int id)
		{
			MeetingAPI_URL += "/";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            GetMeetingPostResponse? meetPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
			{
                MeetingAPI_URL += "Participant/" + id;
                meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: "POST",
                                   url: MeetingAPI_URL,
                                   _logger: _logger,
                                   inputType: usrId,
                                   accessToken: accToken);
            }
			else
			{
                MeetingAPI_URL += id;
                meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: "GET",
                                   url: MeetingAPI_URL,
                                   _logger: _logger);
            }
            if (meetPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                View("Index");
            }

            var meetmodel = meetPostResponse.Data;
            if (!meetPostResponse.Status)
			{
                _logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
					"Error while processing your request! (Getting Meeting Post!).\n"
					+ meetPostResponse.ErrorMessage;
                View("Index");
			}
			/*if(TempData["PartakeNo"] != null)
				ViewBag.PartNumber = Int32.Parse(TempData["PartakeNo"].ToString());*/
            return View(meetmodel);
		}

		[HttpPost("MeetingRegister/{meetingId:int}")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MeetingRegister(int meetingId)
		{
            MeetingAPI_URL += "/Register/" + meetingId;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingParticipationNo>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL,
                _logger: _logger,
                inputType: usrId,
				accessToken: accToken);
			if(participationNo == null)
			{
                _logger.LogInformation("Error while processing your request! (Registering Meeting Participation!): Meeting Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n Meeting Not Found!";
                RedirectToAction("MeetingPost", new { id = meetingId });
            }else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Meeting Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n"
					+ participationNo.ErrorMessage;
                RedirectToAction("MeetingPost", new { id = meetingId });
            }

            return RedirectToAction("MeetingPost", new { id = meetingId });
        }
        [HttpPost("MeetingDeRegister/{meetingId:int}")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MeetingDeRegister(int meetingId)
        {
            MeetingAPI_URL += "/RemoveParticipant/" + meetingId;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            string? imagepath = HttpContext.Session.GetString("IMAGE_PATH");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;
            TempData["IMAGE_PATH"] = imagepath;

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingPostDeRegister>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Remove Meeting Participation Registration!): Meeting Participation Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Remove Meeting Participation Registration!).\n Meeting Participation Not Found!";
                RedirectToAction("MeetingPost", new { id = meetingId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Remove Meeting Participation Registration!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Remove Meeting Participation Registration!).\n"
                    + participationNo.ErrorMessage;
                RedirectToAction("MeetingPost", new { id = meetingId });
            }

            return RedirectToAction("MemberHistoryEvent","Member");
        }
    }
}
