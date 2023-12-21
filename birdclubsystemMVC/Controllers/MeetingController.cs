using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
{
    public class MeetingController : Controller
    {
		private readonly ILogger<MeetingController> _logger;

		public MeetingController(ILogger<MeetingController> logger)
		{
			_logger = logger;
		}
		public IActionResult Index()
        {
            return View();
        }
        public IActionResult MeetingPost()
        {
            return View();
        }
        public IActionResult MeetingRegister()
        {
            return View();
        }
    }
}
