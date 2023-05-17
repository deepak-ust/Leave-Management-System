using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface ILeaveTypeService
    {
        IEnumerable<LeaveType> GetLeaveTypes();
        IEnumerable<Rule> GetRules(int leaveTypeId);
        LeaveType GetLeaveTypeById(int id);
        Rule GetRuleById(int id);
        Rule GetAppliedRule(Enums.Band band, int leaveTypeId);
        void UpdateRule(Rule rule);
        LeaveType GetLeaveTypeByName(string leaveName);
        LeaveType AddLeaveType(LeaveType leaveType);
        void AddLeaveTypeRule(Rule rule);
        void ApplyRule(Enums.Band band, int leaveTypeId);
        LeaveType UpdateLeaveType(LeaveType leaveType);
        LeaveType UpdateSickLeaveType(LeaveType leaveType);
        void DeleteLeaveType(int id);
        void DeleteRule(int id);
    }
}
