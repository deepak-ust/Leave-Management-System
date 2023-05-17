namespace Leave_Management_System.Data.Models
{
    public class EmployeeLeaveBalance
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee Employee { get; set; }
        public int LeaveTypeId { get; set; }
        public LeaveType LeaveType { get; set; }
        public double? Balance { get; set; }
        public double? CarryForward { get; set; }
    }
}
