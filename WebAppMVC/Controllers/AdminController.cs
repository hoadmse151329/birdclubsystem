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
using BAL.ViewModels.Authenticates;
using WebAppMVC.Models.Auth;
using WebAppMVC.Models.Notification;
using WebAppMVC.Models.Transaction;
using System.ComponentModel.DataAnnotations;

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
            AdminAPI_URL = config.GetSection("DefaultApiUrl:ApiConnectionString").Value;
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
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("AdminIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Member Status!).\n"
                    + listMemberStatusResponse.ErrorMessage;
                return View("AdminIndex");
            }
            managerMemberStatusListVM.EmployeeStatuses = listMemberStatusResponse.Data;
            managerMemberStatusListVM.createEmployee = methcall.GetValidationTempData<CreateNewEmployee>(this, TempData, Constants.Constants.CREATE_EMPLOYEE_DETAILS_VALID, "createEmployee", jsonOptions);
            if(managerMemberStatusListVM.createEmployee == null)
            {
                managerMemberStatusListVM.createEmployee = new CreateNewEmployee();
            }
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
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting List Member Status!).\n List was Empty!";
                return View("AdminIndex");
            }
            else
            if (!listMemberStatusResponse.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
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
            string? imagePath = HttpContext.Session.GetString(Constants.Constants.USR_IMAGE);

            var adminInvalids = new AdminProfileVM();
            var memberInvalidDetails = methcall.GetValidationTempData<MemberViewModel>(this, TempData, Constants.Constants.UPDATE_ADMIN_DETAILS_VALID, "adminDetail", jsonOptions);
            if (memberInvalidDetails != null)
            {
                memberInvalidDetails.ImagePath = imagePath;
                memberInvalidDetails.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(memberInvalidDetails.Gender != null ? memberInvalidDetails.Gender : Constants.Constants.MALE);
                adminInvalids.adminDetail = memberInvalidDetails;
                return View(adminInvalids);
            }

            var adminDetails = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: AdminAPI_URL,
                _logger: _logger,
                inputType: usrId,
                accessToken: accToken);
            if (adminDetails == null || adminDetails.Data == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                return RedirectToAction("AdminIndex");
            }
            else
            if (!adminDetails.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!"
                + adminDetails.ErrorMessage;
                return RedirectToAction("AdminIndex");
            }
            var adminInvalidPasswordUpdate = methcall.GetValidationTempData<UpdateMemberPassword>(this, TempData, Constants.Constants.UPDATE_ADMIN_PASSWORD_VALID, "adminPassword", jsonOptions);
            if (adminInvalidPasswordUpdate != null)
            {
                adminInvalids.adminPassword = adminInvalidPasswordUpdate;
            }
            adminDetails.Data.DefaultUserGenderSelectList = methcall.GetUserGenderSelectableList(adminDetails.Data.Gender);
            adminInvalids.adminDetail = adminDetails.Data;
            return View(adminInvalids);
        }
        [HttpPost("Upload")]
        public async Task<IActionResult> UploadImage(IFormFile photo)
        {
            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            string AdminAvatarAPI_URL = AdminAPI_URL + "User/Upload";

            if (photo != null && photo.Length > 0)
            {
                string connectionString = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_CONNECTION_STRING);
                string defaultUrl = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_URL);
                string containerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_DEFAULT_BLOB_FOLDER_NAME);
                string avatarContainerName = _config.GetValue<string>(Constants.Constants.SYSTEM_DEFAULT_AZURE_BLOB_AVATAR_FOLDER_URL);

                BlobServiceClient _blobServiceClient = new BlobServiceClient(connectionString);
                BlobContainerClient _blobContainerClient = _blobServiceClient.GetBlobContainerClient(containerName);

                var azureResponse = new List<BlobContentInfo>();
                string filename = photo.FileName;
                string uniqueBlobName = avatarContainerName + $"{Guid.NewGuid()}-{filename}";
                using (var memoryStream = new MemoryStream())
                {
                    photo.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    var client = await _blobContainerClient.UploadBlobAsync(uniqueBlobName, memoryStream);
                    azureResponse.Add(client);
                }

                var image = defaultUrl + uniqueBlobName;
                UpdateMemberAvatar imageUpload = new(memberId: usrId, imagePath: image);

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
                    TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                        "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                }
                else
                if (!getMemberAvatar.Status)
                {
                    TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                        "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!"
                    + getMemberAvatar.ErrorMessage;
                }
                TempData[Constants.Constants.ALERT_DEFAULT_SUCCESS_NAME] = Constants.Constants.ALERT_USER_AVATAR_IMAGE_UPDATE_SUCCESS;
                HttpContext.Session.SetString(Constants.Constants.USR_IMAGE, getMemberAvatar.Data.ImagePath);
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
        [HttpPost("Profile")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> AdminProfileUpdate(
            [FromForm]MemberViewModel adminDetail
            )
        {
            AdminAPI_URL += "Admin/Profile/Update";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_ADMIN_DETAILS_VALID, adminDetail, jsonOptions);
                return RedirectToAction("AdminProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);
            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            adminDetail.MemberId = usrId;

            var memberDetailupdate = await methcall.CallMethodReturnObject<GetMemberProfileResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: AdminAPI_URL,
                _logger: _logger,
                inputType: adminDetail,
                accessToken: accToken);
            if (memberDetailupdate == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!";
                return RedirectToAction("AdminProfile");
            }
            else
            if (!memberDetailupdate.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Member Profile!).\n Member Details Not Found!"
                + memberDetailupdate.ErrorMessage;
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
        [HttpPost("ChangePassword")]
        //[Authorize(Roles = "Member")]
        public async Task<IActionResult> ChangePassword(UpdateMemberPassword adminPassword)
        {
            string AdminChangePasswordAPI_URL = AdminAPI_URL + "User/ChangePassword";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));
            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.UPDATE_ADMIN_PASSWORD_VALID, adminPassword, jsonOptions);
                return RedirectToAction("AdminProfile");
            }

            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            string? usrId = HttpContext.Session.GetString(Constants.Constants.USR_ID);

            adminPassword.userId = usrId;

            var adminPasswordupdate = await methcall.CallMethodReturnObject<GetMemberPasswordChangeResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.PUT_METHOD,
                url: AdminChangePasswordAPI_URL,
                _logger: _logger,
                inputType: adminPassword,
                accessToken: accToken);
            if (adminPasswordupdate == null)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n Admin Details Not Found!";
                return RedirectToAction("AdminProfile");
            }
            else
            if (!adminPasswordupdate.Status)
            {
                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] =
                    "Error while processing your request! (Getting Admin Profile!).\n"
                + adminPasswordupdate.ErrorMessage;
                return RedirectToAction("AdminProfile");
            }
            return RedirectToAction("AdminProfile");
        }
        [HttpPost("Account/Create")]
        //[Authorize(Roles = "TempMember")]
        public async Task<IActionResult> AdminCreateEmployee(
            [Required] CreateNewEmployee createEmployee)
        {
            AdminAPI_URL += "Admin/User/Create";

            if (methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN) != null)
                return Redirect(methcall.GetUrlStringIfUserSessionDataInValid(this, Constants.Constants.ADMIN));
            string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

            if (!ModelState.IsValid)
            {
                TempData = methcall.SetValidationTempData(TempData, Constants.Constants.CREATE_EMPLOYEE_DETAILS_VALID, createEmployee, jsonOptions);
                return RedirectToAction("AdminAccountIndex");
            }
            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: _httpClient,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: AdminAPI_URL,
                inputType: createEmployee,
                accessToken: accToken,
                _logger: _logger);

            if (authenResponse == null)
            {
                _logger.LogError("Error while registering employee new account");

                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering employee new account !";

                return RedirectToAction("AdminAccountIndex");
            }
            if (authenResponse.Status)
            {
                _logger.LogError("Error while registering employee new account");

                TempData[Constants.Constants.ALERT_DEFAULT_ERROR_NAME] = "Error while registering employee new account !";

                return RedirectToAction("AdminAccountIndex");
            }
            TempData["Success"] = ViewBag.Success = "Account Create Successfully!";

            return RedirectToAction("AdminAccountIndex");
        }
    }
}
