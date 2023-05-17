using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
using Leave_Management_System.Services;
using Moq;
namespace Leave_Management_System.Tests
{
    public class EmployeeServiceTests
    {
        [Fact]
        public void TestGetEmployeeById()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                //Password = "12345678",
                ManagerId = 4
            };
            mockRepository.Setup(r => r.GetEmployeeById(newEmployee.Id)).Returns(newEmployee);
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.GetEmployeeById(1);
            //newEmployee.Password = employeeService.Base64Decode(newEmployee.Password);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(newEmployee.Name, result.Name);
            Assert.Equal(newEmployee.Email, result.Email);
            Assert.Equal(newEmployee.Designation, result.Designation);
            Assert.Equal(newEmployee.Department, result.Department);
            Assert.Equal(newEmployee.ManagerId, result.ManagerId);
            //Assert.Equal(newEmployee.Password, result.Password);
            Assert.Equal(newEmployee.ManagerId, result.ManagerId);
        }
        [Fact]
        public void TestGetEmployees()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            mockRepository.Setup(r => r.GetAllEmployees())
                .Returns(new List<Employee>
                {
            new Employee
            {
                Id = 1,
                Name = "John",
                Email = "john@example.com",
                Designation = "Developer",
                Department = "IT",
                JoiningDate = new DateTime(2022, 1, 1),
                //Password = "password1",
                ManagerId = 2
            },
            new Employee
            {
                Id = 2,
                Name = "Sarah",
                Email = "sarah@example.com",
                Designation = "Manager",
                Department = "HR",
                JoiningDate = new DateTime(2022, 2, 1),
                //Password = "password2",
                ManagerId = 1
            }
                });
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.GetEmployees();
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("John", result.First().Name);
            Assert.Equal("Developer", result.First().Designation);
            Assert.Equal("Sarah", result.Last().Name);
            Assert.Equal("Manager", result.Last().Designation);
        }
        [Fact]
        public void TestDeleteEmployee()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                //Password = "12345678",
                ManagerId = 4
            };
            mockRepository.Setup(r => r.GetEmployeeById(newEmployee.Id)).Returns(newEmployee);
            mockRepository.Setup(r => r.DeleteEmployee(newEmployee));
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var employee = employeeService.AddEmployee(newEmployee);
            var result = employeeService.DeleteEmployee(employee.Id);
            // Assert
            Assert.Equal("Test Employee", employee.Name);
            Assert.True(result);
            mockRepository.Verify(r => r.DeleteEmployee(newEmployee), Times.Once);
        }
        [Fact]
        public void TestAddEmployee()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            };
            mockRepository.Setup(r => r.CreateEmployee(newEmployee));
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.AddEmployee(newEmployee);
            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Employee", result.Name);
            Assert.Equal("Tester", result.Designation);
            Assert.Equal(4, result.ManagerId);
        }
        [Fact]
        public void TestGetEmployeeByManagerId()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            };
            var newManager = new Employee
            {
                Id = 4,
                Name = "Nandu",
                Email = "manager@ust.com",
                Designation = "Manager",
                Department = "WK",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 6
            };
            mockRepository.Setup(r => r.CreateEmployee(newEmployee));
            mockRepository.Setup(r => r.CreateEmployee(newManager));
            mockRepository.Setup(r => r.GetEmployeeByManagerId(newEmployee.Id, newManager.Id)).Returns(newEmployee);
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.GetEmployeeByManagerId(newEmployee.Id, newManager.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(newEmployee.Name, result.Name);
            Assert.Equal(newEmployee.Designation, result.Designation);
            Assert.Equal(newManager.Id, result.ManagerId);
        }
        [Fact]
        public void TestGetEmployeesByManager()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee1 = new Employee
            {
                Id = 1,
                Name = "Test Employee1",
                Email = "test.employee1@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            };
            var newEmployee2 = new Employee
            {
                Id = 2,
                Name = "Test Employee2",
                Email = "test.employee2@example.com",
                Designation = "Developer",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            };
            var newEmployee3 = new Employee
            {
                Id = 3,
                Name = "Test Employee3",
                Email = "test.employee3@example.com",
                Designation = "Developer",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 5
            };
            var newManager = new Employee
            {
                Id = 4,
                Name = "Nandu",
                Email = "manager@ust.com",
                Designation = "Manager",
                Department = "WK",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 6
            };
            mockRepository.Setup(r => r.CreateEmployee(newEmployee1));
            mockRepository.Setup(r => r.CreateEmployee(newEmployee2));
            mockRepository.Setup(r => r.CreateEmployee(newEmployee3));
            mockRepository.Setup(r => r.CreateEmployee(newManager));
            mockRepository.Setup(r => r.GetEmployeesByManager(newManager.Id))
                .Returns(new List<Employee>
                {
            new Employee
            {
                Id = 1,
                Name = "Test Employee1",
                Email = "test.employee1@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
        },
            new Employee
            {
               Id = 2,
                Name = "Test Employee2",
                Email = "test.employee2@example.com",
                Designation = "Developer",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            }
                });
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.GetEmployeesByManager(newManager.Id);
            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
            Assert.Equal("Test Employee1", result.First().Name);
            Assert.Equal("Tester", result.First().Designation);
            Assert.Equal("Test Employee2", result.Last().Name);
            Assert.Equal("Developer", result.Last().Designation);
        }
        [Fact]
        public void TestUpdateEmployee()
        {
            // Arrange
            var mockRepository = new Mock<IEmployeeRepository>();
            var newEmployee = new Employee
            {
                Id = 1,
                Name = "Test Employee",
                Email = "test.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 4
            };
            var newUpdatedEmployee = new Employee
            {
                Id = 1,
                Name = "Updated Employee",
                Email = "updated.employee@example.com",
                Designation = "Tester",
                Department = "QA",
                JoiningDate = new DateTime(2022, 1, 1),
                ManagerId = 5
            };
            mockRepository.Setup(r => r.CreateEmployee(newEmployee));
            mockRepository.Setup(r => r.GetEmployeeById(newEmployee.Id)).Returns(newEmployee);
            mockRepository.Setup(r => r.UpdateEmployee(newUpdatedEmployee));
            var employeeService = new EmployeeService(mockRepository.Object);
            // Act
            var result = employeeService.UpdateEmployee(newUpdatedEmployee);
            // Assert
            Assert.True(result);
            Assert.Equal(newUpdatedEmployee.Id, newEmployee.Id);
            Assert.Equal(newUpdatedEmployee.Name, newEmployee.Name);
            Assert.Equal(newUpdatedEmployee.Email, newEmployee.Email);
            Assert.Equal(newUpdatedEmployee.Designation, newEmployee.Designation);
            Assert.Equal(newUpdatedEmployee.Department, newEmployee.Department);
            Assert.Equal(newUpdatedEmployee.JoiningDate, newEmployee.JoiningDate);
            Assert.Equal(newUpdatedEmployee.ManagerId, newEmployee.ManagerId);
        }
    }
}
