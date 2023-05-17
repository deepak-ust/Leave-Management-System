using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Leave_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class LeaveTypeController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveBalanceService _leaveBalanceService;
        public LeaveTypeController(ILeaveTypeService leaveTypeService, IEmployeeService employeeService, ILeaveBalanceService leaveBalanceService)
        {
            _leaveTypeService = leaveTypeService;
            _employeeService = employeeService;
            _leaveBalanceService = leaveBalanceService;
        }
        public IActionResult Index()
        {
            try
            {
                var leaveTypes = _leaveTypeService.GetLeaveTypes();
                return View(leaveTypes);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        public IActionResult Create()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveType leaveTypeObj)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var leaveType = _leaveTypeService.AddLeaveType(leaveTypeObj);
                    var employees = _employeeService.GetEmployees().ToList();
                    return RedirectToAction(nameof(Index));
                }
                var leave = _leaveTypeService.GetLeaveTypes();
                return View(leave);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(leaveTypeObj);
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                var leaveType = _leaveTypeService.GetLeaveTypeById(id);
                if (leaveType == null)
                {
                    return NotFound();
                }
                return View(leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, LeaveType leaveTypeObj)
        {
            try
            {
                if (id != leaveTypeObj.Id)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    var updatedLeave = _leaveTypeService.UpdateLeaveType(leaveTypeObj);
                    _leaveBalanceService.UpdateLeaveBalance(updatedLeave);
                    return RedirectToAction(nameof(Index));
                }
                var leave = _leaveTypeService.GetLeaveTypes();
                return View(leave);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(leaveTypeObj);
            }
        }
        public IActionResult Delete(int id)
        {
            try
            {
                var leaveType = _leaveTypeService.GetLeaveTypeById(id);
                if (leaveType == null)
                {
                    return NotFound();
                }
                return View(leaveType);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                _leaveTypeService.DeleteLeaveType(id);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public IActionResult Rules(int id)
        {
            try
            {
                var rules = _leaveTypeService.GetRules(id);
                ViewBag.LeaveTypeId = id;
                var leaveType = _leaveTypeService.GetLeaveTypeById(id);
                ViewBag.LeaveType = leaveType.Name;
                return View(rules);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public ActionResult CreateRule(int leaveTypeId)
        {
            try
            {
                ViewBag.LeaveTypeId = leaveTypeId;
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateRule(Rule leaveTypeRule)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (leaveTypeRule.IsApplicable)
                    {
                        var employees = _employeeService.GetEmployeesByBand(leaveTypeRule.Band).ToList();
                        _leaveTypeService.ApplyRule(leaveTypeRule.Band, leaveTypeRule.LeaveTypeId);
                        _leaveBalanceService.AddLeaveBalance(employees, leaveTypeRule);
                    }
                    _leaveTypeService.AddLeaveTypeRule(leaveTypeRule);
                    return RedirectToAction("Rules", new { id = leaveTypeRule.LeaveTypeId });
                }
                var leave = _leaveTypeService.GetLeaveTypes();
                return View(leave);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(leaveTypeRule);
            }
        }
        public IActionResult EditRule(int id)
        {
            try
            {
                var rule = _leaveTypeService.GetRuleById(id);
                if (rule == null)
                {
                    return NotFound();
                }
                ViewBag.LeaveTypeId = rule.LeaveTypeId;
                return View(rule);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditRule(int id, Rule ruleObj)
        {
            try
            {
                if (id != ruleObj.Id)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    if (ruleObj.IsApplicable)
                    {
                        var employees = _employeeService.GetEmployeesByBand(ruleObj.Band).ToList();
                        var leaveType = _leaveTypeService.GetLeaveTypeById(ruleObj.LeaveTypeId);
                        _leaveTypeService.ApplyRule(ruleObj.Band, ruleObj.LeaveTypeId);
                        _leaveBalanceService.AddLeaveBalance(employees, ruleObj);
                    }
                    _leaveTypeService.UpdateRule(ruleObj);
                    return RedirectToAction("Rules", new { id = ruleObj.LeaveTypeId });
                }
                var leave = _leaveTypeService.GetLeaveTypes();
                return View(leave);
            }
            catch
            {
                ModelState.AddModelError("", "An error occurred while processing the request.");
                return View(ruleObj);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRule(int id, int leaveTypeId)
        {
            try
            {
                _leaveTypeService.DeleteRule(id);
                return RedirectToAction("Rules", new { id = leaveTypeId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
