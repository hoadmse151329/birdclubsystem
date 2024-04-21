using BAL.ViewModels.Member;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using WebAppMVC.Constants;
using WebAppMVC.Models.VnPay;
using WebAppMVC.Services;

namespace WebAppMVC.Controllers
{
    public class VnPayController : Controller
    {
        private readonly IVnPayService _vnPayService;
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
		public VnPayController(IVnPayService vnPayService)
        {
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
            var response = _vnPayService.PaymentExecute(Request.Query);
            if (response.TransactionType == Constants.Constants.NEW_MEMBER_REGISTRATION_TRANSACTION_TYPE && response.Success) return RedirectToAction("Registration", "Auth");
			return View(response);
        }
    }
}
