using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface IRoleRepository
    {
        IEnumerable<Role> GetRoles();
        Role GetRole(int id);
        IEnumerable<UserRole> GetUserRoles(int userId);
        void AddUserRole(UserRole userRole);
        bool IsRoleExist(UserRole userRole);
        void DeleteRole(int roleId, int userId);
    }
}
