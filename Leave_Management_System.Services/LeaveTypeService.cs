using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class LeaveTypeService : ILeaveTypeService
    {
        private readonly ILeaveTypeRepository _leaveTypeRepository;
        public LeaveTypeService(ILeaveTypeRepository leaveTypeRepository)
        {
            _leaveTypeRepository = leaveTypeRepository;
        }
        public LeaveType AddLeaveType(LeaveType leaveObj)
        {
            var leaveType = new LeaveType
            {
                Id = leaveObj.Id,
                Name = leaveObj.Name
            };
            _leaveTypeRepository.CreateLeaveType(leaveType);
            _leaveTypeRepository.SaveChanges();
            return leaveType;
        }
        public void AddLeaveTypeRule(Rule rule)
        {
            _leaveTypeRepository.AddLeaveTypeRule(rule);
            _leaveTypeRepository.SaveChanges();
        }
        public void DeleteLeaveType(int id)
        {
            var leave = _leaveTypeRepository.GetLeaveTypeById(id);
            if (leave != null)
            {
                _leaveTypeRepository.DeleteLeaveType(leave);
                _leaveTypeRepository.SaveChanges();
            }
        }
        public void DeleteRule(int id)
        {
            var rule = _leaveTypeRepository.GetRuleById(id);
            if (rule != null)
            {
                _leaveTypeRepository.DeleteRule(rule);
                _leaveTypeRepository.SaveChanges();
            }
        }
        public LeaveType GetLeaveTypeById(int id)
        {
            var leaveType = _leaveTypeRepository.GetLeaveTypeById(id);
            if (leaveType == null)
            {
                return null;
            }
            return new LeaveType
            {
                Id = leaveType.Id,
                Name = leaveType.Name
            };
        }
        public LeaveType GetLeaveTypeByName(string leaveName)
        {
            var leaveType = _leaveTypeRepository.GetLeaveTypeByName(leaveName);
            if (leaveType == null)
            {
                return null;
            }
            return new LeaveType
            {
                Id = leaveType.Id,
                Name = leaveType.Name
            };
        }
        public IEnumerable<LeaveType> GetLeaveTypes()
        {
            var leaves = _leaveTypeRepository.GetAllLeaveTypes();
            return leaves.Select(l => new LeaveType
            {
                Id = l.Id,
                Name = l.Name
            });
        }
        public IEnumerable<Rule> GetRules(int leaveTypeId)
        {
            var rules = _leaveTypeRepository.GetRules(leaveTypeId);
            return rules.Select(r => new Rule
            {
                Id = r.Id,
                Name = r.Name,
                LeaveTypeId = r.LeaveTypeId,
                Band = r.Band,
                DefaultBalance = r.DefaultBalance,
                Credit = r.Credit,
                LeaveCreditFrequency = r.LeaveCreditFrequency,
                AllowedLeaves = r.AllowedLeaves,
                IsApplicable = r.IsApplicable
            });
        }
        public void ApplyRule(Enums.Band band, int leaveTypeId)
        {
            _leaveTypeRepository.ApplyRule(band, leaveTypeId);
            _leaveTypeRepository.SaveChanges();
        }
        public Rule GetRuleById(int id)
        {
            var rule = _leaveTypeRepository.GetRuleById(id);
            return rule;
        }
        public Rule GetAppliedRule(Enums.Band band, int leaveTypeId)
        {
            var rule = _leaveTypeRepository.GetAppliedRule(band, leaveTypeId);
            return rule;
        }
        public void UpdateRule(Rule ruleObj)
        {
            var rule = _leaveTypeRepository.GetRuleById(ruleObj.Id);
            if (rule != null)
            {
                rule.Id = ruleObj.Id;
                rule.Name = ruleObj.Name;
                rule.Band = ruleObj.Band;
                rule.DefaultBalance = ruleObj.DefaultBalance;
                rule.Credit = ruleObj.Credit;
                rule.LeaveCreditFrequency = ruleObj.LeaveCreditFrequency;
                rule.AllowedLeaves = ruleObj.AllowedLeaves;
                rule.IsApplicable = ruleObj.IsApplicable;
                _leaveTypeRepository.UpdateRule(rule);
                _leaveTypeRepository.SaveChanges();
            }
        }
        public LeaveType UpdateLeaveType(LeaveType leaveTypeObj)
        {
            var leaveType = _leaveTypeRepository.GetLeaveTypeById(leaveTypeObj.Id);
            if (leaveType != null)
            {
                leaveType.Id = leaveTypeObj.Id;
                leaveType.Name = leaveTypeObj.Name;
                _leaveTypeRepository.UpdateLeaveType(leaveType);
                _leaveTypeRepository.SaveChanges();
            }
            return leaveType;
        }
        public LeaveType UpdateSickLeaveType(LeaveType leaveTypeObj)
        {
            var leaveType = _leaveTypeRepository.GetLeaveTypeById(leaveTypeObj.Id);
            if (leaveType != null)
            {
                leaveType.Id = leaveTypeObj.Id;
                leaveType.Name = leaveTypeObj.Name;
                _leaveTypeRepository.UpdateLeaveType(leaveType);
                _leaveTypeRepository.SaveChanges();
            }
            return leaveType;
        }
    }
}
