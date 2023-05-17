using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly LeaveManagementDbContext _context;
        public UserRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
        public bool IsManagerExist(int managerId)
        {
            var managerUser = _context.Users.FirstOrDefault(u => u.EmployeeId == managerId);
            if (managerUser == null)
            {
                return false;
            }
            else
            {
                return _context.UserRoles.Any(ur => ur.RoleId == 2 && ur.UserId == managerUser.Id);
            }
        }
        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }
        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
        public User GetUserByEmployeeId(int id)
        {
            return _context.Users.FirstOrDefault(u => u.EmployeeId == id);
        }
        public void UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();
        }
        public void DeleteUser(int id)
        {
            _context.Users.Remove(GetUserById(id));
            _context.SaveChanges();
        }
    }
}
