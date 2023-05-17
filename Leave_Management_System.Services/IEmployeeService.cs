using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface IEmployeeService
    {
        IEnumerable<Employee> GetEmployees();
        IEnumerable<Employee> GetEmployeesByBand(Enums.Band band);
        Employee GetEmployeeById(int id);
        Employee GetEmployeeByManagerId(int id, int currentId);
        Employee AddEmployee(Employee employee);
        bool CheckEmail(string email);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int id);
        bool CheckManager(string managerId, string managerName);
        IEnumerable<Employee> GetEmployeesByManager(int id);
        IEnumerable<Employee> GetManagers();
    }
}
