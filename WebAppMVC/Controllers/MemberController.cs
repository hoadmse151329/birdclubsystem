﻿using Microsoft.AspNetCore.Mvc;
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

namespace WebAppMVC.Controllers
{
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly HttpClient _httpClient = null;
        private string MemberInfoAPI_URL = "";
        private JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        private MethodCaller methcall = new();

        public MemberController(ILogger<MemberController> logger)
        {
            _logger = logger;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri("https://localhost:7022");
            MemberInfoAPI_URL = "/api/Member";
        }

        [HttpGet]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberProfile()
        {
            MemberInfoAPI_URL += "/Profile";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;

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
        [HttpPost]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> MemberUpdate(MemberViewModel memberDetail)
        {
            MemberInfoAPI_URL += "/Update";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;

            memberDetail.MemberId = usrId;
            memberDetail.Status = "Active";

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
                return Redirect("~/Member/Profile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return Redirect("~/Member/Profile");
            }
            return RedirectToAction("MemberProfile");
        }

        public async Task<IActionResult> MemberHistoryEvent()
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            TempData["ROLE_NAME"] = role;
            string MemberMeetingPartAPI_URL = "/api/Meeting/Participation/AllMeetings";
            dynamic registeredModel = new ExpandoObject();

            var memberMeetingPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberMeetingPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (memberMeetingPart == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return Redirect("~/Member/Profile");
            }
            else
            if (!memberMeetingPart.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberMeetingPart.ErrorMessage;
                return Redirect("~/Member/Profile");
            }
            registeredModel.RegisteredEvents = memberMeetingPart.Data;
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
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");

            string MemberAvatarAPI_URL = "api/User/Upload";

            if (photo != null && photo.Length > 0)
            {
                var fileName = Path.GetFileName(photo.FileName);
                var pathImage = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

                using (var stream = new FileStream(pathImage, FileMode.Create))
                {
                    await photo.CopyToAsync(stream);
                }

                var image = "../images/" + fileName;
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
        [HttpPost]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
        {
            string MemberChangePasswordAPI_URL = "/api/User/ChangePassword";

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;
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
                return Redirect("~/Member/Profile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return Redirect("~/Member/Profile");
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
