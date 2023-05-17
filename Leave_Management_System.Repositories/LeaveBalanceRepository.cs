using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class LeaveBalanceRepository : ILeaveBalanceRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveBalanceRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public void UpdateLeaveBalance(int employeeId, int leaveTypeId, double balance)
        {
            var oldBalance = _context.EmployeeLeaveBalances.FirstOrDefault(b => b.EmployeeId == employeeId && b.LeaveTypeId == leaveTypeId);
            if (oldBalance != null)
            {
                oldBalance.Balance = balance;
                _context.SaveChanges();
            }
        }
        public void UpdateCompensatory(int employeeId)
        {
            var leaveType = _context.LeaveTypes.FirstOrDefault(l => l.Name == "Compensatory Leave");
            if (leaveType != null)
            {
                var balance = _context.EmployeeLeaveBalances.FirstOrDefault(b => b.EmployeeId == employeeId && b.LeaveTypeId == leaveType.Id);
                if (balance != null && balance.Balance > 0)
                {
                    balance.Balance--;
                    _context.SaveChanges();
                }
            }
        }
        public void UpdateEmployeeLeaveBalance(Employee employee, LeaveRequest request, LeaveType leaveType)
        {
            var employeeId = employee.Id;
            var leaveTypeId = leaveType.Id;
            var balance = _context.EmployeeLeaveBalances.FirstOrDefault(b => b.EmployeeId == employeeId && b.LeaveTypeId == leaveTypeId);
            if (leaveType.Name == "Compensatory Leave")
            {
                if (balance != null)
                {
                    balance.Balance++;
                }
            }
            else
            {
                var holidays = _context.Holidays.Select(h => h.Date).ToList();
                var diffDays = 0;
                var weekends = 0;
                var start = request.StartDate;
                var end = request.EndDate;
                while (start <= end)
                {
                    if (start.DayOfWeek != DayOfWeek.Sunday && start.DayOfWeek != DayOfWeek.Saturday && !holidays.Contains(start))
                    {
                        diffDays++;
                    }
                    else
                    {
                        weekends++;
                    }
                    start = start.Date.AddDays(1);
                }
                if (balance != null)
                {
                    balance.Balance -= diffDays;
                }
                else
                {
                    balance = new EmployeeLeaveBalance
                    {
                        EmployeeId = employee.Id,
                        LeaveTypeId = leaveType.Id,
                        Balance = -diffDays
                    };
                    _context.EmployeeLeaveBalances.Add(balance);
                }
            }
            _context.SaveChanges();
        }
        public void AddLeaveBalance(EmployeeLeaveBalance leaveBalance)
        {
            var isExist = _context.EmployeeLeaveBalances.Any(b => b.EmployeeId == leaveBalance.EmployeeId && b.LeaveType.Id == leaveBalance.LeaveTypeId);
            if (!isExist)
            {
                _context.EmployeeLeaveBalances.Add(leaveBalance);
            }
            _context.SaveChanges();
        }
        public EmployeeLeaveBalance GetLeaveBalance(int employeeId, int leaveTypeId)
        {
            return _context.EmployeeLeaveBalances.FirstOrDefault(b => b.EmployeeId == employeeId && b.LeaveTypeId == leaveTypeId);
        }
        public IEnumerable<EmployeeLeaveBalance> GetAllLeaveBalances()
        {
            return _context.EmployeeLeaveBalances.ToList();
        }
        public IEnumerable<EmployeeLeaveBalance> GetLeaveBalancesByEmployee(int id)
        {
            return _context.EmployeeLeaveBalances.Where(b => b.EmployeeId == id);
        }
        public void UpdateLeaveCredit(EmployeeLeaveBalance leaveBalance)
        {
            var oldBalance = _context.EmployeeLeaveBalances.FirstOrDefault(b => b.EmployeeId == leaveBalance.EmployeeId && b.LeaveTypeId == leaveBalance.LeaveTypeId);
            if (oldBalance != null)
            {
                oldBalance.Balance = leaveBalance.Balance;
                oldBalance.CarryForward = leaveBalance.CarryForward;
                _context.SaveChanges();
            }
        }
    }
}
