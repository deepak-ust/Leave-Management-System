using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        bool IsManagerExist(int managerId);
        void AddUser(User user);
        User GetUserById(int id);
        User GetUserByEmployeeId(int id);
        void UpdateUser(User user);
        void DeleteUser(int id);
    }
}
