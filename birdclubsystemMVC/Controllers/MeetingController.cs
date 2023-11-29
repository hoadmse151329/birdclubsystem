using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
{
    public class MeetingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
