using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly LeaveManagementDbContext _context;
        public EmployeeRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public void CreateEmployee(Employee employee)
        {
            _context.Employees.Add(employee);
        }
        public void DeleteEmployee(Employee employee)
        {
            _context.Employees.Remove(employee);
        }
        public bool CheckEmail(string email)
        {
            return _context.Employees.Any(e => e.Email == email);
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }
        public IEnumerable<Employee> GetEmployeesByBand(Enums.Band band)
        {
            return _context.Employees.ToList().Where(e => e.Band == band);
        }
        public Employee GetEmployeeById(int id)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                return employee;
            }
            else
                return null;
        }
        public Employee GetEmployeeByManagerId(int id, int currentUser)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id && e.ManagerId == currentUser);
            if (employee != null)
            {
                return employee;
            }
            else
                return null;
        }
        public IEnumerable<Employee> GetEmployeesByManager(int id)
        {
            var user = _context.Users.FirstOrDefault(e => e.EmployeeId == id);
            var employees = _context.UserRoles.ToList().Where(u => u.RoleId == 2).Any(ui => ui.UserId == user.Id);
            if (employees)
            {
                return _context.Employees.ToList().Where(e => e.ManagerId == user.EmployeeId);
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Employee> GetManagers()
        {
            var managerUserIds = _context.UserRoles.ToList().Where(ur => ur.RoleId == 2);
            var managers = new List<Employee>();
            foreach (var managerUserId in managerUserIds)
            {
                var managerUser = _context.Users.FirstOrDefault(u => u.Id == managerUserId.UserId);
                var employee = _context.Employees.FirstOrDefault(e => e.Id == managerUser.EmployeeId);
                managers.Add(employee);
            }
            return managers;
        }
        public void SaveChanges()
        {
            _context.SaveChanges();
        }
        public void UpdateEmployee(Employee employee)
        {
            _context.Employees.Update(employee);
        }
    }
}
