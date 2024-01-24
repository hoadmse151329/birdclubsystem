using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using System.Diagnostics;
using System.Text.Json;
using WebAppMVC.Constants;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Data;
using DAL.Models;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Member;

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
        /*[Authorize(Roles = "Member")]*/
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
                Redirect("~/Home/Index");
            }
            else
            if (!memberDetails.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetails.ErrorMessage;
                Redirect("~/Home/Index");
            }
            return View(memberDetails.Data);
        }

        public IActionResult MemberHistoryEvent()
        {
            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            if (string.IsNullOrEmpty(accToken)) return RedirectToAction("Login", "Auth");

            string? role = HttpContext.Session.GetString("ROLE_NAME");
            if (string.IsNullOrEmpty(role)) return RedirectToAction("Login", "Auth");
            else if (!role.Equals("Member")) return View("Index");

            string? usrId = HttpContext.Session.GetString("USER_ID");
            if (string.IsNullOrEmpty(usrId)) return RedirectToAction("Login", "Auth");
            TempData["ROLE_NAME"] = role;
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    // GET: MemberController
    }
}
