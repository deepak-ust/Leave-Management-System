using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface IRoleService
    {
        IEnumerable<Role> GetRoles();
        Role GetRole(int id);
        IEnumerable<UserRole> GetUserRoles(int userId);
        bool IsRoleExist(UserRole userRole);
        void AddUserRole(UserRole userRole);
        void DeleteRole(int roleId, int userId);
    }
}
