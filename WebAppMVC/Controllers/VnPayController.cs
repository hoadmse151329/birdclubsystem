using BAL.ViewModels;
using BAL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.Meeting;
using WebAppMVC.Models.Transaction;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Services.Interfaces;
using static Org.BouncyCastle.Math.EC.ECCurve;

namespace WebAppMVC.Controllers
{
    public class VnPayController : Controller
    {
		private readonly ILogger<VnPayController> _logger;
		private readonly IConfiguration _config;
		private readonly HttpClient _httpClient = null;
		private readonly IVnPayService _vnPayService;
		private string TransactionAPI_URL = "";
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
		public VnPayController(ILogger<VnPayController> logger, IConfiguration config, IVnPayService vnPayService)
        {
			_logger = logger;
			_config = config;
			_httpClient = new HttpClient();
			_vnPayService = vnPayService;
			var contentType = new MediaTypeWithQualityHeaderValue("application/json");
			_httpClient.DefaultRequestHeaders.Accept.Add(contentType);
			_httpClient.BaseAddress = new Uri(config.GetSection("DefaultApiUrl:ConnectionString").Value);
			TransactionAPI_URL = "/api/Transaction/";
			_vnPayService = vnPayService;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult CreatePaymentUrl(PaymentInformationModel model)
        {
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }

        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);
            return Json(response);
        }

        public async Task<IActionResult> PaymentConfirm()
        {
			string? accToken = HttpContext.Session.GetString(Constants.Constants.ACC_TOKEN);

			string? role = HttpContext.Session.GetString(Constants.Constants.ROLE_NAME);

			var response = _vnPayService.PaymentExecute(Request.Query);

			switch (response.TransactionType)
			{
				case var value when value.Equals(Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE):
					{
						if(response.Success && role.Equals(Constants.Constants.TEMPMEMBER))
						{
                            TransactionAPI_URL += "Create";

                            var tran = new TransactionViewModel()
                            {
                                Value = (int)(response.Value / 100),
                                UserId = null,
                                VnPayId = response.TransactionId.ToString(),
                                TransactionType = response.TransactionType,
                                TransactionDate = DateTime.Now,
                                PaymentDate = DateTime.Now,
                                DocNo = response.DocNo,
                                Status = "Completed"
                            };

                            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                                            _httpClient: _httpClient,
                                            options: jsonOptions,
                                            methodName: Constants.Constants.POST_METHOD,
                                            url: TransactionAPI_URL,
                                            inputType: tran,
                                            accessToken: accToken,
                                            _logger: _logger);

                            if (transactionResponse == null)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Register", "Auth");
                            }
                            else
                            if (!transactionResponse.Status)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Register", "Auth");
                            }

                            methcall.SetCookie(Response, Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_COOKIE, transactionResponse.Data, cookieOptions, jsonOptions, 5);

                            return RedirectToAction("ConfirmRegister", "Auth");
                        }
                        ViewBag.Error = "Error while processing your request! (Getting Transaction Response!)";
                        return RedirectToAction("Register", "Auth");
					}
                case var value when value.Equals(Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_TYPE):
                    {
                        if (response.Success && role.Equals(Constants.Constants.MEMBER))
                        {
                            TransactionAPI_URL += "Create";

                            var tran = new TransactionViewModel()
                            {
                                Value = (int)(response.Value / 100),
                                UserId = null,
                                VnPayId = response.TransactionId.ToString(),
                                TransactionType = response.TransactionType,
                                TransactionDate = DateTime.Now,
                                PaymentDate = DateTime.Now,
                                DocNo = response.DocNo,
                                Status = "Completed"
                            };

                            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                                            _httpClient: _httpClient,
                                            options: jsonOptions,
                                            methodName: Constants.Constants.POST_METHOD,
                                            url: TransactionAPI_URL,
                                            inputType: tran,
                                            accessToken: accToken,
                                            _logger: _logger);

                            if (transactionResponse == null)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Index", "FieldTrip");
                            }
                            else
                            if (!transactionResponse.Status)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Index", "FieldTrip");
                            }
                            methcall.SetCookie(Response, Constants.Constants.MEMBER_FIELDTRIP_REGISTRATION_TRANSACTION_COOKIE, transactionResponse.Data, cookieOptions, jsonOptions, 5);
                            return RedirectToAction("FieldTripConfirmRegister", "FieldTrip");
                        }
                        ViewBag.Error = "Error while processing your request! (Getting Transaction Response!)";
                        return RedirectToAction("Index", "FieldTrip");
                    }
                case var value when value.Equals(Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_TYPE):
                    {
                        if (response.Success && role.Equals(Constants.Constants.MEMBER))
                        {
                            TransactionAPI_URL += "Create";

                            var tran = new TransactionViewModel()
                            {
                                Value = (int)(response.Value / 100),
                                UserId = null,
                                VnPayId = response.TransactionId.ToString(),
                                TransactionType = response.TransactionType,
                                TransactionDate = DateTime.Now,
                                PaymentDate = DateTime.Now,
                                DocNo = response.DocNo,
                                Status = "Completed"
                            };

                            var transactionResponse = await methcall.CallMethodReturnObject<GetTransactionResponse>(
                                            _httpClient: _httpClient,
                                            options: jsonOptions,
                                            methodName: Constants.Constants.POST_METHOD,
                                            url: TransactionAPI_URL,
                                            inputType: tran,
                                            accessToken: accToken,
                                            _logger: _logger);

                            if (transactionResponse == null)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Index", "Contest");
                            }
                            else
                            if (!transactionResponse.Status)
                            {
                                ViewBag.Error =
                                    "Error while processing your request! (Getting Transaction Response!)";
                                return RedirectToAction("Index", "Contest");
                            }
                            methcall.SetCookie(Response, Constants.Constants.MEMBER_CONTEST_REGISTRATION_TRANSACTION_COOKIE, transactionResponse.Data, cookieOptions, jsonOptions, 5);
                            return RedirectToAction("ContestConfirmRegister", "Contest");
                        }
                        ViewBag.Error = "Error while processing your request! (Getting Transaction Response!)";
                        return RedirectToAction("Index", "Contest");
                    }
            }
			return View(response);
        }
    }
}
