using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using WebAppMVC.Constants;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Member;
using WebAppMVC.Models.Notification;
using WebAppMVC.Models.Transaction;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Services.Interfaces;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC.Controllers
{
    [Route("FieldTrip")]
    public class FieldTripController : Controller
	{
		private readonly ILogger<FieldTripController> _logger;
        private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
        private readonly IVnPayService _vnPayService;
		private string FieldTripAPI_URL = "";
		private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
		};
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        private BirdClubLibrary methcall = new();
		public FieldTripController(ILogger<FieldTripController> logger, IConfiguration config, IVnPayService vnPayService)
		{
			_logger = logger;
            _config = config;
			_httpClient = new HttpClient();
            _vnPayService = vnPayService;
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

            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string NotificationAPI_URL = "/api/Notification/Count";

            if (usrId != null)
            {
                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
            }

            var listLocationRoadResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_Road,
                _logger: _logger);
            var listLocationDistrictResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_District,
                _logger: _logger);
            var listLocationCityResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: LocationAPI_URL_All_City,
                _logger: _logger);

            var listTripResponse = await methcall.CallMethodReturnObject<GetFieldTripResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: FieldTripAPI_URL,
                inputType: role,
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

            testmodel.FieldTrips = listTripResponse.Data;
            return View(testmodel);
        }
        [HttpGet("Post/{id:int}")]
        public async Task<IActionResult> FieldTripPost(
            [FromRoute][Required]int id
            )
		{
            FieldTripAPI_URL += "/";

            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string NotificationAPI_URL = "/api/Notification/Count";

            if (usrId != null)
            {
                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
            }

            dynamic fieldtripDetail = new ExpandoObject();

            GetFieldTripPostResponse? fieldtripPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
            {
                FieldTripAPI_URL += "Participant/" + id;
                fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.POST_METHOD,
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
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: FieldTripAPI_URL,
                                   _logger: _logger);
            }
            if (fieldtripPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Fieldtrip Post!).\n Fieldtrip Not Found!";
                return RedirectToAction("Index");
            }
            if (!fieldtripPostResponse.Status)
            {
                //_logger.LogInformation("Username or Password is invalid: " + fieldtripPostResponse.Status + " , Error Message: " + fieldtripPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Fieldtrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("Index");
            }

            fieldtripDetail.FieldTrip = fieldtripPostResponse.Data;
            fieldtripDetail.TourFeatures = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type == "tour_features").ToList();
            fieldtripDetail.ActivitiesAndTransportation = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type == "activities_and_transportation").ToList();
            fieldtripDetail.ImportantToKnow = fieldtripPostResponse.Data.FieldtripAdditionalDetails.Where(f => f.Type == "important_to_know").ToList();
            fieldtripDetail.DayByDays = fieldtripPostResponse.Data.FieldtripDaybyDays;
            fieldtripDetail.Inclusions = fieldtripPostResponse.Data.FieldtripInclusions;
            fieldtripDetail.GettingThere = fieldtripPostResponse.Data.FieldtripGettingTheres;
            fieldtripDetail.Pictures = fieldtripPostResponse.Data.FieldtripPictures;

            return View(fieldtripDetail);
        }

        [HttpPost("{tripId:int}/Register")]
        public async Task<IActionResult> FieldTripRegister(
            [FromRoute][Required] int tripId
            )
        {
            FieldTripAPI_URL += "/" + tripId + "/Lite";
            string MemberAPI_URL = "/api/Member/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var fieldtripPostResponse = await methcall.CallMethodReturnObject<GetFieldTripPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: FieldTripAPI_URL,
                                   _logger: _logger);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (fieldtripPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting FieldTrip!).\n FieldTrip Not Found!";
                return RedirectToAction("Index");
            }
            if (!fieldtripPostResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting FieldTrip Post!).\n"
                    + fieldtripPostResponse.ErrorMessage;
                return RedirectToAction("Index");
            }
            if (memberDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Details!).\n Member Details Not Found!";
                return RedirectToAction("Index");
            }
            if (!memberDetails.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Details!).\n"
                    + memberDetails.ErrorMessage;
                return RedirectToAction("Index");
            }
            methcall.SetCookie(Response, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_COOKIE, fieldtripPostResponse.Data, cookieOptions, jsonOptions, 20);

            PaymentInformationModel model = new PaymentInformationModel()
            {
                Fullname = memberDetails.Data.FullName,
                PayAmount = (decimal)fieldtripPostResponse.Data.Fee,
                TransactionType = Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_TYPE
            };

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        [HttpGet("ConfirmRegister")]
        public async Task<IActionResult> FieldTripConfirmRegister()
        {
            var fieldTrip = await methcall.GetCookie<FieldTripViewModel>(Request, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_COOKIE, jsonOptions);
            
            if(fieldTrip == null)
            {
                return RedirectToAction("Index");
            }
            int tripId = fieldTrip.TripId.Value;

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_COOKIE, cookieOptions, jsonOptions);

            FieldTripAPI_URL += "/Register/" + tripId;

            string TransactionAPI_URL = "/api/Transaction/UpdateUser";
            string MemberAPI_URL = "/api/Member/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var participationNo = await methcall.CallMethodReturnObject<GetFieldTripParticipationNo>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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

            var tran = await methcall.GetCookie<TransactionViewModel>(Request, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_COOKIE, jsonOptions);

            if (tran == null)
            {
                _logger.LogError("Error while registering your new account: Your Registration Transaction not found!");

                ViewBag.error = "Error while registering your new account: Your Registration Transaction not found! " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return View("Register");
            }

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_COOKIE, cookieOptions, jsonOptions);

            UpdateTransactionRequest unmtr = new UpdateTransactionRequest()
            {
                MemberId = memberDetails.Data.MemberId,
                TransactionId = tran.TransactionId
            };

            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: TransactionAPI_URL,
                inputType: unmtr,
                accessToken: accToken,
                _logger: _logger);

            if (transactionResponse == null)
            {
                _logger.LogError("Error while registering your new account: User Transaction Saving Failed!");

                ViewBag.error = "Error while registering your new account: User Transaction Saving Failed!, " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return View("Register");
            }

            return RedirectToAction("FieldTripPost", new { id = tripId });
        }
        [HttpPost("{tripId:int}/DeRegister")]
        public async Task<IActionResult> FieldTripDeRegister(int tripId)
        {
            FieldTripAPI_URL += "/RemoveParticipant/" + tripId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetFieldTripPostDeRegister>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
