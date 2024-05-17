using BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels.Member;
using WebAppMVC.Models.Auth;
using WebAppMVC.Constants;
using Microsoft.AspNetCore.Authentication;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Models.Transaction;
using BAL.ViewModels;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;
using WebAppMVC.Models.Notification;
using WebAppMVC.Services.Interfaces;
namespace WebAppMVC.Controllers
{
    [Route("Auth")]
	public class AuthController : Controller
	{
		private readonly ILogger<AuthController> _logger;
		private readonly IConfiguration _config;
		private readonly HttpClient client = null;
		private readonly IVnPayService _vnPayService;
		private string AuthenAPI_URL = "";
        private BirdClubLibrary methcall = new();
        private readonly JsonSerializerOptions jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
		private readonly CookieOptions cookieOptions = new CookieOptions
		{
			Expires = DateTime.Now.AddMinutes(10),
			MaxAge = TimeSpan.FromMinutes(10),
			Secure = true,
			IsEssential = true,
		};
        public AuthController(ILogger<AuthController> logger, IConfiguration config, IVnPayService vnPayService)
		{
			_logger = logger;
			_config = config;
			client = new HttpClient();
			_vnPayService = vnPayService;
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			client.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
			AuthenAPI_URL = "/api/User";
		}
		[HttpGet("Register")]
		public async Task<IActionResult> Register()
		{
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            if (role == null) role = Constants.Constants.GUEST;

            TempData[Constants.Constants.ROLE_NAME] = role;

			var googleLoginDetails = await methcall.GetCookie<CreateNewMember>(Request, Constants.Constants.GOOGLE_ACC_COOKIE, jsonOptions);
			if(googleLoginDetails != null)
			{
				return View(googleLoginDetails);
			}
            return View();
		}
		[HttpGet("Login")]
		public IActionResult Login()
		{
            string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

            if (role == null) role = "Guest";

            TempData[Constants.Constants.ROLE_NAME] = role;

            return View();
		}
		
		[HttpGet("GoogleLogin")]
		public IActionResult GoogleLogin()
		{
			var properties = new AuthenticationProperties
			{
				RedirectUri = Url.Action("GoogleResponse")
			};
			return Challenge(properties, GoogleDefaults.AuthenticationScheme);
		}
		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			if (!result.Succeeded)
			{
                ViewBag.error = "Error while registering your new account (Failed to Login By Google)";
                return RedirectToAction("Login");
			}
			
			/*var claim = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
			{
				claim.Issuer,
				claim.OriginalIssuer,
				claim.Type,
				claim.Value,
			});*/

			var code = result.Properties.Items.FirstOrDefault(t => t.Key.Equals(Constants.Constants.GOOGLE_ACCESS_TOKEN_KEY_NAME)).Value;
			if(code == null)
			{
                ViewBag.error = "Error while registering your new account (Failed to Get Login Token)";
                return RedirectToAction("Login");
            }
			var userInfo = await GoogleUtils.GetUserInfo(code);
			methcall.SetCookie(Response, Constants.Constants.GOOGLE_ACC_COOKIE, userInfo, cookieOptions, jsonOptions, 20);

            return RedirectToAction("Register");
		}
		[HttpGet("Logout")]
        public IActionResult Logout()
        {
			client.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData[Constants.Constants.ACC_TOKEN] = null;
            TempData[Constants.Constants.ROLE_NAME] = null;
            TempData[Constants.Constants.USR_ID] = null;
			SignOut();

            // If using ASP.NET Identity, you may want to sign out the user
            // Example: await SignInManager.SignOutAsync();

            return RedirectToAction(actionName: "Index", controllerName:"Home");
        }
		[HttpPost("Authorize")]
		public async Task<IActionResult> Authorize(AuthenRequest authenRequest)
		{
            AuthenAPI_URL += "/Login";

            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
                _httpClient: client,
                options: jsonOptions,
                methodName: Constants.Constants.POST_METHOD,
                url: AuthenAPI_URL,
				inputType: authenRequest,
                _logger: _logger);

