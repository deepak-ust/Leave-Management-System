using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface ILeaveBalanceRepository
    {
        void UpdateLeaveBalance(int employeeId, int leaveTypeId, double balance);
        void UpdateEmployeeLeaveBalance(Employee employee, LeaveRequest request, LeaveType leaveType);
        void AddLeaveBalance(EmployeeLeaveBalance leaveBalance);
        EmployeeLeaveBalance GetLeaveBalance(int employeeId, int leaveTypeId);
        IEnumerable<EmployeeLeaveBalance> GetAllLeaveBalances();
        IEnumerable<EmployeeLeaveBalance> GetLeaveBalancesByEmployee(int id);
        void UpdateLeaveCredit(EmployeeLeaveBalance leaveBalance);
        void UpdateCompensatory(int employeeId);
    }
}
