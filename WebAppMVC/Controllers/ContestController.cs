using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using BAL.ViewModels;
using BAL.ViewModels.Authenticates;
using DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Options;
using System.ComponentModel.DataAnnotations;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Bird;
using WebAppMVC.Models.Contest;
using WebAppMVC.Models.FieldTrip;
using WebAppMVC.Models.Location;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Member;
using WebAppMVC.Models.Notification;
using WebAppMVC.Models.Transaction;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Services.Interfaces;
using BAL.ViewModels.Event;
using WebAppMVC.Models.ViewModels;

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
            MemberContestIndexVM contestListVM = new();

            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

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

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listContestResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse + " , Error Message: " + listContestResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else
            if (!listContestResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Meeting!).\n"
                    + listContestResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }

            contestListVM.Contests = listContestResponse.Data;
            contestListVM.Roads = listContestResponse.Data.Select(c => c.Street).Distinct().ToList();
            contestListVM.Districts = listContestResponse.Data.Select(c => c.District).Distinct().ToList();
            contestListVM.Cities = listContestResponse.Data.Select(c => c.City).Distinct().ToList();

            return View(contestListVM);
		}

        [HttpGet("Index/Filter")]
        public async Task<IActionResult> IndexFilter(
            [FromQuery] string? eloRange,
            [FromQuery] List<string>? road,
            [FromQuery] List<string>? district,
            [FromQuery] List<string>? city
            )
        {
            if ((road == null || road.Count == 0) && (district == null || district.Count == 0) && (city == null || city.Count == 0)) ContestAPI_URL += "/All";
            else ContestAPI_URL += "/Search?";

            MemberContestIndexVM contestFilteredVM = new();

            if (eloRange != null && !string.IsNullOrEmpty(eloRange.Trim()))
            {
                string[] eloFilter = eloRange.Split("--");
                string minElo = eloFilter[0];
                string maxElo = eloFilter[1];
                ContestAPI_URL += $"reqMinElo={Uri.EscapeDataString(minElo.Trim())}&reqMaxElo={Uri.EscapeDataString(maxElo.Trim())}";
                contestFilteredVM.SelectedReqEloRange = methcall.GetReqEloRangeSelectableList(eloRange);
            }
            if (road != null && road.Any())
            {
                foreach (var selectedRoad in road)
                {
                    if (selectedRoad != null)
                    {
                        ContestAPI_URL += $"road={Uri.EscapeDataString(selectedRoad.Trim())}&";
                        contestFilteredVM.SelectedRoads.Add(selectedRoad);
                    }
                }
            }
            if (district != null && district.Any())
            {
                foreach (var selectedDistrict in district)
                {
                    if (selectedDistrict != null)
                    {
                        ContestAPI_URL += $"district={Uri.EscapeDataString(selectedDistrict.Trim())}&";
                        contestFilteredVM.SelectedDistricts.Add(selectedDistrict);
                    }
                }
            }
            if (city != null && city.Any())
            {
                foreach (var selectedCity in city)
                {
                    if (selectedCity != null)
                    {
                        ContestAPI_URL += $"city={Uri.EscapeDataString(selectedCity.Trim())}&";
                        contestFilteredVM.SelectedCities.Add(selectedCity);
                    }
                }
            }
            if (ContestAPI_URL.Contains("Search"))
            {
                ContestAPI_URL = ContestAPI_URL.Substring(0, ContestAPI_URL.Length - 1); // Remove the trailing '&'
            }

            methcall.SetUserDefaultData(this);

            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            var listContestResponse = await methcall.CallMethodReturnObject<GetContestResponseByList>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                inputType: role,
                _logger: _logger);

            if (listContestResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Contest!). List was Empty!: " + listContestResponse + " , Error Message: " + listContestResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n List was Empty!";
                Redirect("~/Home/Index");
            }
            else if (!listContestResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting List Contest!).\n"
                    + listContestResponse.ErrorMessage;
                Redirect("~/Home/Index");
            }
            contestFilteredVM.Contests = listContestResponse.Data;

            contestFilteredVM.Roads = listContestResponse.Data.Select(m => m.Street).Distinct().ToList();
            contestFilteredVM.Districts = listContestResponse.Data.Select(m => m.District).Distinct().ToList();
            contestFilteredVM.Cities = listContestResponse.Data.Select(m => m.City).Distinct().ToList();

            return PartialView("_ContestListPartial", contestFilteredVM);
        }

        [HttpGet("Post/{id:int}")]
        public async Task<IActionResult> ContestPost(
            [FromRoute][Required] int id
            )
        {
            ContestAPI_URL += "/";

            methcall.SetUserDefaultData(this);
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);
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

            dynamic contestPostAndBird = new ExpandoObject();

            GetContestPostResponse? contestPostResponse = new();

            if (!string.IsNullOrEmpty(accToken) && !string.IsNullOrEmpty(usrId) && role.Equals(Constants.Constants.MEMBER))
            {   
                ContestAPI_URL += id +"/Participant";
                contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.POST_METHOD,
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
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            }
            if (contestPostResponse == null)
            {
                //_logger.LogInformation("Username or Password is invalid: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return View("Index");
            }
            if (!contestPostResponse.Status)
            {
                _logger.LogInformation("Username or Password is invalid: " + contestPostResponse.Status + " , Error Message: " + contestPostResponse.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return View("Index");
            }
            contestPostAndBird.ContestDetails = contestPostResponse.Data;
            /*if(birdForContestRegistration != null)
            {
                birdForContestRegistration.BirdMainImage = await methcall.GetCookieForTempFile(Request, Constants.Constants.CREATE_OR_UPDATE_BIRD_PROFILE_PICTURE_VALID, jsonOptions);
                methcall.RemoveCookieTempFile(Response, Constants.Constants.CREATE_OR_UPDATE_BIRD_PROFILE_PICTURE_VALID, birdForContestRegistration.BirdMainImage, cookieOptions);
            }*/
            contestPostAndBird.CreateBirdForContest = methcall.GetValidationTempData<BirdViewModel>(this, TempData, Constants.Constants.CREATE_CONTEST_PARTICIPATION_VALID, "createOrSelectedBird", jsonOptions);
            return View(contestPostAndBird);
        }

        [HttpPost("{contestId:int}/Register")]
        public async Task<IActionResult> ContestRegister(
            [FromRoute][Required] int contestId,
            [Required] BirdViewModel selectedBird
            )
        {
            ContestAPI_URL += "/" + contestId;
            string MemberAPI_URL = "/api/Member/Profile";
            string BirdAPI_URL = "/api/Bird/" + selectedBird.BirdId;
            
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            if (!ModelState.IsValid)
            {
                //methcall.SetCookieForTempFile(Response, Constants.Constants.CREATE_OR_UPDATE_BIRD_PROFILE_PICTURE_VALID, createOrSelectedBird.BirdMainImage, cookieOptions, jsonOptions);
                selectedBird.BirdMainImage = null;
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_CONTEST_PARTICIPATION_VALID, selectedBird, jsonOptions);

                return RedirectToAction("ContestPost", new { id = contestId });
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);
            /*selectedBird.MemberId = usrId;
            if (selectedBird.BirdMainImage != null && selectedBird.BirdMainImage.Length > 0 )
            {
                string connectionString = _config.GetSection("AzureStorage:BlobConnectionString").Value;
                string containerName = _config.GetSection("AzureStorage:BlobContainerName").Value;
                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = selectedBird.BirdMainImage.FileName;
                string uniqueBlobName = $"bird/{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    selectedBird.BirdMainImage.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = "https://edwinbirdclubstorage.blob.core.windows.net/images/" + uniqueBlobName;
                selectedBird.ProfilePic = image;
                selectedBird.BirdMainImage = null;
            }*/
            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            var birdDetails = await methcall.CallMethodReturnObject<GetBirdResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: BirdAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (contestPostResponse == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Contest!).\n Contest Not Found!";
                return RedirectToAction("Index");
            }
            if (!contestPostResponse.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Contest Post!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("Index");
            }
            if (memberDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member!).\n Member Not Found!";
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            if (!memberDetails.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member!).\n"
                    + contestPostResponse.ErrorMessage;
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            if (birdDetails == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Bird for Contest Registration!).\n Bird Not Found!";
                return RedirectToAction("ContestPost",new { id = contestId});
            }
            if (!birdDetails.Status)
            {
                ViewBag.error =
                   "Error while processing your request! (Getting Bird for Contest Registration!).\n";
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            if (birdDetails.Data.Elo < contestPostResponse.Data.ReqMinELO && birdDetails.Data.Elo > contestPostResponse.Data.ReqMaxELO)
            {
                ViewBag.error =
                   "Error while processing your request! (Your Bird Elo must be more than "
                   + contestPostResponse.Data.ReqMinELO + " and less than "
                   + contestPostResponse.Data.ReqMaxELO + " to register a Contest!).\n";
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            methcall.SetCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_COOKIE, contestPostResponse.Data, cookieOptions, jsonOptions, 20);

            methcall.SetCookie(Response, Constants.Constants.MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE, birdDetails.Data, cookieOptions, jsonOptions, 20);

            PaymentInformationModel model = new PaymentInformationModel()
            {
                Fullname = memberDetails.Data.FullName,
                PayAmount = (decimal)contestPostResponse.Data.Fee,
                TransactionType = Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_TYPE
            };

            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
            return Redirect(url);
        }

        [HttpGet("ConfirmRegister")]
        public async Task<IActionResult> ContestConfirmRegister()
        {
            var contest = await methcall.GetCookie<ContestViewModel>(Request, Constants.Constants.MEMBER_CONTEST_REGISTRATION_COOKIE, jsonOptions);

            if (contest == null)
            {
                return RedirectToAction("Index");
            }
            int conId = contest.ContestId.Value;
            string conName = contest.ContestName;

            var bird = await methcall.GetCookie<BirdViewModel>(Request, Constants.Constants.MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE, jsonOptions);

            if (bird == null)
            {
                return RedirectToAction("Index");
            }
            int birdId = bird.BirdId.Value;

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_COOKIE, cookieOptions, jsonOptions);
            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_CONTEST_BIRD_REGISTRATION_COOKIE, cookieOptions, jsonOptions);

            ContestAPI_URL += "/" + conId + "/Bird/" + birdId + "/Register";

            string TransactionAPI_URL = "/api/Transaction/UpdateUser";
            string MemberAPI_URL = "/api/Member/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: MemberAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            var participationNo = await methcall.CallMethodReturnObject<GetContestParticipationNo>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: ContestAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);

            if (participationNo == null)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): Contest Not Found!");
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n Contest Not Found!";
                return RedirectToAction("ContestPost", new { id = conId });
            }
            else
            if (!participationNo.Status)
            {
                _logger.LogInformation("Error while processing your request! (Registering Contest Participation!): " + participationNo.Status + " , Error Message: " + participationNo.ErrorMessage);
                ViewBag.error =
                    "Error while processing your request! (Registering Contest Participation!).\n"
                    + participationNo.ErrorMessage;
                return RedirectToAction("ContestPost", new { id = conId });
            }

            var tran = await methcall.GetCookie<TransactionViewModel>(Request, Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE, jsonOptions);

            if (tran == null)
            {
                _logger.LogError("Error while registering your bird for contest participation: Your Registration Transaction not found!");

                ViewBag.error = "Error while registering your bird for contest participation: Your Registration Transaction not found! " +
                    "\nPlease contact birdclub manager for assistance with resolving this issue";

                return RedirectToAction("ContestPost", new { id = conId });
            }

            methcall.RemoveCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE, cookieOptions, jsonOptions);

            UpdateTransactionRequest unmtr = new UpdateTransactionRequest()
            {
                MemberId = memberDetails.Data.MemberId,
                TransactionId = tran.TransactionId
            };

            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: TransactionAPI_URL,
                inputType: unmtr,
                accessToken: accToken,
                _logger: _logger);

            if (transactionResponse == null)
            {
                _logger.LogError("Error while registering your bird for contest participation: User Transaction Saving Failed!");

                ViewBag.error = "Error while registering your bird for contest participation: User Transaction Saving Failed!, " +
                    "\nPlease contact the birdclub manager for assistance with resolving this issue!";

                return RedirectToAction("ContestPost", new { id = conId });
            }

            CreateNotificationRequest notif = new CreateNotificationRequest()
            {
                Title = Constants.Constants.NOTIFICATION_TYPE_CONTEST_REGISTER,
                Description = Constants.Constants.NOTIFICATION_DESCRIPTION_CONTEST_REGISTER + conName,
                MemberId = usrId
            };

            string NotificationAPI_URL = "/api/Notification/CreateEvent";

            var notificationResponse = await methcall.CallMethodReturnObject<GetNotificationPostResponse>(
                    _httpClient: _httpClient,
                    options: jsonOptions,
                    methodName: "POST",
                    url: NotificationAPI_URL,
                    inputType: notif,
                    accessToken: accToken,
                    _logger: _logger);

            if (notificationResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Notification).\n User Not Found!";
                return RedirectToAction("ContestPost", new { id = conId });
            }
            if (!notificationResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + notificationResponse.Status + " , Error Message: " + notificationResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Notification!).\n"
                    + notificationResponse.ErrorMessage;
                return RedirectToAction("ContestPost", new { id = conId });
            }
            TempData["Success"] = participationNo.SuccessMessage;
            return RedirectToAction("ContestPost", new { id = conId });
        }

        [HttpPost("{contestId:int}/DeRegister")]
        public async Task<IActionResult> ContestDeRegister(
            [FromRoute][Required] int contestId
            )
        {
            ContestAPI_URL += "/" + contestId + "/Participant/Remove";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.MEMBER));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var participationNo = await methcall.CallMethodReturnObject<GetContestPostDeRegister>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
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

            var contestPostResponse = await methcall.CallMethodReturnObject<GetContestPostResponse>(
                                   _httpClient: _httpClient,
                                   options: jsonOptions,
                                   methodName: Constants.Constants.GET_METHOD,
                                   url: ContestAPI_URL,
                                   _logger: _logger);

            CreateNotificationRequest notif = new CreateNotificationRequest()
            {
                Title = Constants.Constants.NOTIFICATION_TYPE_CONTEST_DEREGISTER,
                Description = Constants.Constants.NOTIFICATION_DESCRIPTION_CONTEST_DEREGISTER + contestPostResponse.Data.ContestName,
                MemberId = usrId
            };

            string NotificationAPI_URL = "/api/Notification/CreateEvent";

            var notificationResponse = await methcall.CallMethodReturnObject<GetNotificationPostResponse>(
                    _httpClient: _httpClient,
                    options: jsonOptions,
                    methodName: "POST",
                    url: NotificationAPI_URL,
                    inputType: notif,
                    accessToken: accToken,
                    _logger: _logger);

            if (notificationResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Notification).\n User Not Found!";
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            if (!notificationResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + notificationResponse.Status + " , Error Message: " + notificationResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Notification!).\n"
                    + notificationResponse.ErrorMessage;
                return RedirectToAction("ContestPost", new { id = contestId });
            }
            TempData["Success"] = participationNo.SuccessMessage;
            return RedirectToAction("ContestPost", new { id = contestId });
        }
        public IActionResult LeaderBoard()
		{
			return View();
		}
        /*[HttpPost]
        public ActionResult UpdatePlayerElo(double playerElo, List<double> playerElos, List<int> birdPoints)
        {
            // Count the number of players participating
            int n = playerElos.Count;
            // Calculate the total points earned by all birds
            int totalPoints = birdPoints.Sum();
            // Calculate the average Elo of all players
            double averageElo = playerElos.Sum() / n;

            // List of Elo change factors
            List<double> Y = new List<double> { 40, 35, 30, 25, 20 }; // Adjust this list as needed
            // Calculate basic Elo change for the player based on the provided parameters
            double basicEloChange = EloCalculator.CalculateBasicEloChange(playerElo, averageElo, birdPoints[0], totalPoints, n, Y);
            // Update the player's Elo using the calculated Elo change
            double updatedElo = EloCalculator.UpdateElo(playerElo, basicEloChange);

            // Update player's Elo in your database or wherever it's stored

            // Return the updated Elo as JSON response
            return Json(new { updatedElo });
        }*/
    }
}
