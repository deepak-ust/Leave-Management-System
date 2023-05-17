using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Leave_Management_System.Controllers
{
    [Authorize]
    public class LeaveRequestController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IHolidayService _holidayService;
        private readonly IWebHostEnvironment _hostEnvironment;
        public LeaveRequestController(IEmployeeService employeeService, ILeaveRequestService leaveRequestService, ILeaveTypeService leaveTypeService, ILeaveBalanceService leaveBalanceService, IHolidayService holidayService, IWebHostEnvironment hostEnvironment)
        {
            _employeeService = employeeService;
            _leaveRequestService = leaveRequestService;
            _leaveTypeService = leaveTypeService;
            _leaveBalanceService = leaveBalanceService;
            _holidayService = holidayService;
            _hostEnvironment = hostEnvironment;
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Index()
        {
            ViewBag.Heading = "Pending Requests";
            ViewBag.Status = "Pending";
            var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
            var leaveRequests = _leaveRequestService.GetLeaveRequests(currentId, ViewBag.Status);
            return View(leaveRequests);
        }
        public IActionResult Create()
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var currentObj = _employeeService.GetEmployeeById(currentId);
                var sickLeaveObj = _leaveTypeService.GetLeaveTypeByName("Sick Leave");
                var leaveTypes = _leaveTypeService.GetLeaveTypes();
                var sickLeaveRule = _leaveTypeService.GetAppliedRule(currentObj.Band, sickLeaveObj.Id);
                DateTime[] days = Array.Empty<DateTime>();
                var holidays = _holidayService.GetHolidays();
                foreach (var holiday in holidays)
                {
                    days.Append(holiday.Date);
                }
                ViewBag.Holidays = days;
                ViewBag.LeaveTypes = leaveTypes;
                if (sickLeaveRule.IsApplicable)
                {
                    ViewBag.AllowedLeaves = sickLeaveRule.AllowedLeaves;
                    ViewBag.IsApplicable = "true";
                }
                else
                {
                    ViewBag.IsApplicable = "false";
                }
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(LeaveRequest leaveRequestObj, IFormFile? file, int allowedLeaves)
        {
            try
            {
                var holidays = _holidayService.GetHolidays();
                List<DateTime> days = new List<DateTime>();
                foreach (var holiday in holidays)
                {
                    days.Add(holiday.Date);
                }
                var currentId = User.FindFirstValue(ClaimTypes.UserData);
                leaveRequestObj.EmployeeId = int.Parse(currentId);
                leaveRequestObj.Status = "Pending";
                leaveRequestObj.AppliedOn = DateTime.Now;
                leaveRequestObj.FileUrl = "";
                var leaveBalance = _leaveBalanceService.GetEmployeeLeaveBalance(leaveRequestObj);
                if (leaveBalance.Balance > 0)
                {
                    if (leaveRequestObj.LeaveType == "Compensatory Leave" && !days.Contains(leaveRequestObj.StartDate) && leaveRequestObj.StartDate.DayOfWeek != DayOfWeek.Sunday && leaveRequestObj.StartDate.DayOfWeek != DayOfWeek.Saturday)
                    {
                        if (ModelState.IsValid)
                        {
                            _leaveBalanceService.UpdateCompensatory(leaveRequestObj);
                            return RedirectToAction("YourRequests");
                        }
                    }
                    else
                    {
                        var startDate = leaveRequestObj.StartDate;
                        var endDate = leaveRequestObj.EndDate;
                        var differenceDays = 0;
                        var inactiveDays = 0;
                        while (startDate <= endDate)
                        {
                            if (startDate.DayOfWeek != DayOfWeek.Sunday && startDate.DayOfWeek != DayOfWeek.Saturday && !days.Contains(startDate.Date))
                            {
                                differenceDays++;
                            }
                            else
                            {
                                inactiveDays++;
                            }
                            startDate = startDate.AddDays(1);
                        }
                        if (file != null && file.Length > 0)
                        {
                            // Save the file to a specified path
                            var uniqueFileName = currentId + "_" + DateTime.Now.ToString().Replace(":", "").Replace(" ", "_") + "_" + file.FileName;
                            var filePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", uniqueFileName);
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyToAsync(stream);
                            }
                            leaveRequestObj.FileUrl = uniqueFileName;
                        }
                        else if (leaveRequestObj.LeaveType == "Sick Leave" && file == null && allowedLeaves < differenceDays)
                        {
                            ModelState.AddModelError(string.Empty, "Upload Certificate");
                            var currentObj = _employeeService.GetEmployeeById(int.Parse(currentId));
                            var sickLeaveObj = _leaveTypeService.GetLeaveTypeByName("Sick Leave");
                            var leaveTypes = _leaveTypeService.GetLeaveTypes();
                            var sickLeaveRule = _leaveTypeService.GetAppliedRule(currentObj.Band, sickLeaveObj.Id);
                            ViewBag.LeaveTypes = leaveTypes;
                            if (sickLeaveRule.IsApplicable)
                            {
                                ViewBag.AllowedLeaves = sickLeaveRule.AllowedLeaves;
                                ViewBag.IsApplicable = "true";
                            }
                            else
                            {
                                ViewBag.IsApplicable = "false";
                            }
                        }
                        else
                        {
                            leaveRequestObj.FileUrl = "";
                        }
                        if (ModelState.IsValid)
                        {
                            _leaveRequestService.AddLeaveRequest(leaveRequestObj);
                            return RedirectToAction("YourRequests");
                        }
                        return View();
                    }
                }
                return RedirectToAction("Create");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult ApplyCompensatory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ApplyCompensatory(LeaveRequest leaveRequestObj)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                leaveRequestObj.EmployeeId = currentId;
                var holidays = _holidayService.GetHolidays();
                List<DateTime> days = new List<DateTime>();
                foreach (var holiday in holidays)
                {
                    days.Add(holiday.Date);
                }
                if (days.Contains(leaveRequestObj.StartDate) || leaveRequestObj.StartDate.DayOfWeek == DayOfWeek.Sunday || leaveRequestObj.StartDate.DayOfWeek == DayOfWeek.Saturday)
                {
                    leaveRequestObj.Status = "Pending";
                    leaveRequestObj.AppliedOn = DateTime.Now;
                    leaveRequestObj.EndDate = leaveRequestObj.StartDate;
                    leaveRequestObj.Reason = "";
                    leaveRequestObj.EmployeeId = currentId;
                    if (ModelState.IsValid)
                    {
                        _leaveRequestService.AddLeaveRequest(leaveRequestObj);
                        return RedirectToAction("YourRequests");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Select an applicable date");
                }
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var leaveRequest = _leaveRequestService.GetLeaveRequestById(id, currentId);
                if (leaveRequest == null)
                {
                    return NotFound();
                }
                ViewBag.Status = leaveRequest.Status;
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult YourRequestDetails(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var leaveRequest = _leaveRequestService.GetYourRequestById(id, currentId);
                if (leaveRequest == null)
                {
                    return NotFound();
                }
                return View(leaveRequest);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult ApprovedRequests()
        {
            try
            {
                ViewBag.Heading = "Approved Requests";
                ViewBag.Status = "Approved";
                var id = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var leaveRequests = _leaveRequestService.GetLeaveRequests(id, ViewBag.Status);
                return View("Index", leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult RejectedRequests()
        {
            try
            {
                ViewBag.Heading = "Rejected Requests";
                ViewBag.Status = "Rejected";
                var id = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var leaveRequests = _leaveRequestService.GetLeaveRequests(id, ViewBag.Status);
                return View("Index", leaveRequests);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Approve(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var leaveRequestObj = _leaveRequestService.GetLeaveRequestById(id, currentId);
                _leaveBalanceService.UpdateEmployeeLeaveBalance(leaveRequestObj);
                _leaveRequestService.SetLeaveRequestStatus(id, true, currentId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Roles = "Admin,Manager")]
        public IActionResult Reject(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                _leaveRequestService.SetLeaveRequestStatus(id, false, currentId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpGet]
        public IActionResult YourRequests()
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var employee = _leaveRequestService.GetLeaveRequestsByEmployee(currentId);
                if (employee == null)
                {
                    return NotFound();
                }
                return View(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                _leaveRequestService.DeleteLeaveRequest(id, currentId);
                return RedirectToAction("YourRequests");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        public async Task<IActionResult> Download(string fileName)
        {
            try
            {
                var filePath = Path.Combine(_hostEnvironment.WebRootPath, "uploads", fileName);
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    await stream.CopyToAsync(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(filePath), Path.GetFileName(filePath));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }
        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
    {
        { ".txt", "text/plain" },
        { ".pdf", "application/pdf" },
        { ".doc", "application/vnd.ms-word" },
        { ".docx", "application/vnd.ms-word" },
        { ".xls", "application/vnd.ms-excel" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".png", "image/png" },
        { ".jpg", "image/jpeg" },
        { ".jpeg", "image/jpeg" },
        { ".gif", "image/gif" },
        { ".csv", "text/csv" }
    };
        }
    }
}
