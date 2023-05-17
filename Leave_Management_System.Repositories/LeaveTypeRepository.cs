using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class LeaveTypeRepository : ILeaveTypeRepository
    {
        private readonly LeaveManagementDbContext _context;
        public LeaveTypeRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public void CreateLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Add(leaveType);
        }
        public void AddLeaveTypeRule(Rule rule)
        {
            _context.Rules.Add(rule);
        }
        public void DeleteLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Remove(leaveType);
        }
        public void DeleteRule(Rule rule)
        {
            _context.Rules.Remove(rule);
        }
        public IEnumerable<LeaveType> GetAllLeaveTypes()
        {
            return _context.LeaveTypes.ToList();
        }
        public IEnumerable<Rule> GetRules(int leaveTypeId)
        {
            return _context.Rules.Where(r => r.LeaveTypeId == leaveTypeId);
        }
        public LeaveType GetLeaveTypeById(int id)
        {
            return _context.LeaveTypes.FirstOrDefault(l => l.Id == id);
        }
        public LeaveType GetLeaveTypeByName(string leaveName)
        {
            return _context.LeaveTypes.FirstOrDefault(l => l.Name == leaveName);
        }
        public void ApplyRule(Enums.Band band, int leaveTypeId)
        {
            var rowsToUpdate = _context.Rules.Where(r => r.Band == band && r.LeaveTypeId == leaveTypeId);
            foreach (var row in rowsToUpdate)
            {
                row.IsApplicable = false;
            }
        }
        public Rule GetRuleById(int id)
        {
            return _context.Rules.FirstOrDefault(r => r.Id == id);
        }
        public Rule GetAppliedRule(Enums.Band band, int leaveTypeId)
        {
            return _context.Rules.FirstOrDefault(r => r.Band == band && r.LeaveTypeId == leaveTypeId && r.IsApplicable == true);
        }
        public void UpdateRule(Rule rule)
        {
            _context.Rules.Update(rule);
        }
        public void UpdateLeaveType(LeaveType leaveType)
        {
            _context.LeaveTypes.Update(leaveType);
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
