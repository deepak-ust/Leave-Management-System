using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Leave_Management_System.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly IEmployeeService _employeeService;
        private readonly IHolidayService _holidayService;
        private readonly ILeaveRequestService _leaveRequestService;
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IRoleService _roleService;
        private readonly IUserService _userService;
        public AccountController(IAccountService accountService, IUserService userService, ILeaveTypeService leaveTypeService, IEmployeeService employeeService, IHolidayService holidayService, ILeaveRequestService leaveRequestService, ILeaveBalanceService leaveBalanceService, IRoleService roleService)
        {
            _accountService = accountService;
            _leaveTypeService = leaveTypeService;
            _employeeService = employeeService;
            _holidayService = holidayService;
            _leaveRequestService = leaveRequestService;
            _leaveBalanceService = leaveBalanceService;
            _roleService = roleService;
            _userService = userService;
        }
        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            try
            {
                ViewData["ReturnUrl"] = returnUrl;
                return View();
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string email, string password, string returnUrl = null)
        {
            try
            {
                var validUser = _accountService.ValidateUser(email, password);
                if (validUser != null)
                {
                    var user = _employeeService.GetEmployeeById(validUser.EmployeeId);
                    var roles = _roleService.GetUserRoles(validUser.Id);
                    var rolesList = roles.Select(role => _roleService.GetRole(role.RoleId).Name).ToList();
                    var claims = new List<Claim>
                {
                  new Claim(ClaimTypes.Email, validUser.Email),
                  new Claim(ClaimTypes.Name, user.Name),
                  new Claim(ClaimTypes.UserData, validUser.EmployeeId.ToString())
                };
                    claims.AddRange(rolesList.Select(r => new Claim(ClaimTypes.Role, r)));
                    var identity = new ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                        IsPersistent = true,
                        RedirectUri = returnUrl,
                    };
                    await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(identity),
                    authProperties);
                    return RedirectToLocal(returnUrl);
                }
                ModelState.AddModelError(string.Empty, "Please check your email and password.");
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return RedirectToAction(nameof(AccountController.Login), "Account");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        private IActionResult RedirectToLocal(string returnUrl)
        {
            try
            {
                if (Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                else
                {
                    return RedirectToAction(nameof(AccountController.Dashboard), "Account");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        public IActionResult Dashboard()
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var numberOfEmployees = _employeeService.GetEmployees().Count();
                var numberOfLeaveTypes = _leaveTypeService.GetLeaveTypes().Count();
                var numberOfHolidays = _holidayService.GetHolidays().Count();
                var numberOfPendingRequests = _leaveRequestService.GetLeaveRequests(currentId, "Pending").Count();
                var numberOfApprovedRequests = _leaveRequestService.GetLeaveRequests(currentId, "Approved").Count();
                var numberOfRejectedRequests = _leaveRequestService.GetLeaveRequests(currentId, "Rejected").Count();
                var numberOfTeamMembers = 0;
                if (User.IsInRole("Manager"))
                {
                    var teamMembers = _employeeService.GetEmployeesByManager(currentId);
                    if (teamMembers != null)
                    {
                        numberOfTeamMembers = teamMembers.Count();
                    }
                }
                var numberOfEmployeeRequests = _leaveRequestService.GetLeaveRequestsByEmployee(currentId).Count();
                var leaveBalances = _leaveBalanceService.GetLeaveBalancesByEmployee(currentId).ToList();
                List<string> leaveTypesList = new List<string> { };
                List<double?> leaveBalancesList = new List<double?> { };
                double? carryForward = 0.0;
                foreach (var leaveBalance in leaveBalances)
                {
                    leaveBalance.LeaveType = _leaveTypeService.GetLeaveTypeById(leaveBalance.LeaveTypeId);
                    leaveTypesList.Add(leaveBalance.LeaveType.Name);
                    leaveBalancesList.Add(leaveBalance.Balance);
                    if (leaveBalance.LeaveType.Name == "Earned Leave")
                    {
                        carryForward = leaveBalance.CarryForward;
                    }
                }
                List<string> colors = new List<string> { "success", "danger", "info" };
                while (colors.Count < numberOfLeaveTypes)
                {
                    colors.Add(colors[colors.Count - 3]);//for providing card colors
                }
                ViewBag.Colors = colors;
                ViewBag.LeaveTypes = leaveTypesList;
                ViewBag.Balances = leaveBalancesList;
                ViewBag.CarryForward = carryForward;
                ViewBag.LeaveTypesCount = numberOfLeaveTypes;
                ViewBag.EmployeesCount = numberOfEmployees;
                ViewBag.HolidaysCount = numberOfHolidays;
                ViewBag.PendingCount = numberOfPendingRequests;
                ViewBag.ApprovedCount = numberOfApprovedRequests;
                ViewBag.RejectedCount = numberOfRejectedRequests;
                ViewBag.TeamMembers = numberOfTeamMembers;
                ViewBag.EmployeeRequests = numberOfEmployeeRequests;
                return View(leaveBalances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpGet]
        public IActionResult ResetPassword()
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
                var user = _userService.GetUserByEmployeeId(currentId);
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ResetPassword(int id, User userObj)
        {
            try
            {
                UserController userController = new UserController(_userService, _employeeService, _roleService);
                if (id != userObj.Id)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    userObj.Password = userController.Base64Encode(userObj.Password);
                    _userService.UpdateUser(userObj);
                    Logout();
                    return View("Login");
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
