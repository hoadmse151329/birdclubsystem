using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Contest;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Member;
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

            var listLocationRoadResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "GET",
                url: LocationAPI_URL_All_Road,
                _logger: _logger);
            var listLocationDistrictResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "GET",
                url: LocationAPI_URL_All_District,
                _logger: _logger);
            var listLocationCityResponse = await methcall.CallMethodReturnObject<GetLocationAddressResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "GET",
                url: LocationAPI_URL_All_City,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
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

        [HttpGet("ContestPost/{id:int}")]
        public async Task<IActionResult> ContestPost(
            [FromRoute][Required] int id
            )
        {
            ContestAPI_URL += "/";

            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            GetContestPostResponse? contestPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
            {
                ContestAPI_URL += "Participant/" + id;
                contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: "POST",
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
                                   methodName: "GET",
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

            var contestmodel = contestPostResponse.Data;
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Username or Password is invalid: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                View("Index");
            }
            /*if(TempData["PartakeNo"] != null)
				ViewBag.PartNumber = Int32.Parse(TempData["PartakeNo"].ToString());*/
            return View(contestmodel);
        }

        [HttpPost("ContestRegister/{contestId:int}")]
        public async Task<IActionResult> ContestRegister(
            [FromRoute][Required] int contestId
            )
        {
            ContestAPI_URL += "/" + contestId;
            string MemberAPI_URL = "/api/Member/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: "GET",
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            methcall.SetCookie(Response, "tripRegistrationInProgress", contestPostResponse.Data, cookieOptions, jsonOptions, 20);

            PaymentInformationModel model = new PaymentInformationModel()
            {
                Fullname = memberDetails.Data.FullName,
                PayAmount = (decimal)contestPostResponse.Data.Fee,
                TransactionType = "Member-FieldTrip-Registration"
            };

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        [HttpGet("FieldTripConfirmRegister")]
        public async Task<IActionResult> ContestConfirmRegister(
            [FromRoute][Required] int contestId
            )
        {
            ContestAPI_URL += "/Register/" + contestId;
            string MemberAPI_URL = "/api/Member/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetContestParticipationNo>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: ContestAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): Contest Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n Contest Not Found!";
                RedirectToAction("ContestPost", new { id = contestId });
            }
            else if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n"
                        + participationNo.ErrorMessage;
                RedirectToAction("ContestPost", new { id = contestId });
            }

            return RedirectToAction("ContestPost", new { id = contestId });
        }

        [HttpPost]
        [Route("ContestDeRegister/{contestId:int}")]
        public async Task<IActionResult> ContestDeRegister(int contestId)
        {
            ContestAPI_URL += "/RemoveParticipant/" + contestId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetContestPostDeRegister>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
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
	}
}
