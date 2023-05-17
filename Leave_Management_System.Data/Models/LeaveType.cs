namespace Leave_Management_System.Data.Models
{
    public class LeaveType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<EmployeeLeaveBalance>? EmployeeBalances { get; set; }
        public ICollection<Rule>? Rules { get; set; }
    }
}
