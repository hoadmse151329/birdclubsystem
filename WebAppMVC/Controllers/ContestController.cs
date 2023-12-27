using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
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
