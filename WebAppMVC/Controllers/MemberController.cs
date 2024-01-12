using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using System.Diagnostics;
namespace WebAppMVC.Controllers
{
    public class MemberController : Controller
    {
            private readonly ILogger<MemberController> _logger;

            public MemberController(ILogger<MemberController> logger)
            {
                _logger = logger;
            }

            public IActionResult MemberProfile()
            {
                return View();
            }

            public IActionResult MemberHistoryEvent()
            {
                return View();
            }

            [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
            public IActionResult Error()
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
    // GET: MemberController
    }
}
