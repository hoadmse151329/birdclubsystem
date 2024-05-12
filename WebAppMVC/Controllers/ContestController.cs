﻿using BAL.ViewModels;
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
using WebAppMVC.Constants;
using WebAppMVC.Models.Bird;
using WebAppMVC.Models.Contest;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Member;
using WebAppMVC.Models.Notification;
using WebAppMVC.Models.Transaction;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Services;

namespace WebAppMVC.Controllers
{
    [Route("Contest")]
	public class ContestController : Controller
	{
		private readonly ILogger<ContestController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string ContestAPI_URL = "";
        private readonly IVnPayService _vnPayService;
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
        public ContestController(ILogger<ContestController> logger, IConfiguration config, IVnPayService vnPayService)
		{
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            _vnPayService = vnPayService;
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            ContestAPI_URL = "/api/Contest";
        }

        [HttpGet("Index")]
        public async Task<IActionResult> Index()
		{
            ContestAPI_URL += "/All";
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

                ViewBag.NotificationCount = notificationCount.Data;
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

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listContestResponse == null || listLocationRoadResponse == null || listLocationDistrictResponse == null || listLocationCityResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse + " , Error Message: " + listContestResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listContestResponse.Status || !listLocationRoadResponse.Status || !listLocationDistrictResponse.Status || !listLocationCityResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listContestResponse.ErrorMessage + "\n" + listLocationRoadResponse.ErrorMessage;
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

            testmodel.Contests = listContestResponse.Data;
            return View(testmodel);
		}

        [HttpGet("Post/{id:int}")]
        public async Task<IActionResult> ContestPost(
            [FromRoute][Required] int id
            )
        {
            ContestAPI_URL += "/";

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

                ViewBag.NotificationCount = notificationCount.Data;
            }

