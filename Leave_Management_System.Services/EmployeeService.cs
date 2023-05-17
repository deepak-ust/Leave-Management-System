using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
using System.Text;
namespace Leave_Management_System.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }
        public Employee AddEmployee(Employee employeeObj)
        {
            //var encryptpassword = Base64Encode(employeeObj.Password);
            var employee = new Employee
            {
                Id = employeeObj.Id,
                Name = employeeObj.Name,
                Email = employeeObj.Email,
                Designation = employeeObj.Designation,
                Band = employeeObj.Band,
                Department = employeeObj.Department,
                ManagerId = employeeObj.ManagerId,
                JoiningDate = employeeObj.JoiningDate
                //Password = encryptpassword,
                //RoleId = employeeObj.RoleId,
            };
            _employeeRepository.CreateEmployee(employee);
            _employeeRepository.SaveChanges();
            return employee;
        }
        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            bool flag = false;
            if (employee != null)
            {
                _employeeRepository.DeleteEmployee(employee);
                _employeeRepository.SaveChanges();
                flag = true;
                return flag;
            }
            else
            {
                return flag;
            }
        }
        public Employee GetEmployeeById(int id)
        {
            var employee = _employeeRepository.GetEmployeeById(id);
            if (employee == null)
            {
                return null;
            }
            //var decryptpassword = Base64Decode(employee.Password);
            return new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Designation = employee.Designation,
                Band = employee.Band,
                Department = employee.Department,
                ManagerId = employee.ManagerId,
                JoiningDate = employee.JoiningDate
                //Password = decryptpassword,
                //RoleId = employee.RoleId
            };
        }
        public Employee GetEmployeeByManagerId(int id, int currentUser)
        {
            var employee = _employeeRepository.GetEmployeeByManagerId(id, currentUser);
            if (employee == null)
            {
                return null;
            }
            //var decryptpassword = Base64Decode(employee.Password);
            return new Employee
            {
                Id = employee.Id,
                Name = employee.Name,
                Email = employee.Email,
                Designation = employee.Designation,
                Band = employee.Band,
                Department = employee.Department,
                ManagerId = employee.ManagerId,
                JoiningDate = employee.JoiningDate
                //Password = decryptpassword
            };
        }
        public IEnumerable<Employee> GetEmployees()
        {
            var employees = _employeeRepository.GetAllEmployees();
            return employees.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Designation = e.Designation,
                Department = e.Department,
                JoiningDate = e.JoiningDate,
                //Password = e.Password,
                ManagerId = e.ManagerId
            });
        }
        public IEnumerable<Employee> GetEmployeesByBand(Enums.Band band)
        {
            var employees = _employeeRepository.GetEmployeesByBand(band);
            return employees.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name,
                Email = e.Email,
                Designation = e.Designation,
                Department = e.Department,
                JoiningDate = e.JoiningDate,
                //Password = e.Password,
                ManagerId = e.ManagerId
            });
        }
        public bool CheckEmail(string email)
        {
            bool result = _employeeRepository.CheckEmail(email);
            return result;
        }
        public bool UpdateEmployee(Employee employeeObj)
        {
            var employee = _employeeRepository.GetEmployeeById(employeeObj.Id);
            //var encryptpassword = Base64Encode(employeeObj.Password);
            bool flag = false;
            if (employee != null)
            {
                employee.Id = employeeObj.Id;
                employee.Name = employeeObj.Name;
                employee.Email = employeeObj.Email;
                employee.Designation = employeeObj.Designation;
                employee.Band = employeeObj.Band;
                employee.Department = employeeObj.Department;
                employee.ManagerId = employeeObj.ManagerId;
                employee.JoiningDate = employeeObj.JoiningDate;
                //employee.Password = encryptpassword;
                //employee.RoleId = employeeObj.RoleId;
                _employeeRepository.UpdateEmployee(employee);
                _employeeRepository.SaveChanges();
                flag = true;
                return flag; ;
            }
            else
            {
                return flag;
            }
        }
        public bool CheckManager(string managerId, string managerName)
        {
            var stringId = managerId.Substring(3);
            int Id = int.Parse(stringId);
            var manager = _employeeRepository.GetEmployeeById(Id);
            if (manager != null)
            {
                if (manager.Name == managerName && (manager.Designation == "Manager" || manager.Designation == "Administrator"))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public IEnumerable<Employee> GetEmployeesByManager(int id)
        {
            var employees = _employeeRepository.GetEmployeesByManager(id);
            if (employees != null)
            {
                return employees.Select(e => new Employee
                {
                    Id = e.Id,
                    Name = e.Name,
                    Email = e.Email,
                    Designation = e.Designation,
                    Department = e.Department,
                    JoiningDate = e.JoiningDate
                    //Password = e.Password,
                    //RoleId = e.RoleId
                });
            }
            else
            {
                return null;
            }
        }
        public IEnumerable<Employee> GetManagers()
        {
            var employees = _employeeRepository.GetManagers();
            return employees.Select(e => new Employee
            {
                Id = e.Id,
                Name = e.Name
            });
        }
        public string Base64Encode(string plainText)
        {
            var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
