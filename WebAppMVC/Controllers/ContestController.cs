using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
	public class ContestController : Controller
	{
		public IActionResult Index()
		{
			return View();
		}
		public IActionResult LeaderBoard()
		{
			return View();
		}
	}
}
