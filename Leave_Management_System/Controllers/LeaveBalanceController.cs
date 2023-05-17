using Leave_Management_System.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
namespace Leave_Management_System.Controllers
{
    public class LeaveBalanceController : Controller
    {
        private readonly ILeaveTypeService _leaveTypeService;
        private readonly ILeaveBalanceService _leaveBalanceService;
        private readonly IEmployeeService _employeeService;
        public LeaveBalanceController(ILeaveTypeService leaveTypeService, ILeaveBalanceService leaveBalanceService, IEmployeeService employeeService)
        {
            _leaveTypeService = leaveTypeService;
            _leaveBalanceService = leaveBalanceService;
            _employeeService = employeeService;
        }
        public void CreditLeaveBalance(DateTime currentDate)
        {
            try
            {
                var leaveBalances = _leaveBalanceService.GetLeaveBalances().ToList();
                foreach (var leaveBalance in leaveBalances)
                {
                    var leaveType = _leaveTypeService.GetLeaveTypeById(leaveBalance.LeaveTypeId);
                    var employee = _employeeService.GetEmployeeById(leaveBalance.EmployeeId);
                    var rule = _leaveTypeService.GetAppliedRule(employee.Band, leaveBalance.LeaveTypeId);
                    if (currentDate.Month == 1 && currentDate.Day == 1 && leaveType.Name == "Earned Leave")
                    {
                        leaveBalance.CarryForward = leaveBalance.Balance;
                    }
                    if ((rule.LeaveCreditFrequency == 1 && currentDate.Day == 1) || (rule.LeaveCreditFrequency == 6 && currentDate.Month % 6 == 1 && currentDate.Day == 1) || (rule.LeaveCreditFrequency == 12 && currentDate.Month == 1 && currentDate.Day == 1))
                    {
                        if (leaveType.Name == "Earned Leave")
                        {
                            if (rule.LeaveCreditFrequency == 1)
                            {
                                leaveBalance.Balance = rule.Credit * (13 - currentDate.Month);
                            }
                            else if (rule.LeaveCreditFrequency == 6)
                            {
                                leaveBalance.Balance = rule.Credit * 2;
                            }
                            else if (rule.LeaveCreditFrequency == 12)
                            {
                                leaveBalance.Balance = rule.Credit;
                            }
                        }
                        else if (leaveType.Name != "Earned Leave")
                        {
                            if (rule.LeaveCreditFrequency == 6 || rule.LeaveCreditFrequency == 12)
                            {
                                leaveBalance.Balance = rule.DefaultBalance;
                            }
                            else
                            {
                                leaveBalance.Balance += rule.DefaultBalance;
                            }
                        }
                    }
                    if (currentDate.Month == 1 && currentDate.Day == 1 && leaveType.Name == "Earned Leave")
                    {
                        leaveBalance.Balance += leaveBalance.CarryForward;
                    }
                    _leaveBalanceService.UpdateLeaveCredit(leaveBalance);
                }
            }
            catch
            {
                throw;
            }
        }
        public IActionResult Index()
        {
            try
            {
                var currentId = int.Parse(User.FindFirstValue(ClaimTypes.UserData));
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
                ViewBag.LeaveTypes = leaveTypesList;
                ViewBag.Balances = leaveBalancesList;
                ViewBag.CarryForward = carryForward;
                return View(leaveBalances);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
