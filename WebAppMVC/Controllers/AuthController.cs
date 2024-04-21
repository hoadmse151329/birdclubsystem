﻿using BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels.Member;
using WebAppMVC.Models.Auth;
using System.Net.Http;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Constants;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.Cookies;
using WebAppMVC.Services;
using WebAppMVC.Models.VnPay;

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
		public IActionResult Register()
		{
			return View();
		}
		[HttpGet("Login")]
		public IActionResult Login()
		{
			return View();
		}

        #region Old Google Login Code (Obsolete)
        /*public async Task GoogleLogin()
		{
			await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
				new AuthenticationProperties
				{
					RedirectUri = Url.Action("GoogleResponse")
				});
		}
		public async Task<IActionResult> GoogleResponse()
		{
			var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);
			
			var claims = result.Principal.Identities.FirstOrDefault().Claims.Select(claim => new
			{
				claim.Issuer,
				claim.OriginalIssuer,
				claim.Type,
				claim.Value
			});
			*//*if (result.Succeeded)
			{
				
			}
			var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: "POST",
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
				HttpContext.Session.SetString("ACCESS_TOKEN", responseAuth.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", responseAuth.RoleName);
				HttpContext.Session.SetString("USER_ID", responseAuth.UserId);
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);
				TempData["ACCESS_TOKEN"] = responseAuth.AccessToken;
				TempData["ROLE_NAME"] = responseAuth.RoleName;
				TempData["USER_ID"] = responseAuth.UserId;
			}
			if (responseAuth!.RoleName == Constants.Constants.ADMIN)
			{
				_logger.LogInformation("Admin Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.MANAGER)
			{
				_logger.LogInformation("Manager Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.STAFF)
			{
				_logger.LogInformation("Staff Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
				_logger.LogInformation("Member Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MEMBER_URL);
			}*//*
			return base.Redirect(Constants.Constants.MEMBER_URL);
		}*/
        #endregion

        [HttpGet("Logout")]
        public IActionResult Logout()
        {
			client.DefaultRequestHeaders.Authorization = null;
            HttpContext.Session.Clear();
            TempData["ACCESS_TOKEN"] = null;
            TempData["ROLE_NAME"] = null;
            TempData["USER_ID"] = null;

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
                methodName: "POST",
                url: AuthenAPI_URL,
				inputType: authenRequest,
                _logger: _logger);

            if (authenResponse == null)
			{
                _logger.LogInformation("Username or Password is invalid.");
                ViewBag.error = "Username or Password is invalid.";
				return View("Login");
			}
			var responseAuth = authenResponse.Data;

			if (authenResponse.Status)
			{
				HttpContext.Session.SetString("ACCESS_TOKEN", responseAuth.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", responseAuth.RoleName);
				HttpContext.Session.SetString("USER_ID", responseAuth.UserId);
                HttpContext.Session.SetString("USER_NAME", responseAuth.UserName);
				HttpContext.Session.SetString("IMAGE_PATH", responseAuth.ImagePath);
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

				TempData["ACCESS_TOKEN"] = responseAuth.AccessToken;
				TempData["ROLE_NAME"] = responseAuth.RoleName;
				TempData["USER_ID"] = responseAuth.UserId;
				TempData["USER_NAME"] = responseAuth.UserName;
				TempData["IMAGE_PATH"] = responseAuth.ImagePath;
			}
			if (responseAuth!.RoleName == Constants.Constants.ADMIN)
			{
				_logger.LogInformation("Admin Login Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.MANAGER)
			{
                _logger.LogInformation("Manager Login Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
                return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.STAFF)
			{
                _logger.LogInformation("Staff Login Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
                return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
                _logger.LogInformation("Member Login Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
                return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
		[HttpGet("Registration")]
		public async Task<IActionResult> FinishingRegistration()
		{
			AuthenAPI_URL += "/Register";

			var newmemRequest = await methcall.GetCookie<CreateNewMember>(Request, "memRequest", jsonOptions);

			if (newmemRequest == null)
			{
				_logger.LogInformation("Error while registering your new account ! Please Try Again");
				ViewBag.error = "Error while registering your new account ! ";
				return View("Register");
			}

			var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: "POST",
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
				HttpContext.Session.SetString("ACCESS_TOKEN", responseAuth.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", responseAuth.RoleName);
				HttpContext.Session.SetString("USER_ID", responseAuth.UserId);
				HttpContext.Session.SetString("USER_NAME", responseAuth.UserName);
				HttpContext.Session.SetString("IMAGE_PATH", responseAuth.ImagePath);

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

				TempData["ACCESS_TOKEN"] = responseAuth.AccessToken;
				TempData["ROLE_NAME"] = responseAuth.RoleName;
				TempData["USER_ID"] = responseAuth.UserId;
				TempData["USER_NAME"] = responseAuth.UserName;
				TempData["IMAGE_PATH"] = responseAuth.ImagePath;
			}
			if (responseAuth!.RoleName == Constants.Constants.ADMIN)
			{
				_logger.LogInformation("Admin Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.MANAGER)
			{
				_logger.LogInformation("Manager Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.STAFF)
			{
				_logger.LogInformation("Staff Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
				_logger.LogInformation("Member Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
		[HttpPost("ConfirmRegistration")]
		public IActionResult ConfirmPaymentforRegistration(CreateNewMember newmemRequest)
		{
			methcall.SetCookie(Response, "memRequest", newmemRequest, cookieOptions, jsonOptions, 15);
			PaymentInformationModel model = new PaymentInformationModel()
			{
				Fullname = newmemRequest.FullName,
				PayAmount = newmemRequest.PayAmount,
				TransactionType = "New-Membership-Registration",
			};
			var url = _vnPayService.CreatePaymentUrl(model, HttpContext);
			return Redirect(url);
		}
		[HttpPost("SignUp")]
		public async Task<IActionResult> SignUp(CreateNewMember newmemRequest)
		{
			AuthenAPI_URL += "/Register";

			var authenResponse = await methcall.CallMethodReturnObject<GetAuthenResponse>(
				_httpClient: client,
				options: jsonOptions,
				methodName: "POST",
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
				HttpContext.Session.SetString("ACCESS_TOKEN", responseAuth.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", responseAuth.RoleName);
				HttpContext.Session.SetString("USER_ID", responseAuth.UserId);
				HttpContext.Session.SetString("USER_NAME", responseAuth.UserName);

				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", responseAuth.AccessToken);

				TempData["ACCESS_TOKEN"] = responseAuth.AccessToken;
				TempData["ROLE_NAME"] = responseAuth.RoleName;
				TempData["USER_ID"] = responseAuth.UserId;
				TempData["USER_NAME"] = responseAuth.UserName;
			}
			if (responseAuth!.RoleName == Constants.Constants.ADMIN)
			{
				_logger.LogInformation("Admin Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.MANAGER)
			{
				_logger.LogInformation("Manager Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (responseAuth!.RoleName == Constants.Constants.STAFF)
			{
				_logger.LogInformation("Staff Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
				_logger.LogInformation("Member Register Successful: " + TempData["ROLE_NAME"] + " , Id: " + TempData["USER_ID"]);
				return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
	}
}
