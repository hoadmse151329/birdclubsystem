using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Admin()
        {
            return View();
        }
    }
}
