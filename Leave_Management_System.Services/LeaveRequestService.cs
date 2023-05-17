using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class LeaveRequestService : ILeaveRequestService
    {
        private readonly ILeaveRequestRepository _leaveRequestRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public LeaveRequestService(ILeaveRequestRepository leaveRequestRepository, IEmployeeRepository employeeRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _employeeRepository = employeeRepository;
        }
        public void AddLeaveRequest(LeaveRequest leaveRequestObj)
        {
            var leaveRequest = new LeaveRequest
            {
                Id = leaveRequestObj.Id,
                EmployeeId = leaveRequestObj.EmployeeId,
                LeaveType = leaveRequestObj.LeaveType,
                StartDate = leaveRequestObj.StartDate,
                EndDate = leaveRequestObj.EndDate,
                Reason = leaveRequestObj.Reason,
                Status = leaveRequestObj.Status,
                AppliedOn = leaveRequestObj.AppliedOn,
                FileUrl = leaveRequestObj.FileUrl
            };
            _leaveRequestRepository.CreateLeaveRequest(leaveRequest);
            _leaveRequestRepository.SaveChanges();
        }
        public LeaveRequest GetLeaveRequestById(int id, int currentUser)
        {
            var leaveRequest = _leaveRequestRepository.GetLeaveRequestById(id, currentUser);
            if (leaveRequest == null)
            {
                return null;
            }
            return new LeaveRequest
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveType = leaveRequest.LeaveType,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason,
                Status = leaveRequest.Status,
                AppliedOn = leaveRequest.AppliedOn,
                FileUrl = leaveRequest.FileUrl,
                Employee = _employeeRepository.GetEmployeeById(leaveRequest.EmployeeId)
            };
        }
        public LeaveRequest GetYourRequestById(int id, int currentUser)
        {
            var leaveRequest = _leaveRequestRepository.GetYourRequestById(id, currentUser);
            if (leaveRequest == null)
            {
                return null;
            }
            return new LeaveRequest
            {
                Id = leaveRequest.Id,
                EmployeeId = leaveRequest.EmployeeId,
                LeaveType = leaveRequest.LeaveType,
                StartDate = leaveRequest.StartDate,
                EndDate = leaveRequest.EndDate,
                Reason = leaveRequest.Reason,
                Status = leaveRequest.Status,
                AppliedOn = leaveRequest.AppliedOn,
                FileUrl = leaveRequest.FileUrl,
                Employee = _employeeRepository.GetEmployeeById(leaveRequest.EmployeeId)
            };
        }
        public IEnumerable<LeaveRequest> GetLeaveRequests(int id, string status)
        {
            var leaveRequests = _leaveRequestRepository.GetLeaveRequests(id, status);
            return leaveRequests.Select(l => new LeaveRequest
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                Employee = _employeeRepository.GetEmployeeById(l.EmployeeId)
            });
        }
        public IEnumerable<LeaveRequest> GetLeaveRequestsByEmployee(int id)
        {
            var leaveRequests = _leaveRequestRepository.GetLeaveRequestsByEmployee(id);
            return leaveRequests.Select(l => new LeaveRequest
            {
                Id = l.Id,
                EmployeeId = l.EmployeeId,
                LeaveType = l.LeaveType,
                StartDate = l.StartDate,
                EndDate = l.EndDate,
                Reason = l.Reason,
                Status = l.Status,
                AppliedOn = l.AppliedOn,
                FileUrl = l.FileUrl,
                Employee = _employeeRepository.GetEmployeeById(l.EmployeeId)
            });
        }
        public void SetLeaveRequestStatus(int id, bool flag, int currentUser)
        {
            var request = _leaveRequestRepository.GetLeaveRequestById(id, currentUser);
            if (request != null)
            {
                if (flag)
                {
                    request.Status = "Approved";
                }
                else
                {
                    request.Status = "Rejected";
                }
                _leaveRequestRepository.UpdateLeaveRequest(request);
                _leaveRequestRepository.SaveChanges();
            }
        }
        public void DeleteLeaveRequest(int id, int currentId)
        {
            var leaveRequest = _leaveRequestRepository.GetYourRequestById(id, currentId);
            if (leaveRequest != null)
            {
                _leaveRequestRepository.DeleteLeaveRequest(leaveRequest);
                _leaveRequestRepository.SaveChanges();
            }
        }
    }
}
