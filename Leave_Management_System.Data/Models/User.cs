namespace Leave_Management_System.Data.Models
{
    public class User
    {
        public int Id { get; set; }
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public ICollection<UserRole>? UserRoles { get; set; }
    }
}
