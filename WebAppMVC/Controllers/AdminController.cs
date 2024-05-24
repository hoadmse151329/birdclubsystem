using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using BAL.ViewModels;
using BAL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Net.Http.Headers;
using System.Text.Encodings.Web;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Member;
using WebAppMVC.Models.ViewModels;
using WebAppMVC.Models.Admin;
using BAL.ViewModels.Admin;

namespace WebAppMVC.Controllers
{
	[Route("Admin")]
	public class AdminController : Controller
	{
		private readonly ILogger<AdminController> _logger;
		private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private string AdminAPI_URL = "";
		private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
		{
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            PropertyNameCaseInsensitive = true
        };
        private readonly CookieOptions cookieOptions = new CookieOptions
        {
            Expires = DateTime.Now.AddMinutes(10),
            MaxAge = TimeSpan.FromMinutes(10),
            Secure = true,
            IsEssential = true,
        };
        private BirdClubLibrary methcall = new();

        public AdminController(ILogger<AdminController> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;
            _httpClient = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _httpClient.DefaultRequestHeaders.Accept.Add(contentType);
            _httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
            AdminAPI_URL = "/api/";
        }

        [HttpGet("Index")]
        public async Task<IActionResult> AdminIndex()
		{
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            return View();
        }
        [HttpGet("Account/Index")]
        public async Task<IActionResult> AdminAccountIndex([FromQuery] string? search)
        {
            AdminAPI_URL += "Admin/MemberStatus";
            if (!string.IsNullOrEmpty(search))
            {
                AdminAPI_URL += "?memberusername=" + search;
            }

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            AdminAccountIndexVM managerMemberStatusListVM = new();

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListEmployeeStatus>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.GET_METHOD,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listMemberStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Member Status!). List was Empty!: " + listMemberStatusResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("AdminIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n"
                    + listMemberStatusResponse.ErrorMessage;
                return View("AdminIndex");
            }
            managerMemberStatusListVM.EmployeeStatuses = listMemberStatusResponse.Data;

            foreach (var employee in managerMemberStatusListVM.EmployeeStatuses)
            {
                employee.DefaultEmployeeStatusSelectList = methcall.GetMemberStatusSelectableList(employee.Status);
                employee.DefaultEmployeeRoleSelectList = methcall.GetEmployeeRoleSelectableList(employee.Role);
            }
            return View(managerMemberStatusListVM);
        }
        [HttpPost("Account/Index/Update")]
        public async Task<IActionResult> AdminUpdateAccountIndex(List<GetEmployeeStatus> listRequest)
        {
            AdminAPI_URL += "Admin/MemberStatus/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            var listMemberStatusResponse = await methcall.CallMethodReturnObject<GetListEmployeeStatusUpdate>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                inputType: listRequest,
                url: AdminAPI_URL,
                accessToken: accToken,
                _logger: _logger);

            if (listMemberStatusResponse == null)
            {
                _logger.LogInformation(
                    "Error while processing your request! (Getting List Member Status!). List was Empty!: " + listMemberStatusResponse);
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("AdminIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting List Member Status!).\n"
                    + listMemberStatusResponse.ErrorMessage;
                return View("AdminIndex");
            }
            return RedirectToAction("AdminAccountIndex");
        }
        [HttpGet("Profile")]
        public async Task<IActionResult> AdminProfile()
        {
            AdminAPI_URL += "Admin/Profile";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            var memberDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: AdminAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (memberDetails == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                return RedirectToAction("AdminIndex");
            }
            else
            if (!memberDetails.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!"
                + memberDetails.ErrorMessage;
                return RedirectToAction("AdminIndex");
            }
            return View(memberDetails.Data);
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string AdminAvatarAPI_URL = "/api/User/Upload";

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
                    url: AdminAvatarAPI_URL,
                    _logger: _logger,
                    inputType: imageUpload,
                    accessToken: accToken);
                if (getMemberAvatar == null)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                }
                else
                if (!getMemberAvatar.Status)
                {
                    ViewBag.error =
                        "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!"
                    + getMemberAvatar.ErrorMessage;
                }
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
        [HttpPost("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> AdminProfileUpdate(MemberViewModel memberDetail)
        {
            AdminAPI_URL += "Admin/Profile/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            memberDetail.MemberId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: AdminAPI_URL,
                _logger: _logger,
                inputType: memberDetail,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("AdminProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.Error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword memberPassword)
        {
            string AdminChangePasswordAPI_URL = "/api/User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            memberPassword.userId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: AdminChangePasswordAPI_URL,
                _logger: _logger,
                inputType: memberPassword,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                return RedirectToAction("AdminProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                ViewBag.error =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
    }
}
