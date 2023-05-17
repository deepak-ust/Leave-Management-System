using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface ILeaveBalanceService
    {
        void UpdateEmployeeLeaveBalance(LeaveRequest request);
        void UpdateLeaveBalance(LeaveType leaveType);
        void AddLeaveBalance(List<Employee> employees, Rule ruleObj);
        void AddLeaveBalanceToEmployee(Employee employee, List<LeaveType> leaveTypes);
        IEnumerable<EmployeeLeaveBalance> GetLeaveBalances();
        IEnumerable<EmployeeLeaveBalance> GetLeaveBalancesByEmployee(int id);
        EmployeeLeaveBalance GetEmployeeLeaveBalance(LeaveRequest request);
        void UpdateLeaveCredit(EmployeeLeaveBalance leaveBalance);
        void UpdateCompensatory(LeaveRequest leaveRequest);
    }
}