            GetContestPostResponse? contestPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
            {   
                ContestAPI_URL += id +"/Participant";
                contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.POST_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger,
                                   inputType: usrId,
                                   accessToken: accToken);
            }
            else
            {
                ContestAPI_URL += id;
                contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            }
            if (contestPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                View("Index");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Username or Password is invalid: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                View("Index");
            }
            var contestmodel = contestPostResponse.Data;
            return View(contestmodel);
        }

        [HttpPost("{contestId:int}/Bird/{birdId:int}/Register")]
        public async Task<IActionResult> ContestRegister(
            [FromRoute][Required] int contestId,
            [FromRoute][Required] int birdId
            )
        {
            ContestAPI_URL += "/" + contestId;
            string MemberAPI_URL = "/api/Member/Profile";
            string BirdAPI_URL = "/api/Bird/" + birdId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var birdDetails = await methcall.CallMethodReturnObject<GetBirdResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: BirdAPI_URL,
                _logger: _logger);

            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                RedirectToAction("Index");
            }
            if (!contestPostResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                RedirectToAction("Index");
            }
            if (memberDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member!).\n Member Not Found!";
                RedirectToAction("ContestPost", new { id = contestId });
            }
            if (!memberDetails.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member!).\n"
                    + contestPostResponse.ErrorMessage;
                RedirectToAction("ContestPost", new { id = contestId });
            }
            if (birdDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Bird for Contest Registration!).\n Bird Not Found!";
                RedirectToAction("ContestPost",new { id = contestId});
            }
            if (!birdDetails.Status)
            {
                ViewBag.error =
                   "Error while processing your request! (Getting Bird for Contest Registration!).\n";
                RedirectToAction("ContestPost", new { id = contestId });
            }
            if (birdDetails.Data.Elo >= contestPostResponse.Data.ReqMinELO && birdDetails.Data.Elo <= contestPostResponse.Data.ReqMaxELO)
            {
                ViewBag.error =
                   "Error while processing your request! (Your Bird Elo must be more than " + contestPostResponse.Data.ReqMinELO + "and lesser than" + contestPostResponse.Data.ReqMaxELO + " to register this Contest!).\n";
                RedirectToAction("ContestPost", new { id = contestId });
            }
            methcall.SetCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_COOKIE, contestPostResponse.Data, cookieOptions, jsonOptions, 20);

            methcall.SetCookie(Response, Constants.Constants.MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE, birdDetails.Data, cookieOptions, jsonOptions, 20);

            PaymentInformationModel model = new PaymentInformationModel()
            {
                Fullname = memberDetails.Data.FullName,
                PayAmount = (decimal)contestPostResponse.Data.Fee,
                TransactionType = Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_TYPE
            };

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        [HttpGet("ConfirmRegister")]
        public async Task<IActionResult> ContestConfirmRegister()
        {
            var contest = await methcall.GetCookie<ContestViewModel>(Request, Constants.Constants.MEMBER_CONTEST_REGISTRATION_COOKIE, jsonOptions);

            if (contest == null)
            {
                return RedirectToAction("Index");
            }
            int conId = contest.ContestId.Value;

            var bird = await methcall.GetCookie<BirdViewModel>(Request, Constants.Constants.MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE, jsonOptions);

            if (bird == null)
            {
                return RedirectToAction("Index");
            }
            int birdId = bird.BirdId.Value;

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_COOKIE, cookieOptions, jsonOptions);

            ContestAPI_URL += conId + "Bird/" + birdId + "/Register";

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
            var participationNo = await methcall.CallMethodReturnObject<GetContestParticipationNo>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): Contest Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n Contest Not Found!";
                return RedirectToAction("ContestPost", new { id = conId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n"
                    + participationNo.ErrorMessage;
                return RedirectToAction("ContestPost", new { id = conId });
            }

            var tran = await methcall.GetCookie<TransactionViewModel>(Request, Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE, jsonOptions);

            if (tran == null)
            {
                _logger.LogError("Error while registering your bird for contest participation: Your Registration Transaction not found!");

                ViewBag.error = "Error while registering your bird for contest participation: Your Registration Transaction not found! " +
                    "\nPlease contact birdclub manager for assistance with resolving this issue";

                return RedirectToAction("ContestPost", new { id = conId });
            }

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE, cookieOptions, jsonOptions);

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
                _logger.LogError("Error while registering your bird for contest participation: User Transaction Saving Failed!");

                ViewBag.error = "Error while registering your bird for contest participation: User Transaction Saving Failed!, " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return RedirectToAction("ContestPost", new { id = conId });
            }

            return RedirectToAction("ContestPost", new { id = conId });
        }

        [HttpPost("{contestId:int}/Bird/{birdId:int}/DeRegister")]
        public async Task<IActionResult> ContestDeRegister(
            [FromRoute][Required] int contestId,
            [FromRoute][Required] int birdId
            )
        {
            ContestAPI_URL += contestId + "/Bird/" + birdId + "/Participant/Remove";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetContestPostDeRegister>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Remove Contest Participation Registration!): Contest Participation Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Remove Contest Participation Registration!).\n Contest Participation Not Found!";
                RedirectToAction("ContestPost", new { id = contestId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Remove Contest Participation Registration!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Remove Contest Participation Registration!).\n"
                    + participationNo.ErrorMessage;
                RedirectToAction("ContestPost", new { id = contestId });
            }

            return RedirectToAction("ContestPost", new { id = contestId });
        }
        public IActionResult LeaderBoard()
		{
			return View();
		}
        [HttpPost("Something")]
        public ActionResult UpdatePlayerElo(double playerElo, double averageElo, int birdPoints, int totalPoints, double n)
        {
            List<double> Y = new List<double> { 40, 35, 30, 25, 20 }; // Adjust this list as needed
            double basicEloChange = methcall.CalculateBasicEloChange(playerElo, averageElo, birdPoints, totalPoints, n, Y);
            double updatedElo = methcall.UpdateElo(playerElo, basicEloChange);

            // Update player's Elo in your database or wherever it's stored

            return Json(new { updatedElo });
        }
    }
}
