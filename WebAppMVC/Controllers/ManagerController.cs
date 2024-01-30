using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    public class ManagerController : Controller
    {
        // GET: ManagerController
        public IActionResult ManagerIndex()
        {
            return View();
        }
        public IActionResult ManagerMeeting()
        {
            return View();
        }
        public IActionResult ManagerNotification()
        {
            return View();
        }
        public IActionResult ManagerMeetingDetail()
        {
            return View();
        }
        public IActionResult ManagerFieldtrip()
        {
            return View();
        }
        public IActionResult ManagerFieldtripDetail()
        {
            return View();
        }
        public IActionResult ManagerBirdContest()
        {
            return View();
        }
        public IActionResult ManagerBirdContestDetail()
        {
            return View();
        }
        public IActionResult ManagerProfile()
        {
            return View();
        }
        public IActionResult ManagerFeedBack()
        {
            return View();
        }
        public IActionResult ManagerStatical()
        {
            return View();
        }
        public IActionResult ManagerHistoryEventsDetail()
        {
            return View();
        }
        public IActionResult ManagerHistoryEvents()
        {
            return View();
        }
    }
}
