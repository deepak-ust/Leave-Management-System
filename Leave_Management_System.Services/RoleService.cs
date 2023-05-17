using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
using System.Data;
namespace Leave_Management_System.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }
        public IEnumerable<Role> GetRoles()
        {
            var leaves = _roleRepository.GetRoles();
            return leaves.Select(r => new Role
            {
                Id = r.Id,
                Name = r.Name,
            });
        }
        public Role GetRole(int id)
        {
            var role = _roleRepository.GetRole(id);
            return role;
        }
        public bool IsRoleExist(UserRole userRole)
        {
            return _roleRepository.IsRoleExist(userRole);
        }
        public IEnumerable<UserRole> GetUserRoles(int userId)
        {
            var roles = _roleRepository.GetUserRoles(userId);
            return roles.Select(ur => new UserRole
            {
                UserId = ur.UserId,
                RoleId = ur.RoleId
            });
        }
        public void AddUserRole(UserRole userRole)
        {
            _roleRepository.AddUserRole(userRole);
        }
        public void DeleteRole(int roleId, int userId)
        {
            _roleRepository.DeleteRole(roleId, userId);
        }
    }
}
