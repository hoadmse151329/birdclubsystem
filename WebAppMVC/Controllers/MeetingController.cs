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
using System.ComponentModel.DataAnnotations;
using System;
using WebAppMVC.Models.Notification;

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
        /*private async Task<bool> UpdateMeetingStatus(int? meetingId, string newStatus)
        {
            // Prepare the API URL
            string apiUrl = $"{_config["DefaultApiUrl:ConnectionString"]}/api/Meeting/{meetingId}/{newStatus}";

            // Prepare the HTTP client
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(_config["DefaultApiUrl:ConnectionString"]);

            // Send the HTTP request
            HttpResponseMessage response = await httpClient.PutAsync(apiUrl, null);

            // Check the response status code
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
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
                MeetingAPI_URL += $"meetingName={Uri.EscapeDataString(meetingName)}&";
            }
            if (!string.IsNullOrEmpty(locationAddress))
            {
                locationAddress = locationAddress.Trim();
                MeetingAPI_URL += $"locationAddress={Uri.EscapeDataString(locationAddress)}&";
            }
            if (MeetingAPI_URL.Contains("Search"))
            {
                MeetingAPI_URL = MeetingAPI_URL.Substring(0, MeetingAPI_URL.Length - 1); // Remove the trailing '&'
            }

            string LocationAPI_URL_All_Road = "/api/Location/AllAddressRoads";
            string LocationAPI_URL_All_District = "/api/Location/AllAddressDistricts";
            string LocationAPI_URL_All_City = "/api/Location/AllAddressCities";
            dynamic testmodel = new ExpandoObject();

            methcall.SetUserDefaultData(this);

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string NotificationAPI_URL = "/api/Notification/Count";

            if (usrId != null)
            {
                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.Data;
            }

            var listLocationRoadResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_Road,
                _logger: _logger);
            var listLocationDistrictResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_District,
                _logger: _logger);
            var listLocationCityResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_City,
                _logger: _logger);


            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
				_httpClient: _httpClient,
				options: options,
				methodName: Constants.Constants.POST_METHOD,
				url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listMeetResponse == null || listLocationRoadResponse == null || listLocationDistrictResponse == null || listLocationCityResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse + " , Error Message: " + listMeetResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listMeetResponse.Status || !listLocationRoadResponse.Status || !listLocationDistrictResponse.Status || !listLocationCityResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationRoadResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            else
            {
                
        foreach (var meeting in listMeetResponse.Data)
        {
            if (meeting.EndDate < DateTime.Now && meeting.Status == "open registration")
            {
                // If the current date and time is after the end date of the meeting and its status is 'open registration',
                // update the status to 'end registration'
                //bool success = await UpdateMeetingStatus(meeting.MeetingId, "end registration");

                //if (!success)
                //{
                //    ViewBag.error = "Error while updating meeting status.";
                //    return View("Index", testmodel);
                //}
            }
        }
    }
            testmodel.Meetings = listMeetResponse.Data;

            List<SelectListItem> roads = new();
            foreach(var road in listLocationRoadResponse.Data)
            {
                roads.Add(new SelectListItem(text: road, value: road));
            }

            List<SelectListItem> districts = new();
            foreach (var district in listLocationDistrictResponse.Data)
            {
                districts.Add(new SelectListItem(text: district, value: district));
            }

            List<SelectListItem> cities = new();
            foreach (var city in listLocationCityResponse.Data)
            {
                cities.Add(new SelectListItem(text: city, value: city));
            }

            testmodel.Roads = roads;
            testmodel.Districts = districts;
            testmodel.Cities = cities;

            return View(testmodel);
		}


		[HttpGet("Post/{id:int}")]
		public async Task<IActionResult> MeetingPost(
            [FromRoute][Required] int id
            )
		{
			MeetingAPI_URL += "/";

            methcall.SetUserDefaultData(this);

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string NotificationAPI_URL = "/api/Notification/Count";

            if (usrId != null)
            {
                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.Data;
            }

            GetMeetingPostResponse? meetPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
			{
                MeetingAPI_URL += "Participant/" + id;
                meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: Constants.Constants.POST_METHOD,
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
                                   methodName: Constants.Constants.GET_METHOD,
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

		[HttpPost("{meetingId:int}/Register")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MeetingRegister(int meetingId)
		{
            MeetingAPI_URL += "/Register/" + meetingId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingParticipationNo>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.POST_METHOD,
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
        [HttpPost("DeRegister/{meetingId:int}")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MeetingDeRegister(int meetingId)
        {
            MeetingAPI_URL += "/RemoveParticipant/" + meetingId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingPostDeRegister>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.POST_METHOD,
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
