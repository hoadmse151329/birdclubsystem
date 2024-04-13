using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Contest;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;

namespace WebAppMVC.Controllers
{
	public class ContestController : Controller
	{
		private readonly ILogger<ContestController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string ContestAPI_URL = "";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private MethodCaller methcall = new();
		public ContestController(ILogger<ContestController> logger, IConfiguration config)
		{
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            ContestAPI_URL = "/api/Contest";
        }

        [HttpGet]
        [Route("Contest/Index")]
        public async Task<IActionResult> Index()
		{
            ContestAPI_URL += "/All";
            string LocationAPI_URL_All = "/api/Location/All";
            dynamic testmodel = new ExpandoObject();


            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: ContestAPI_URL,
                _logger: _logger);

            if (listContestResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse + " , Error Message: " + listContestResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listContestResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listContestResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.Contests = listContestResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
		}

        [HttpGet("{id:int}")]
        [Route("Contest/ContestPost/{id:int}")]
        public async Task<IActionResult> ContestPost(int id)
        {
            ContestAPI_URL += "/";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? role = HttpContext.Session.GetString("ROLE_NAME");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            string? usrname = HttpContext.Session.GetString("USER_NAME");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            GetContestPostResponse? contestPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId))
            {
                ContestAPI_URL += "Participant/" + id;
                contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
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
                                   options: options,
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

        [HttpPost]
        [Route("Contest/ContestRegister/{contestId:int}")]
        public async Task<IActionResult> ContestRegister(int contestId)
        {
            ContestAPI_URL += "/Register/" + contestId;

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

            var participationNo = await methcall.CallMethodReturnObject<GetContestParticipationNo>(
                _httpClient: _httpClient,
                options: options,
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
            
            return RedirectToAction("ContestPost", new {id = contestId});
        }

        [HttpPost]
        [Route("Contest/ContestDeRegister/{contestId:int}")]
        public async Task<IActionResult> ContestDeRegister(int contestId)
        {
            ContestAPI_URL += "/RemoveParticipant/" + contestId;

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

            var participationNo = await methcall.CallMethodReturnObject<GetContestPostDeRegister>(
                _httpClient: _httpClient,
                options: options,
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
