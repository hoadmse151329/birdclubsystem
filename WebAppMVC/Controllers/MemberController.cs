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
using WebAppMVC.Models.Transaction;
using WebAppMVC.Models.Bird;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

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

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");
            string? usrId = HttpContext.Session.GetString("USER_ID");

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

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            memberDetail.MemberId = usrId;

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
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            string MemberMeetingPartAPI_URL = "/api/Meeting/Participation/AllMeetings";
            string MemberFieldTripPartAPI_URL = "/api/FieldTrip/Participation/AllFieldTrips";
            string MemberContestPartAPI_URL = "/api/Contest/Participation/AllContests";
            
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

            var memberContestPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberContestPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (memberMeetingPart == null || memberFieldTripPart == null || memberContestPart == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Participation History!).\n Member Participation History Not Found!\n"
                    + memberMeetingPart + "\n" + memberFieldTripPart;
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberMeetingPart.Status || !memberFieldTripPart.Status || !memberContestPart.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Participation History!).\n Member Participation History Not Found!"
                + memberMeetingPart + "\n" + memberFieldTripPart;
                return RedirectToAction("MemberProfile");
            }

            List<GetEventParticipation> registeredEvents = new();
            registeredEvents.AddRange(memberMeetingPart.Data);
            registeredEvents.AddRange(memberFieldTripPart.Data);
            registeredEvents.AddRange(memberContestPart.Data);

            registeredModel.RegisteredEvents = registeredEvents;
            return View(registeredModel);
        }
        [HttpPost("Upload")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            string MemberAvatarAPI_URL = "/api/User/Upload";

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;
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
            return RedirectToAction("MemberProfile");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
        {
            string MemberChangePasswordAPI_URL = "/api/User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID");

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
        [HttpGet("Bird")]
        public async Task<IActionResult> MemberBird()
        {
            string MemberBirdAPI_URL = "/api/Bird/AllBirds";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID");

            dynamic birdModel = new ExpandoObject();

            var memberBird = await methcall.CallMethodReturnObject<GetListBirdByMemberResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberBirdAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (memberBird == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Bird List!). \n Member Not Found!";
                return RedirectToAction("MemberProfile");
            }
            if (!memberBird.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberBird.Status + " , Error Message: " + memberBird.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Bird List!). \n"
                    + memberBird.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }
            birdModel.MemberBirds = memberBird.Data;
            return View(birdModel);
        }
        [HttpGet("Payment")]
        public async Task<IActionResult> MemberPayment()
        {
            string MemberPaymentAPI_URL = "/api/Transaction/AllTransactions";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString("ACCESS_TOKEN");

            string? usrId = HttpContext.Session.GetString("USER_ID"); ;

            dynamic transactionModel = new ExpandoObject();

            var memberPayment = await methcall.CallMethodReturnObject<GetUserPaymentResponse>(
                _httpClient: _httpClient,
                options: options,
                methodName: "POST",
                url: MemberPaymentAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (memberPayment == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member!). \n Member Not Found!";
                return RedirectToAction("MemberProfile");
            }
            if (!memberPayment.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberPayment.Status + " , Error Message: " + memberPayment.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting User Payment!). \n"
                    + memberPayment.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }
            transactionModel.MemberPayments = memberPayment.Data;
            //transactionModel.MemberDetail = memberDetails.Data;
            return View(transactionModel);
        }
    }
}
