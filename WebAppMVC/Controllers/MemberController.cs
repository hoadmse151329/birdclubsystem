using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using System.Diagnostics;
using System.Text.Json;
using WebAppMVC.Constants;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using System.Web;
using DAL.Models;
using WebAppMVC.Models.Member;
using BAL.ViewModels;
using System.Dynamic;
using Microsoft.AspNetCore.Identity;
using System;
using BAL.ViewModels.Member;
using System.Text.Encodings.Web;
using BAL.ViewModels.Event;

namespace WebAppMVC.Controllers
{
    [Route("Member")]
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string MemberInfoAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true,
        };
        private BirdClubLibrary methcall = new();

        public MemberController(ILogger<MemberController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            MemberInfoAPI_URL = "/api/Member/";
        }

        [HttpGet("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberProfile()
        {
            MemberInfoAPI_URL += "Profile";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberInfoAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (memberDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return Redirect("~/Home/Index");
            }
            else
            if (!memberDetails.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetails.ErrorMessage;
                return Redirect("~/Home/Index");
            }
            return View(memberDetails.Data);
        }
        [HttpPost("Update")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberUpdate(MemberViewModel memberDetail)
        {
            MemberInfoAPI_URL += "Update";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            memberDetail.MemberId = usrId;
            memberDetail.Status = 1;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "PUT",
                url: MemberInfoAPI_URL,
                _logger: _logger,
                inputType: memberDetail,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }
            return RedirectToAction("MemberProfile");
        }
        [HttpGet("HistoryEvent")]
        public async Task<IActionResult> MemberHistoryEvent()
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            string MemberMeetingPartAPI_URL = "/api/Meeting/Participation/AllMeetings";
            string MemberFieldTripPartAPI_URL = "/api/FieldTrip/Participation/AllFieldTrips";
            
            dynamic registeredModel = new ExpandoObject();

            var memberMeetingPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberMeetingPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var memberFieldTripPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberFieldTripPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (memberMeetingPart == null || memberFieldTripPart == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Participation History!).\n Member Participation History Not Found!\n"
                    + memberMeetingPart + "\n" + memberFieldTripPart;
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberMeetingPart.Status || !memberFieldTripPart.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Participation History!).\n Member Participation History Not Found!"
                + memberMeetingPart + "\n" + memberFieldTripPart;
                return RedirectToAction("MemberProfile");
            }

            List<GetEventParticipation> registeredEvents = new();
            registeredEvents.AddRange(memberMeetingPart.Data);
            registeredEvents.AddRange(memberFieldTripPart.Data);

            registeredModel.RegisteredEvents = registeredEvents;
            return View(registeredModel);
        }
        [HttpPost("Upload")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            string MemberAvatarAPI_URL = "/api/User/Upload";

            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(pathImage, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var image = "~/images/" + fileName;
                dynamic imageUpload = new ExpandoObject();
                imageUpload.ImagePath = image;
                imageUpload.MemberId = usrId;

                var getMemberAvatar = await methcall.CallMethodReturnObject<GetMemberAvatarResponse>(
                    _httpClient: _httpClient,
                    options: options,
                    methodName: "POST",
                    url: MemberAvatarAPI_URL,
                    _logger: _logger,
                    inputType: imageUpload,
                    accessToken: accToken);
                if (getMemberAvatar == null)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                }
                else
                if (!getMemberAvatar.Status)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                    + getMemberAvatar.ErrorMessage;
                }
                return RedirectToAction("MemberProfile");
            }
            return RedirectToAction("Error");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
        {
            string MemberChangePasswordAPI_URL = "/api/User/ChangePassword";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return RedirectToAction("Index", "Home");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string? usrname = HttpContext.Session.GetString("USER_NAME");
            if (string.IsNullOrEmpty(usrname)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            TempData["USER_NAME"] = usrname;

            memberPassword.userId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "PUT",
                url: MemberChangePasswordAPI_URL,
                _logger: _logger,
                inputType: memberPassword,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }
            return RedirectToAction("MemberProfile");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    // GET: MemberController
    }
}
