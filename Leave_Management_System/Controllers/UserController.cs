using Leave_Management_System.Data.Models;
using Leave_Management_System.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text;
namespace Leave_Management_System.Controllers
{
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IEmployeeService _employeeService;
        private readonly IRoleService _roleService;
        public UserController(IUserService userService, IEmployeeService employeeService, IRoleService roleService)
        {
            _userService = userService;
            _employeeService = employeeService;
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            try
            {
                var users = _userService.GetUsers();
                return View(users);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(User userObj)
        {
            try
            {
                var employee = _employeeService.GetEmployeeById(userObj.EmployeeId);
                if (employee != null)
                {
                    userObj.Email = employee.Email;
                }
                else
                {
                    ModelState.AddModelError("EmployeeId", "Employee Id not found.");
                }
                if (ModelState.IsValid)
                {
                    var encryptedPassword = Base64Encode(userObj.Password);
                    userObj.Password = encryptedPassword;
                    _userService.AddUser(userObj);
                    return Redirect("AddRole?userId=" + userObj.Id);
                }
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        public IActionResult AddRole(int userId)
        {
            var userRole = new UserRole
            {
                UserId = userId
            };
            return View(userRole);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AssignRole(UserRole userRoleObj)
        {
            try
            {
                bool isRoleExist = _roleService.IsRoleExist(userRoleObj);
                if (!isRoleExist)
                {
                    if (ModelState.IsValid)
                    {
                        _roleService.AddUserRole(userRoleObj);
                    }
                }
                return RedirectToAction("AddRole", new { userId = userRoleObj.UserId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteRole(int roleId, int userId)
        {
            try
            {
                _roleService.DeleteRole(roleId, userId);
                return Redirect("ViewRoles?userId=" + userId);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        public IActionResult Edit(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                var decryptedPassword = Base64Decode(user.Password);
                user.Password = decryptedPassword;
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        public IActionResult ViewRoles(int userId)
        {
            try
            {
                var roles = _roleService.GetUserRoles(userId);
                ViewBag.UserId = userId;
                if (roles == null)
                {
                    return NotFound();
                }
                return View(roles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, User userObj)
        {
            try
            {
                if (id != userObj.Id)
                {
                    return BadRequest();
                }
                if (ModelState.IsValid)
                {
                    userObj.Password = Base64Encode(userObj.Password);
                    _userService.UpdateUser(userObj);
                    return RedirectToAction(nameof(Index));
                }
                var users = _userService.GetUsers();
                return View(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int userId)
        {
            try
            {
                var roles = _roleService.GetUserRoles(userId);
                foreach (var role in roles)
                {
                    _roleService.DeleteRole(role.RoleId, userId);
                }
                _userService.DeleteUser(userId);
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
