using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Leave_Management_System.Controllers
{
    [Authorize]
    public class HolidayController : Controller
    {
        private readonly IHolidayService _holidayService;
        public HolidayController(IHolidayService holidayService)
        {
            _holidayService = holidayService;
        }
        public IActionResult Index()
        {
            try
            {
                var holidays = _holidayService.GetHolidays();
                return View(holidays);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        public ActionResult Create(DateTime? date)
        {
            try
            {
                if (date == null)
                {
                    date = DateTime.Today;
                }
                var model = new Holiday { Date = date.Value };
                return View(model);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Holiday holidayObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _holidayService.AddHoliday(holidayObj);
                    return RedirectToAction(nameof(Index));
                }
                var holidays = _holidayService.GetHolidays();
                return View(holidays);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(holidayObj);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            try
            {
                var holiday = _holidayService.GetHolidayById(id);
                if (holiday == null)
                {
                    return RedirectToAction("Index");
                }
                return View(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Holiday holidayObj)
        {
            try
            {
                if (id != holidayObj.Id)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    _holidayService.UpdateHoliday(holidayObj);
                    return RedirectToAction(nameof(Index));
                }
                var holidays = _holidayService.GetHolidays();
                return View(holidays);
            }
            catch 
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(holidayObj);
            }
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                var holiday = _holidayService.GetHolidayById(id);
                if (holiday == null)
                {
                    return NotFound();
                }
                return View(holiday);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                _holidayService.DeleteHoliday(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}