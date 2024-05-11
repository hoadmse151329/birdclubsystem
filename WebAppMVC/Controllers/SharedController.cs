using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class SharedController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
