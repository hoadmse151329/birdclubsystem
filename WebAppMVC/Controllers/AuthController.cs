using BAL.ViewModels.Authenticates;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Text;
using BAL.ViewModels.Member;
using WebAppMVC.Models.Auth;

namespace WebAppMVC.Controllers
{
	public class AuthController : Controller
	{
		private readonly ILogger<AuthController> _logger;
		private readonly HttpClient client = null;
		private string AuthenAPI_URL = "";
		public AuthController(ILogger<AuthController> logger)
		{
			_logger = logger;
			client = new HttpClient();
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			client.DefaultRequestHeaders.Accept.Add(contentType);
			client.BaseAddress = new Uri("https://localhost:7022");
			AuthenAPI_URL = "/api/User";
		}
		public IActionResult Register()
		{
			return View();
		}
		public IActionResult Login()
		{
			return View();
		}
		[HttpPost]
		public async Task<IActionResult> Authorize(AuthenRequest authenRequest)
		{
			var json = JsonSerializer.Serialize(authenRequest);
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			AuthenAPI_URL += "/Login";
			HttpResponseMessage response = await client.PostAsync(AuthenAPI_URL, content);
			if (!response.IsSuccessStatusCode)
			{
				ViewBag.error = "Username or Password is invalid.";
				return View("Login");
			}
			string jsonResponse = await response.Content.ReadAsStringAsync();
			var authenResponse = JsonSerializer.Deserialize<GetAuthenResponse>(jsonResponse, options);
			var reponseAuthen = authenResponse.Data;
			if (authenResponse.Status)
			{
				HttpContext.Session.SetString("ACCESS_TOKEN", reponseAuthen.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", reponseAuthen.RoleName);
				HttpContext.Session.SetString("USER_ID", reponseAuthen.UserId.ToString());
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reponseAuthen.AccessToken);
				TempData["ACCESS_TOKEN"] = reponseAuthen.AccessToken;
				TempData["ROLE_NAME"] = reponseAuthen.RoleName;
				TempData["USER_ID"] = reponseAuthen.UserId.ToString();
			}
			if (reponseAuthen!.RoleName == Constants.Constants.ADMIN)
			{
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (reponseAuthen!.RoleName == Constants.Constants.MANAGER)
			{
				return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (reponseAuthen!.RoleName == Constants.Constants.STAFF)
			{
				return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
				return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
		[HttpPost]
		public async Task<IActionResult> SignUp(CreateNewMember newmemRequest)
		{
			var json = JsonSerializer.Serialize(newmemRequest);
			//var content = new MultipartFormDataContent();
			var content = new StringContent(json, Encoding.UTF8, "application/json");
			var options = new JsonSerializerOptions
			{
				PropertyNameCaseInsensitive = true,
			};
			AuthenAPI_URL += "/Register";
			HttpResponseMessage response = await client.PostAsync(AuthenAPI_URL, content);
			if (!response.IsSuccessStatusCode)
			{
				ViewBag.error = "Error while registering your new account ! ";
				return View("Register");
			}
			string jsonResponse = await response.Content.ReadAsStringAsync();
			var authenResponse = JsonSerializer.Deserialize<GetAuthenResponse>(jsonResponse, options);
			var reponseAuthen = authenResponse.Data;
			if (authenResponse.Status)
			{
				HttpContext.Session.SetString("ACCESS_TOKEN", reponseAuthen.AccessToken);
				HttpContext.Session.SetString("ROLE_NAME", reponseAuthen.RoleName);
				HttpContext.Session.SetString("USER_ID", reponseAuthen.UserId.ToString());
				client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", reponseAuthen.AccessToken);
				TempData["ACCESS_TOKEN"] = reponseAuthen.AccessToken;
				TempData["ROLE_NAME"] = reponseAuthen.RoleName;
				TempData["USER_ID"] = reponseAuthen.UserId.ToString();
			}
			if (reponseAuthen!.RoleName == Constants.Constants.ADMIN)
			{
				return base.Redirect(Constants.Constants.ADMIN_URL);
			}
			else if (reponseAuthen!.RoleName == Constants.Constants.MANAGER)
			{
				return base.Redirect(Constants.Constants.MANAGER_URL);
			}
			else if (reponseAuthen!.RoleName == Constants.Constants.STAFF)
			{
				return base.Redirect(Constants.Constants.STAFF_URL);
			}
			else
			{
				return base.Redirect(Constants.Constants.MEMBER_URL);
			}
		}
	}
}
