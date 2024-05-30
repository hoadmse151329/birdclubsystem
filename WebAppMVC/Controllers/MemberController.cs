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
using WebAppMVC.Models.Notification;
using System.ComponentModel.DataAnnotations;
using WebAppMVC.Models.Feedback;
using WebAppMVC.Models.ViewModels;

namespace WebAppMVC.Controllers
{
    [Route("Member")]
    public class MemberController : Controller
    {
        private readonly ILogger<MemberController> _logger;
        private readonly IConfiguration _config;
        private readonly HttpClient _httpClient = null;
        private string MemberInfoAPI_URL = "";
        private JsonSerializerOptions jsonOptions = new JsonSerializerOptions
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

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);
            string? imagePath = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE);

            var memberInvalids = new MemberProfileVM();
            var memberInvalidDetails = methcall.GetValidationTempData<MemberViewModel>(this, TempData, Constants.Constants.UPDATE_MEMBER_DETAILS_VALID, "memberDetail", jsonOptions);
            if (memberInvalidDetails != null)
            {
                memberInvalidDetails.ImagePath = imagePath;
                memberInvalidDetails.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(memberInvalidDetails.Gender);
                memberInvalids.managerDetail = memberInvalidDetails;
                return View(memberInvalids);
            }

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
            var memberInvalidPasswordUpdate = methcall.GetValidationTempData<UpdateMemberPassword>(this, TempData, Constants.Constants.UPDATE_MEMBER_PASSWORD_VALID, "memberPassword", jsonOptions);
            if (memberInvalidPasswordUpdate != null)
            {
                memberInvalids.managerPassword = memberInvalidPasswordUpdate;
            }
            memberDetails.Data.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(memberDetails.Data.Gender);
            memberInvalids.managerDetail = memberDetails.Data;
            return View(memberInvalids);
        }
        [HttpPost("Profile")]
        public async Task<IActionResult> MemberUpdate(MemberViewModel memberDetail)
        {
            MemberInfoAPI_URL += "Update";
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEMBER_DETAILS_VALID, memberDetail, jsonOptions);
                return RedirectToAction("MemberProfile");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? imagePath = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE);

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            memberDetail.ImagePath = imagePath;
            memberDetail.MemberId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: MemberInfoAPI_URL,
                _logger: _logger,
                inputType: memberDetail,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Member Profile!).\n Member Details Not Found!";
                TempData["Error"] = "Error while updating profile.";
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Updating Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                TempData["Error"] = "Error while updating profile.";
                return RedirectToAction("MemberProfile");
            }
            TempData["Success"] = "Successfully updated profile!";
            return RedirectToAction("MemberProfile");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
        {
            string MemberChangePasswordAPI_URL = "/api/User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_MEMBER_PASSWORD_VALID, memberPassword, jsonOptions);
                return RedirectToAction("MemberProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            memberPassword.userId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: MemberChangePasswordAPI_URL,
                _logger: _logger,
                inputType: memberPassword,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                TempData["Error"] = "Error while updating password.";
                return RedirectToAction("MemberProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                TempData["Error"] = "Error while updating password.";
                return RedirectToAction("MemberProfile");
            }
            TempData["Success"] = "Successfully updated password!";
            return RedirectToAction("MemberProfile");
        }
        [HttpGet("HistoryEvent")]
        public async Task<IActionResult> MemberHistoryEvent()
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            string MemberMeetingPartAPI_URL = "/api/Meeting/Participation/AllMeetings";
            string MemberFieldTripPartAPI_URL = "/api/FieldTrip/Participation/AllFieldTrips";
            string MemberContestPartAPI_URL = "/api/Contest/Participation/AllContests";

            dynamic registeredModel = new ExpandoObject();

            var memberMeetingPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberMeetingPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var memberFieldTripPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberFieldTripPartAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var memberContestPart = await methcall.CallMethodReturnObject<GetListEventParticipation>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
        [HttpPost("CreateFeedback")]
        public async Task<IActionResult> CreateFeedback(CreateFeedbackRequest createFeedback)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            createFeedback.MemberId = usrId;

            string FeedbackAPI_URL = "/api/Feedback/Create";

            var feedbackResponse = await methcall.CallMethodReturnObject<GetFeedbackPostResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: FeedbackAPI_URL,
                inputType: createFeedback,
                accessToken: accToken,
                _logger: _logger);

            if (feedbackResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Feedback!).\n Not Found!";
                TempData["Error"] = feedbackResponse.ErrorMessage;
                return RedirectToAction("MemberHistoryEvent");
            }
            if (!feedbackResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + feedbackResponse.Status + " , Error Message: " + feedbackResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Feedback!).\n"
                    + feedbackResponse.ErrorMessage;
                TempData["Error"] = feedbackResponse.ErrorMessage;
                return RedirectToAction("MemberHistoryEvent");
            }
            TempData["Success"] = feedbackResponse.SuccessMessage;
            return RedirectToAction("MemberHistoryEvent");
        }
        [HttpPost("Upload")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string MemberAvatarAPI_URL = "/api/User/Upload";

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"avatar/{Guid.NewGuid()}-{filename}";
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
                    options: jsonOptions,
                    methodName: Constants.Constants.POST_METHOD,
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

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            dynamic birdModel = new ExpandoObject();

            var memberBird = await methcall.CallMethodReturnObject<GetListBirdByMemberResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
            birdModel.CreateBird = methcall.GetValidationTempData<BirdViewModel>(this, TempData, Constants.Constants.CREATE_BIRD_VALID, "createBird", jsonOptions);
            birdModel.MemberBirds = memberBird.Data;
            return View(birdModel);
        }
        [HttpGet("Bird/{birdId:int}")]
        public async Task<IActionResult> MemberBirdDetail([FromRoute][Required] int birdId)
        {
            string MemberBirdAPI_URL = "/api/Bird/" + birdId;

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            var memberBirdDetail = await methcall.CallMethodReturnObject<GetBirdResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: MemberBirdAPI_URL,
                _logger: _logger,
                accessToken: accToken
                );
            if (memberBirdDetail == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Bird Detail!). \n Bird Not Found!";
                return RedirectToAction("MemberProfile");
            }
            if (!memberBirdDetail.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberBirdDetail.Status + " , Error Message: " + memberBirdDetail.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Bird Detail!). \n"
                    + memberBirdDetail.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }

            dynamic memberBirdVM = new ExpandoObject();
            memberBirdVM.MemberBirdDetails = memberBirdDetail.Data;
            memberBirdVM.UpdateBird = methcall.GetValidationTempData<BirdViewModel>(this, TempData, Constants.Constants.UPDATE_BIRD_VALID, "updateBird", jsonOptions);
            return View(memberBirdVM);
        }
        [HttpPost("Bird/Create")]
        public async Task<IActionResult> MemberCreateBird(BirdViewModel createBird)
        {
            string MemberBirdAPI_URL = "/api/Bird/Create";

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_BIRD_VALID, createBird, jsonOptions);
                return RedirectToAction("MemberBird");
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            IFormFile photo = createBird.BirdMainImage;

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = $"bird/{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;

                createBird.ProfilePic = image;
            }

            createBird.BirdMainImage = null;
            createBird.MemberId = usrId;

            var memberBirdResponse = await methcall.CallMethodReturnObject<GetBirdResponse>(
                                _httpClient: _httpClient,
                                options: jsonOptions,
                                methodName: Constants.Constants.POST_METHOD,
                                url: MemberBirdAPI_URL,
                                inputType: createBird,
                                accessToken: accToken,
                                _logger: _logger);

            if (memberBirdResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Bird!).\n Not Found!";
                return RedirectToAction("MemberBird");
            }
            if (!memberBirdResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberBirdResponse.Status + " , Error Message: " + memberBirdResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Bird!).\n"
                    + memberBirdResponse.ErrorMessage;
                return RedirectToAction("MemberBird");
            }
            return RedirectToAction("MemberBird");
        }
        [HttpGet("Payment")]
        public async Task<IActionResult> MemberPayment()
        {
            string MemberPaymentAPI_URL = "/api/Transaction/AllTransactions";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            dynamic transactionModel = new ExpandoObject();

            var memberPayment = await methcall.CallMethodReturnObject<GetUserPaymentResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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
        [HttpGet("Notification")]
        public async Task<IActionResult> MemberNotification()
        {
            string MemberNotificationAPI_URL = "/api/Notification/AllNotifications";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string? NotificationReadAllAPI_URL = "/api/Notification/Update";

            #region NotificationBell
            // show read and unread notifications when you click on the bell in the header bar
            if (usrId != null)
            {
                string NotificationCountAPI_URL = "/api/Notification/Count";
                string NotificationUnreadAPI_URL = "/api/Notification/Unread";
                string NotificationReadAPI_URL = "/api/Notification/Read";

                var notificationCount = await methcall.CallMethodReturnObject<GetNotificationCountResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationCountAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationUnread = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationUnreadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                var notificationRead = await methcall.CallMethodReturnObject<GetNotificationTitleResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: "POST",
                url: NotificationReadAPI_URL,
                inputType: usrId,
                _logger: _logger);

                ViewBag.NotificationCount = notificationCount.IntData;
                ViewBag.NotificationUnread = notificationUnread.Data.ToList();
                ViewBag.NotificationRead = notificationRead.Data.ToList();
            }
            #endregion

            dynamic notificationModel = new ExpandoObject();

            var memberNotification = await methcall.CallMethodReturnObject<GetUserNotificationResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberNotificationAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (memberNotification == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member!). \n Member Not Found!";
                return RedirectToAction("MemberProfile");
            }
            if (!memberNotification.Status)
            {
                _logger.LogInformation("Error while processing your request: " + memberNotification.Status + " , Error Message: " + memberNotification.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Notification!). \n"
                    + memberNotification.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }

            var readAllNotifications = await methcall.CallMethodReturnObject<GetUserNotificationReadAll>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: NotificationReadAllAPI_URL,
                inputType: memberNotification.Data,
                _logger: _logger,
                accessToken: accToken);

            if (readAllNotifications == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Reading All Notifications!). \n Member Not Found!";
                return RedirectToAction("MemberProfile");
            }
            if (!readAllNotifications.Status)
            {
                _logger.LogInformation("Error while processing your request: " + readAllNotifications.Status + " , Error Message: " + readAllNotifications.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Reading All Notifications!). \n"
                    + readAllNotifications.ErrorMessage;
                return RedirectToAction("MemberProfile");
            }

            memberNotification = await methcall.CallMethodReturnObject<GetUserNotificationResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberNotificationAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            notificationModel.MemberNotifications = memberNotification.Data;
            return View(notificationModel);
        }
    }
}
