using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Leave_Management_System.Controllers
{
    [Authorize(Roles = "Admin,Manager")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public EmployeeController(IEmployeeService employeeService, IUserService userService, ILeaveTypeService leaveTypeService, ILeaveBalanceService leaveBalanceService, IRoleService roleService)
        {
            _employeeService = employeeService;
            _leaveTypeService = leaveTypeService;
            _leaveBalanceService = leaveBalanceService;
            _roleService = roleService;
            _userService = userService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var employees = _employeeService.GetEmployees();
            return View(employees);
        }
        [HttpGet]
        public IActionResult ManagerIndex()
        {
            var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
            var employees = _employeeService.GetEmployeesByManager(currentId);
            return View("Index", employees);
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public ActionResult Create()
        {
            var managers = _employeeService.GetManagers().ToList();
            ViewBag.Managers = managers;
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Employee employeeObj)
        {
            try
            {
                bool checkEmail = _employeeService.CheckEmail(employeeObj.Email);//returns true if the table has employee with same email
                var managers = _employeeService.GetManagers().ToList();
                ViewBag.Managers = managers;
                if (!ModelState.IsValid)
                {
                    return View(employeeObj);
                }
                if (checkEmail)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                    return View(employeeObj);
                }
                var employee = _employeeService.AddEmployee(employeeObj);
                var leavetypes = _leaveTypeService.GetLeaveTypes().ToList();
                _leaveBalanceService.AddLeaveBalanceToEmployee(employee, leavetypes);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing the request.");
                return View(employeeObj);
            }
        }
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            try
            {
                if (employee == null)
                {
                    return NotFound();
                }
                var isManagerExist = _userService.IsManagerExist(employee.ManagerId);
                if (isManagerExist)
                {
                    var manager = _employeeService.GetEmployeeById(employee.ManagerId);
                    ViewBag.Manager = manager.Name;
                }
                else
                {
                    ViewBag.Manager = "--Select Manager--";
                }
                var managers = _employeeService.GetManagers().ToList();
                ViewBag.Email = employee.Email;
                ViewBag.Managers = managers;
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "An error occurred while processing the request.");
            }
            return View(employee);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Employee employeeObj, string email)
        {
            try
            {
                if (id != employeeObj.Id)
                {
                    return BadRequest();
                }
                bool checkEmail = _employeeService.CheckEmail(employeeObj.Email);//returns true if the table has employee with same email
                if (employeeObj.Email == email)
                {
                    checkEmail = false;
                }
                var isManagerExist = _userService.IsManagerExist(employeeObj.ManagerId);
                if (ModelState.IsValid && !checkEmail && isManagerExist)
                {
                    _employeeService.UpdateEmployee(employeeObj);
                    return RedirectToAction(nameof(Index));
                }
                else if (checkEmail)
                {
                    ModelState.AddModelError("Email", "This email address is already in use.");
                }
                else if (!isManagerExist)
                {
                    ViewBag.Manager = "--Select Manager--";
                    ModelState.AddModelError("ManagerId", "Select a Manager.");
                }
            }
            catch {
                ModelState.AddModelError(string.Empty, "An error occurred while processing the request.");
            }
            var managers = _employeeService.GetManagers().ToList();
            ViewBag.Email = employeeObj.Email;
            ViewBag.Managers = managers;
            return View(employeeObj);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirm(int id)
        {
            try
            {
                _employeeService.DeleteEmployee(id);
            }
            catch (Exception ex)    
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Details(int id)
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var role = User.FindFirstValue(ClaimTypes.Role);
                Employee employee = new();
                if (role != "Admin")
                {
                    employee = _employeeService.GetEmployeeByManagerId(id, currentId);
                }
                else
                {
                    employee = _employeeService.GetEmployeeById(id);
                }
                if (employee == null)
                {
                    return NotFound();
                }
                var manager = _employeeService.GetEmployeeById(employee.ManagerId);
                ViewBag.Manager = manager.Name;
                return View(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
