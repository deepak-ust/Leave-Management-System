using System.ComponentModel.DataAnnotations;
namespace Leave_Management_System.Data.Models
{
    public class LeaveRequest
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public string LeaveType { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:d}")]
        [Display(Name = "End Date")]
        public DateTime? EndDate { get; set; }
        public string? Reason { get; set; }
        public string? Status { get; set; }
        [Display(Name = "Applied On")]
        public DateTime AppliedOn { get; set; }
        [Display(Name = "Upload File")]
        public string? FileUrl { get; set; }
        public Employee? Employee { get; set; }
    }
}
