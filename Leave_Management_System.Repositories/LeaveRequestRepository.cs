using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveRequestRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public void CreateLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Add(leaveRequest);
        }
        public IEnumerable<LeaveRequest> GetLeaveRequests(int id, string status)
        {
            return _context.LeaveRequests.ToList().Where(lr => _context.Employees.Any(e => e.Id == lr.EmployeeId && e.ManagerId == id && lr.Status == status));
        }
        public IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int id)
        {
            return _context.LeaveRequests.ToList().Where(lr => lr.EmployeeId == id);
        }
        public LeaveRequest GetLeaveRequestById(int id, int currentUser)
        {
            return _context.LeaveRequests.FirstOrDefault(l => _context.Employees.Any(e => l.Id == id && l.EmployeeId == e.Id && e.ManagerId == currentUser));
        }
        public LeaveRequest GetYourRequestById(int id, int currentUser)
        {
            return _context.LeaveRequests.FirstOrDefault(l => l.Id == id && l.EmployeeId == currentUser);
        }
        public void UpdateLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Update(leaveRequest);
        }
        public void DeleteLeaveRequest(LeaveRequest leaveRequest)
        {
            _context.LeaveRequests.Remove(leaveRequest);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
