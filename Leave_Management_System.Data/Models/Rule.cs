using System.ComponentModel.DataAnnotations;
using static Leave_Management_System.Data.Models.Enums;
namespace Leave_Management_System.Data.Models
{
    public class Rule
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType? LeaveType { get; set; }
        public Band Band { get; set; }
        [Display(Name = "Default Balance")]
        public double DefaultBalance { get; set; }
        public double Credit { get; set; }
        [Display(Name = "Credit Frequency")]
        public int LeaveCreditFrequency { get; set; }
        [Display(Name = "Allowed Leaves")]
        public int AllowedLeaves { get; set; }
        [Display(Name = "Active")]
        public bool IsApplicable { get; set; }
    }
}
