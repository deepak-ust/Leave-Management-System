using System.ComponentModel.DataAnnotations;
using static Leave_Management_System.Data.Models.Enums;
namespace Leave_Management_System.Data.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Designation { get; set; }
        public Band Band { get; set; }
        public string Department { get; set; }
        [Display(Name = "Manager ID")]
        public int ManagerId { get; set; }
        [Display(Name = "Joining Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime JoiningDate { get; set; }
        //public string Password { get; set; }
        public ICollection<LeaveRequest>? LeaveRequests { get; set; }
        public ICollection<EmployeeLeaveBalance>? LeaveBalances { get; set; }
        //public int RoleId { get; set; }
        //public Role? Role { get; set; }
    }
}