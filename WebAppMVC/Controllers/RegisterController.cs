using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
{
    public class RegisterController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }
    }
}
