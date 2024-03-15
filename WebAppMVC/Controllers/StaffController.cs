using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class StaffController : Controller
	{
        [HttpGet("Index")]
        public IActionResult Staff()
		{
			return View();
		}

	}
}
