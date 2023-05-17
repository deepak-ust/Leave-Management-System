using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface IEmployeeRepository
    {
        IEnumerable<Employee> GetAllEmployees();
        IEnumerable<Employee> GetEmployeesByBand(Enums.Band band);
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByManagerId(int id, int currentUser);
        void CreateEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        bool CheckEmail(string email);
        IEnumerable<Employee> GetEmployeesByManager(int id);
        IEnumerable<Employee> GetManagers();
        void SaveChanges();
    }
}
