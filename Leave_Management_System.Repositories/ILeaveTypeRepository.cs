using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface ILeaveTypeRepository
    {
        IEnumerable<LeaveType> GetAllLeaveTypes();
        IEnumerable<Rule> GetRules(int leaveTypeId);
        void ApplyRule(Enums.Band band, int leaveTypeId);
        LeaveType GetLeaveTypeById(int id);
        Rule GetRuleById(int id);
        Rule GetAppliedRule(Enums.Band band, int leaveTypeId);
        void UpdateRule(Rule rule);
        LeaveType GetLeaveTypeByName(string leaveName);
        void CreateLeaveType(LeaveType leaveType);
        void AddLeaveTypeRule(Rule rule);
        void UpdateLeaveType(LeaveType leaveType);
        void DeleteLeaveType(LeaveType leaveType);
        void DeleteRule(Rule rule);
        void SaveChanges();
    }
}
