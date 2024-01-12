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

namespace WebAppMVC.Controllers
{
	public class MeetingController : Controller
	{
		private readonly ILogger<MeetingController> _logger;
		private readonly HttpClient _httpClient = null;
		private string MeetingAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
		private MethodCaller methcall = new();
        public MeetingController(ILogger<MeetingController> logger)
		{
			_logger = logger;
			_httpClient = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			_httpClient.BaseAddress = new Uri("https://localhost:7022");
			MeetingAPI_URL = "/api/Meeting";
		}

		[HttpGet]
		[Route("Meeting/Index")]
		public async Task<IActionResult> Index()
		{
            MeetingAPI_URL += "/All";
			string LocationAPI_URL_All = "/api/Location/All";
			dynamic testmodel = new ExpandoObject();

            var listLocationResponse = await methcall.CallMethodReturnObject<GetLocationResponseByList>(
                _httpClient: _httpClient,
                options: options,
                methodName: "GET",
                url: LocationAPI_URL_All);

            var listMeetResponse = await methcall.CallMethodReturnObject<GetMeetingResponseByList>(
				_httpClient: _httpClient,
				options: options,
				methodName: "GET",
				url: MeetingAPI_URL);

			if(listMeetResponse == null || listLocationResponse == null)
			{
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
			else
			if(!listMeetResponse.Status || !listLocationResponse.Status)
			{
				ViewBag.error =
					"Error while processing your request! (Getting List Meeting!).\n"
					+ listMeetResponse.ErrorMessage + "\n" + listLocationResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            testmodel.Meetings = listMeetResponse.Data;
			testmodel.Locations = listLocationResponse.Data;
            return View(testmodel);
		}

		[HttpGet("{id:int}")]
		[Route("Meeting/MeetingPost/{id}",Name = "Post")]
		public async Task<IActionResult> MeetingPost(int id)
		{
			MeetingAPI_URL += "/";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
			string? usrId = HttpContext.Session.GetString("USER_ID");
			GetMeetingPostResponse meetPostResponse = new();
            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId))
			{
                MeetingAPI_URL += "Participant/" + id;
                meetPostResponse = await methcall.CallMethodReturnObject<GetMeetingPostResponse>(
                                   _httpClient: _httpClient,
                                   options: options,
                                   methodName: "POST",
                                   url: MeetingAPI_URL,
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
                                   url: MeetingAPI_URL);
            }
            if (meetPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Meeting!).\n Meeting Not Found!";
                View("Index");
            }

            var meetmodel = meetPostResponse.Data;
            if (!meetPostResponse.Status)
			{
				ViewBag.error =
					"Error while processing your request! (Getting Meeting Post!).\n"
					+ meetPostResponse.ErrorMessage;
                View("Index");
			}
			/*if(TempData["PartakeNo"] != null)
				ViewBag.PartNumber = Int32.Parse(TempData["PartakeNo"].ToString());*/
            return View(meetmodel);
		}

		[HttpPost]
		public async Task<IActionResult> MeetingRegister(int meetingId)
		{
            MeetingAPI_URL += "/Register/" + meetingId;

            string? role = HttpContext.Session.GetString("ROLE_NAME");
			if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
			else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
			if(string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingParticipationNo>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL,
				inputType: usrId,
				accessToken: accToken);
			if(participationNo == null)
			{
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n Meeting Not Found!";
                RedirectToAction("MeetingPost", new { id = meetingId });
            }else
            if (!participationNo.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n"
					+ participationNo.ErrorMessage;
                RedirectToAction("MeetingPost", new { id = meetingId });
            }
			//TempData["partakeNo"] = participationNo.Data;

            return RedirectToAction("MeetingPost", new { id = meetingId });
        }
        /*[HttpPost]
		public async Task<IActionResult> In()
		{
			// Call the API endpoint
			HttpResponseMessage response = await _httpClient.GetAsync("1"); // Replace '1' with the actual meeting ID
			if (response.IsSuccessStatusCode)
			{
				var meeting = await response.Content.ReadAsAsync<MeetingViewModel>();
				return View(meeting);
			}
			else
			{
				// Handle error
				_logger.LogError($"API request failed with status code {response.StatusCode}");
				return View("Error");
			}
		}*/
	}
}
