using Microsoft.AspNetCore.Mvc;

namespace birdclubsystem.Controllers
{
    public class StaffController : Controller
    {
        public IActionResult Staff()
        {
            return View();
        }
    }
}
