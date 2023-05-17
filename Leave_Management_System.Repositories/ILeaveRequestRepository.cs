using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface ILeaveRequestRepository
    {
        IEnumerable<LeaveRequest> GetLeaveRequests(int id, string status);
        IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int id);
        LeaveRequest GetLeaveRequestById(int id, int currentUser);
        LeaveRequest GetYourRequestById(int id, int currentUser);
        void CreateLeaveRequest(LeaveRequest leaveRequest);
        void UpdateLeaveRequest(LeaveRequest leaveRequest);
        void DeleteLeaveRequest(LeaveRequest leaveRequest);
        void SaveChanges();
    }
}
