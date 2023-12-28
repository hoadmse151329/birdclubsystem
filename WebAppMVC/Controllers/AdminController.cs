using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class AdminController : Controller
	{
		public IActionResult Admin()
		{
			return View();
		}
	}
}
