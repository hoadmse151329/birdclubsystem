using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using BAL.ViewModels;
using WebAppMVC.Models.Meeting;
using BAL.ViewModels.Meeting;
using System.Dynamic;
using WebAppMVC.Constants;
using WebAppMVC.Models.Location;
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


		[HttpGet("{id}")]
		public async Task<IActionResult> MeetingPost(int id)
		{
			var options = new JsonSerializerOptions 
			{ PropertyNameCaseInsensitive = true, };
			MeetingAPI_URL += "/{id}";
			HttpResponseMessage response = await _httpClient.GetAsync(MeetingAPI_URL);
			if (!response.IsSuccessStatusCode)
			{
				ViewBag.error = "Error while getting meeting information!";
				return Redirect("~/Meeting/Index");
			}
			string jsonResponse = await response.Content.ReadAsStringAsync();
			var meetpostResponse = JsonSerializer.Deserialize<GetMeetingPostResponse>(jsonResponse, options);
			var responsemeetpost = meetpostResponse.Data;
			return View(responsemeetpost);
		}
		[HttpPost]
		public async Task<IActionResult> MeetingRegister(int meetingId)
		{
            MeetingAPI_URL += "/Register";

            string? role = HttpContext.Session.GetString("ROLE_NAME");
			if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
			else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
			if(usrId == null) return RedirectToAction("Login", "Auth");

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (accToken == null) return RedirectToAction("Login", "Auth");

            var participationNo = await methcall.CallMethodReturnObject<GetMeetingParticipationNo>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MeetingAPI_URL + "/" + meetingId,
				inputType: usrId,
				accessToken: accToken);
			if(participationNo == null)
			{
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n Meeting Not Found!";
                View("Index");
            }
            if (!participationNo.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Registering Meeting Participation!).\n"
					+ participationNo.ErrorMessage;
                View("Index");
            }
			ViewBag.PartNumber = participationNo.Data;
			return View("MeetingPost");
        }
        [HttpPost]
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
		}





		[HttpPost]
		public async Task<IActionResult> RegisterMeeting(RegisterMeeting register)
		{
			var json = JsonSerializer.Serialize(register);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			MeetingAPI_URL += "/Register";
			HttpResponseMessage response = await _httpClient.PostAsync(MeetingAPI_URL, content);
			if (!response.IsSuccessStatusCode)
			{
				ViewBag.error = "Error while registering for meeting ! ";
				return View("MeetingRegister");
			}
			string jsonResponse = await response.Content.ReadAsStringAsync();
			var meetingResponse = JsonSerializer.Deserialize<GetMeetingRegisterResponse>(jsonResponse);
			var responseRegister = meetingResponse.Data;
			if (meetingResponse.Status)
			{
				HttpContext.Session.SetString("USER_NAME", responseRegister.UserName);
				HttpContext.Session.SetString("FULL_NAME", responseRegister.FullName);
			}
			return null;
		}
	}
}
