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
using BAL.ViewModels.Event;
using Microsoft.AspNetCore.Http.Json;

namespace WebAppMVC.Controllers
{
    [Route("Meeting")]
    public class MeetingController : Controller
    {
        private readonly string LocationAPI_URL_All_Road = "/api/Location/AllAddressRoads";
        private readonly string LocationAPI_URL_All_District = "/api/Location/AllAddressDistricts";
        private readonly string LocationAPI_URL_All_City = "/api/Location/AllAddressCities";
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
            [FromQuery] List<string>? road,
            [FromQuery] List<string>? district,
            [FromQuery] List<string>? city
            )
        {
            if ((road == null || road.Count == 0) && (district == null || district.Count == 0) && 
                city == null || 
                city.Count == 0) MeetingAPI_URL += "/All";
            else MeetingAPI_URL += "/Search?";

            if (road != null && road.Any())
            {
                foreach (var selectedRoad in road)
                {
                    if(selectedRoad != null)
                    {
                        MeetingAPI_URL += $"road={Uri.EscapeDataString(selectedRoad.Trim())}&";
                    }
                }
            }
            if (district != null && district.Any())
            {
                foreach (var selectedDistrict in district)
                {
                    if (selectedDistrict != null)
                    {
                        MeetingAPI_URL += $"district={Uri.EscapeDataString(selectedDistrict.Trim())}&";
                    }
                }
            }
            if (city != null && city.Any())
            {
                foreach (var selectedCity in city)
                {
                    if(selectedCity != null)
                    {
                        MeetingAPI_URL += $"city={Uri.EscapeDataString(selectedCity.Trim())}&";
                    }
                }
            }
            if (MeetingAPI_URL.Contains("Search"))
            {
                MeetingAPI_URL = MeetingAPI_URL.Substring(0, MeetingAPI_URL.Length - 1); // Remove the trailing '&'
            }

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
                methodName: Constants.Constants.POST_METHOD,
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
            else if (!listMeetResponse.Status || !listLocationRoadResponse.Status || !listLocationDistrictResponse.Status || !listLocationCityResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationRoadResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;

            List<SelectListItem> roads = new();
            foreach (var roadOption in listLocationRoadResponse.Data)
            {
                roads.Add(new SelectListItem(text: roadOption, value: roadOption));
            }

            List<SelectListItem> districts = new();
            foreach (var districtOption in listLocationDistrictResponse.Data)
            {
                districts.Add(new SelectListItem(text: districtOption, value: districtOption));
            }

            List<SelectListItem> cities = new();
            foreach (var cityOption in listLocationCityResponse.Data)
            {
                cities.Add(new SelectListItem(text: cityOption, value: cityOption));
            }

            testmodel.Roads = roads;
            testmodel.Districts = districts;
            testmodel.Cities = cities;

            return View(testmodel);
        }

        [HttpGet("Index/Filter")]
        public async Task<IActionResult> IndexFilter(
            [FromQuery] List<string>? road,
            [FromQuery] List<string>? district,
            [FromQuery] List<string>? city
            )
        {
            if ((road == null || road.Count == 0) && (district == null || district.Count == 0) &&
                city == null ||
                city.Count == 0) MeetingAPI_URL += "/All";
            else MeetingAPI_URL += "/Search?";

            if (road != null && road.Any())
            {
                foreach (var selectedRoad in road)
                {
                    if (selectedRoad != null)
                    {
                        MeetingAPI_URL += $"road={Uri.EscapeDataString(selectedRoad.Trim())}&";
                    }
                }
            }
            if (district != null && district.Any())
            {
                foreach (var selectedDistrict in district)
                {
                    if (selectedDistrict != null)
                    {
                        MeetingAPI_URL += $"district={Uri.EscapeDataString(selectedDistrict.Trim())}&";
                    }
                }
            }
            if (city != null && city.Any())
            {
                foreach (var selectedCity in city)
                {
                    if (selectedCity != null)
                    {
                        MeetingAPI_URL += $"city={Uri.EscapeDataString(selectedCity.Trim())}&";
                    }
                }
            }
            if (MeetingAPI_URL.Contains("Search"))
            {
                MeetingAPI_URL = MeetingAPI_URL.Substring(0, MeetingAPI_URL.Length - 1); // Remove the trailing '&'
            }

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
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.Data;
            }

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: Constants.Constants.POST_METHOD,
                url: MeetingAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listMeetResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse + " , Error Message: " + listMeetResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else if (!listMeetResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;

            /*testmodel.Roads = roads;
            testmodel.Districts = districts;
            testmodel.Cities = cities;*/

            return Json(testmodel);
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

                ViewBag.NotificationCount = notificationCount.IntData;
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

            string NotificationAPI_URL = "/api/Notification/CreateEvent";

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
            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Registering Meeting Participation!): Meeting Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n Meeting Not Found!";
                RedirectToAction("MeetingPost", new { id = meetingId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Meeting Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n"
                    + participationNo.ErrorMessage;
                RedirectToAction("MeetingPost", new { id = meetingId });
            }

            string MeetingPostAPI_URL = "/api/Meeting/" + meetingId;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: MeetingPostAPI_URL,
                                   _logger: _logger);

            if (meetPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                View("Index");
            }

            if (!meetPostResponse.Status)
            {
                _logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting Post!).\n"
                    + meetPostResponse.ErrorMessage;
                View("Index");
            }

            CreateNotificationRequest notif = new CreateNotificationRequest()
            {
                Title = Constants.Constants.NOTIFICATION_TYPE_MEETING_REGISTER,
                Description = Constants.Constants.NOTIFICATION_DESCRIPTION_MEETING_REGISTER + meetPostResponse.Data.MeetingName,
                MemberId = usrId
            };

            var notificationResponse = await methcall.CallMethodReturnObject<GetNotificationPostResponse>(
                    _httpClient: _httpClient,
                    options: options,
                    methodName: "POST",
                    url: NotificationAPI_URL,
                    inputType: notif,
                    accessToken: accToken,
                    _logger: _logger);

            if (notificationResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Notification).\n User Not Found!";
                return RedirectToAction("MeetingPost", new { id = meetingId });
            }
            if (!notificationResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + notificationResponse.Status + " , Error Message: " + notificationResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Notification!).\n"
                    + notificationResponse.ErrorMessage;
                return RedirectToAction("MeetingPost", new { id = meetingId });
            }

            return RedirectToAction("MeetingPost", new { id = meetingId });
        }
        [HttpPost("{meetingId:int}/DeRegister")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MeetingDeRegister([FromRoute][Required] int meetingId)
        {
            MeetingAPI_URL += "/" + meetingId + "/Participant/Remove";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

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

            return RedirectToAction("MemberHistoryEvent", "Member");
        }
    }
}
