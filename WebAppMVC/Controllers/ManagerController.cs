using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    public class ManagerController : Controller
    {

        private readonly ILogger<MeetingController> _logger;
        private readonly HttpClient _httpClient = null;
        private string ManagerAPI_URL = "";
        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private MethodCaller methcall = new();

        public ManagerController(ILogger<MeetingController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
            ManagerAPI_URL = "/api/";
        }

        // GET: ManagerController
        public IActionResult ManagerIndex()
        {
            return View();
        }
        [HttpGet]
        [Route("Manager/Meeting")]
        public async Task<IActionResult> ManagerMeeting()
        {
            string LocationAPI_URL_All = ManagerAPI_URL + "Location/All";
            ManagerAPI_URL += "Meeting/All";
            dynamic testmodel = new ExpandoObject();

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All,
                _logger: _logger);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: ManagerAPI_URL,
                _logger: _logger);

            if (listMeetResponse == null || listLocationResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Meeting!). List was Empty!: " + listMeetResponse + " , Error Message: " + listMeetResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                Redirect("~/Manager/Index");
            }
            else
            if (!listMeetResponse.Status || !listLocationResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listMeetResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                Redirect("~/Manager/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;
            testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
        }
        public IActionResult ManagerNotification()
        {
            return View();
        }
        [HttpGet("{id:int}")]
        [Route("Manager/Meeting/{id:int}")]
        public async Task<IActionResult> ManagerMeetingDetail(int id)
        {
            ManagerAPI_URL += "Meeting/" + id;

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Manager")) return RedirectToAction("Login", "Auth");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;

            var meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                _httpClient: _httpClient,
                                options: options,
                                methodName: "GET",
                                url: ManagerAPI_URL,
                                _logger: _logger);
            if (meetPostResponse == null)
            {
                _logger.LogInformation("Username or Password is invalid: " + meetPostResponse.Status + " , Error Message: " + meetPostResponse.ErrorMessage);
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
        public IActionResult ManagerFieldtrip()
        {
            return View();
        }
        public IActionResult ManagerFieldtripDetail()
        {
            return View();
        }
        public IActionResult ManagerBirdContest()
        {
            return View();
        }
        public IActionResult ManagerBirdContestDetail()
        {
            return View();
        }
        public IActionResult ManagerProfile()
        {
            return View();
        }
        public IActionResult ManagerFeedBack()
        {
            return View();
        }
        public IActionResult ManagerStatical()
        {
            return View();
        }

    }
}
