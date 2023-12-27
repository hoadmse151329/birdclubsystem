using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class StaffController : Controller
	{
		public IActionResult Staff()
		{
			return View();
		}
	}
}
