using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class LeaveBalanceService : ILeaveBalanceService
    {
        private readonly ILeaveBalanceRepository _leaveBalanceRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public LeaveBalanceService(ILeaveBalanceRepository leaveBalanceRepository, IEmployeeRepository employeeRepository, ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveBalanceRepository = leaveBalanceRepository;
            _employeeRepository = employeeRepository;
            _leaveTypeRepository = leaveTypeRepository;
        }
        public IEnumerable<EmployeeLeaveBalance> GetLeaveBalances()
        {
            var leaveBalances = _leaveBalanceRepository.GetAllLeaveBalances();
            return leaveBalances.Select(l => new EmployeeLeaveBalance
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                LeaveTypeId = l.LeaveTypeId,
                Balance = l.Balance,
                CarryForward = l.CarryForward
            });
        }
        public IEnumerable<EmployeeLeaveBalance> GetLeaveBalancesByEmployee(int id)
        {
            var leaveBalances = _leaveBalanceRepository.GetLeaveBalancesByEmployee(id);
            return leaveBalances.Select(l => new EmployeeLeaveBalance
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                LeaveTypeId = l.LeaveTypeId,
                Balance = l.Balance,
                CarryForward = l.CarryForward
            });
        }
        public void UpdateEmployeeLeaveBalance(LeaveRequest request)
        {
            var employee = _employeeRepository.GetEmployeeById(request.EmployeeId);
            var leaveType = _leaveTypeRepository.GetLeaveTypeByName(request.LeaveType);
            _leaveBalanceRepository.UpdateEmployeeLeaveBalance(employee, request, leaveType);
        }
        public void UpdateLeaveCredit(EmployeeLeaveBalance leaveBalance)
        {
            _leaveBalanceRepository.UpdateLeaveCredit(leaveBalance);
        }
        public void UpdateLeaveBalance(LeaveType leaveType)
        {
            var employees = _employeeRepository.GetAllEmployees().ToList();
            foreach (var employee in employees)
            {
                var balance = 0.0;
                var rule = _leaveTypeRepository.GetAppliedRule(employee.Band, leaveType.Id);
                if (rule != null)
                {
                    balance = rule.DefaultBalance;
                }
                _leaveBalanceRepository.UpdateLeaveBalance(employee.Id, leaveType.Id, balance);
            }
        }
        public void AddLeaveBalance(List<Employee> employees, Rule ruleObj)
        {
            foreach (var employee in employees)
            {
                var balance = 0.0;
                balance = ruleObj.DefaultBalance;
                var employeeLeaveBalance = new EmployeeLeaveBalance
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = ruleObj.LeaveTypeId,
                    Balance = balance
                };
                _leaveBalanceRepository.AddLeaveBalance(employeeLeaveBalance);
            }
        }
        public void AddLeaveBalanceToEmployee(Employee employee, List<LeaveType> leaveTypes)
        {
            foreach (var leaveType in leaveTypes)
            {
                var balance = 0.0;
                var rule = _leaveTypeRepository.GetAppliedRule(employee.Band, leaveType.Id);
                balance = rule.DefaultBalance;
                var employeeLeaveBalance = new EmployeeLeaveBalance
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    Balance = balance
                };
                _leaveBalanceRepository.AddLeaveBalance(employeeLeaveBalance);
            }
        }
        public EmployeeLeaveBalance GetEmployeeLeaveBalance(LeaveRequest request)
        {
            var employeeId = request.EmployeeId;
            var leaveType = _leaveTypeRepository.GetLeaveTypeByName(request.LeaveType);
            var leaveTypeId = leaveType.Id;
            var leaveBalance = _leaveBalanceRepository.GetLeaveBalance(employeeId, leaveTypeId);
            return leaveBalance;
        }
        public void UpdateCompensatory(LeaveRequest request)
        {
            _leaveBalanceRepository.UpdateCompensatory(request.EmployeeId);
        }
    }
}
