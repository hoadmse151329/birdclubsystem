using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
// thêm crud của meeting, fieldtrip, contest.
namespace WebAppMVC.Controllers
{
    public class ManagerController : Controller
    {
        // GET: ManagerController
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ManagerMeeting()
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
        // GET: ManagerController/Details/5
        public IActionResult Details(int id)
        {
            return View();
        }

        // GET: ManagerController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: ManagerController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Edit/5
        public IActionResult Edit(int id)
        {
            return View();
        }

        // POST: ManagerController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ManagerController/Delete/5
        public IActionResult Delete(int id)
        {
            return View();
        }

        // POST: ManagerController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