            if (authenResponse == null)
			{
                string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

                if (role == null) role = Constants.Constants.GUEST;

                TempData[Constants.Constants.ROLE_NAME] = role;
                _logger.LogInformation("Username or Password is invalid.");
                ViewBag.error = "Username or Password is invalid.";
				return View("Login");
			}
			var responseAuth = authenResponse.Data;

			if (authenResponse.Status)
			{
				HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, responseAuth.AccessToken);
				HttpContext.Session.SetString(Constants.Constants.ROLE_NAME, responseAuth.RoleName);
				HttpContext.Session.SetString(Constants.Constants.USR_ID, responseAuth.UserId);
                HttpContext.Session.SetString(Constants.Constants.USR_NAME, responseAuth.UserName);
				HttpContext.Session.SetString(Constants.Constants.USR_IMAGE, responseAuth.ImagePath);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

				TempData[Constants.Constants.ACC_TOKEN] = responseAuth.AccessToken;
				TempData[Constants.Constants.ROLE_NAME] = responseAuth.RoleName;
				TempData[Constants.Constants.USR_ID] = responseAuth.UserId;
				TempData[Constants.Constants.USR_NAME] = responseAuth.UserName;
				TempData[Constants.Constants.USR_IMAGE] = responseAuth.ImagePath;
			}
			if (responseAuth!.RoleName == Constants.Constants.ADMIN)
			{
				_logger.LogInformation("Admin Login Successful: " + TempData[Constants.Constants.ROLE_NAME] + " , Id: " + TempData[Constants.Constants.USR_ID]);
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.MANAGER)
			{
                _logger.LogInformation("Manager Login Successful: " + TempData[Constants.Constants.ROLE_NAME] + " , Id: " + TempData[Constants.Constants.USR_ID]);
                return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.STAFF)
			{
                _logger.LogInformation("Staff Login Successful: " + TempData[Constants.Constants.ROLE_NAME] + " , Id: " + TempData[Constants.Constants.USR_ID]);
                return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
                _logger.LogInformation("Member Login Successful: " + TempData[Constants.Constants.ROLE_NAME] + " , Id: " + TempData[Constants.Constants.USR_ID]);
                return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
		[HttpGet("ConfirmRegister")]
		//[Authorize(Roles = "TempMember")]
		public async Task<IActionResult> ConfirmRegister()
		{
			AuthenAPI_URL += "/Register";

			string TransactionAPI_URL = "/api/Transaction/UpdateUser";

			var newmemRequest = await methcall.GetCookie<CreateNewMember>(Request, Constants.Constants.NEW_MEMBER_REGISTRATION_COOKIE, jsonOptions);

			if (newmemRequest == null)
			{
				_logger.LogInformation("Error while registering your new account ! Please Try Again");
				ViewBag.error = "Error while registering your new account ! ";
				return View("Register");
			}

			methcall.RemoveCookie(Response, Constants.Constants.NEW_MEMBER_REGISTRATION_COOKIE, cookieOptions, jsonOptions);

			var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: Constants.Constants.POST_METHOD,
				url: AuthenAPI_URL,
				inputType: newmemRequest,
				_logger: _logger);

			if (authenResponse == null)
			{
				_logger.LogError("Error while registering your new account");

				ViewBag.error = "Error while registering your new account !";

				return View("Register");
			}

			var responseAuth = authenResponse.Data;

			var tran = await methcall.GetCookie<TransactionViewModel>(Request, Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE,jsonOptions);

			if (tran == null)
			{
				_logger.LogError("Error while registering your new account: Your Registration Transaction not found!");

				ViewBag.error = "Error while registering your new account: Your Registration Transaction not found! " +
					"\nPlease contact the birdclub manager for assistance with resolving this issue!";

				return View("Register");
			}
			methcall.RemoveCookie(Response, Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE, cookieOptions, jsonOptions);

			UpdateTransactionRequest unmtr = new UpdateTransactionRequest()
			{
				MemberId = responseAuth.UserId,
				TransactionId = tran.TransactionId
			};

			string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

			var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: Constants.Constants.PUT_METHOD,
				url: TransactionAPI_URL,
				inputType: unmtr,
				accessToken: accToken,
				_logger: _logger);

			if (transactionResponse == null)
			{
				_logger.LogError("Error while registering your new account: User Transaction Saving Failed!");

				ViewBag.error = "Error while registering your new account: User Transaction Saving Failed!, " +
					"\nPlease contact the birdclub manager for assistance with resolving this issue!";

				return View("Register");
			}
			if (authenResponse.Status)
			{
				HttpContext.Session.Remove(Constants.Constants.ACC_TOKEN);
				HttpContext.Session.Remove(Constants.Constants.USR_NAME);
				HttpContext.Session.Remove(Constants.Constants.ROLE_NAME);
			}
            ViewBag.Success = "Account Create Successfully, Please contact the manager for your account approval!";

			NotificationViewModel notif = new NotificationViewModel()
			{
				NotificationId = Guid.NewGuid().ToString(),
				Title = Constants.Constants.NOTIFICATION_TYPE_ACCOUNT_REGISTER,
				Description = Constants.Constants.NOTIFICATION_DESCRIPTION_ACCOUNT_REGISTER,
				Date = DateTime.Now,
				UserId = transactionResponse.Data.UserId,
				Status = Constants.Constants.NOTIFICATION_STATUS_UNREAD
			};
            string NotificationAPI_URL = "/api/Notification/CreateRegister";

            var notificationResponse = await methcall.CallMethodReturnObject<GetNotificationPostResponse>(
					_httpClient: client,
                    options: jsonOptions,
                    methodName: Constants.Constants.POST_METHOD,
                    url: NotificationAPI_URL,
                    inputType: notif,
                    accessToken: accToken,
                    _logger: _logger);

            if (notificationResponse == null)
            {
                ViewBag.Error =
                    "Error while processing your request! (Create Notification).\n User Not Found!";
                return RedirectToAction("Login", "Auth");
            }
            if (!notificationResponse.Status)
            {
                _logger.LogInformation("Error while processing your request: " + notificationResponse.Status + " , Error Message: " + notificationResponse.ErrorMessage);
                ViewBag.Error =
                    "Error while processing your request! (Create Notification!).\n"
                    + notificationResponse.ErrorMessage;
                return RedirectToAction("Login", "Auth");
            }
            return RedirectToAction("Login", "Auth");
		}
		[HttpPost("Register")]
		public async Task<IActionResult> RegisterMember(CreateNewMember newmemRequest)
		{
			AuthenAPI_URL += "/RegisterTempMember";

            if (!TryValidateModel(newmemRequest))
            {
                ViewBag.Error =
                "Error while processing your request! (Registering New Member!).\n Validation Failed!";
                return View("Register");
            }
            var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: Constants.Constants.POST_METHOD,
				url: AuthenAPI_URL,
				inputType: newmemRequest,
				_logger: _logger);

			if (authenResponse == null)
			{
				_logger.LogInformation("Error while registering your new account: ");
				ViewBag.error = "Error while registering your new account ! ";
				return View("Register");
			}
            var responseAuth = authenResponse.Data;

			if (authenResponse.Status)
			{
				HttpContext.Session.SetString(Constants.Constants.ACC_TOKEN, responseAuth.AccessToken);
				HttpContext.Session.SetString(Constants.Constants.ROLE_NAME, responseAuth.RoleName);
				HttpContext.Session.SetString(Constants.Constants.USR_NAME, responseAuth.UserName);

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);
			}

			methcall.SetCookie(Response, Constants.Constants.NEW_MEMBER_REGISTRATION_COOKIE, newmemRequest, cookieOptions, jsonOptions, 20);
			PaymentInformationModel model = new PaymentInformationModel()
			{
				Fullname = newmemRequest.FullName,
				PayAmount = newmemRequest.PayAmount,
				TransactionType = Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE,
			};
			var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
			return Redirect(url);
		}
	}
}
