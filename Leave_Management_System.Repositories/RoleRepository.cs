using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
using System.Data;
namespace Leave_Management_System.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly LeaveManagementDbContext _context;
        public RoleRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }
        public IEnumerable<UserRole> GetUserRoles(int userId)
        {
            return _context.UserRoles.ToList().Where(ur => ur.UserId == userId);
        }
        public Role GetRole(int id)
        {
            var role = _context.Roles.FirstOrDefault(r => r.Id == id);
            return role;
        }
        public bool IsRoleExist(UserRole userRole)
        {
            return _context.UserRoles.Any(ur => ur.RoleId == userRole.RoleId && ur.UserId == userRole.UserId);
        }
        public void AddUserRole(UserRole userRole)
        {
            _context.UserRoles.Add(userRole);
            _context.SaveChanges();
        }
        public void DeleteRole(int roleId, int userId)
        {
            var userRole = _context.UserRoles.FirstOrDefault(ur => ur.RoleId == roleId && ur.UserId == userId);
            _context.UserRoles.Remove(userRole);
            _context.SaveChanges();
        }
    }
}
