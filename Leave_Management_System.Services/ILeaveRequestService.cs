using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface ILeaveRequestService
    {
        IEnumerable<LeaveRequest> GetLeaveRequests(int id, string status);
        IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int id);
        LeaveRequest GetLeaveRequestById(int id, int currentUser);
        LeaveRequest GetYourRequestById(int id, int currentUser);
        void AddLeaveRequest(LeaveRequest leaveRequest);
        void SetLeaveRequestStatus(int id, bool flag, int currentUser);
        void DeleteLeaveRequest(int id, int currentId);
    }
}
