using Leave_Management_System.Data;
using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly LeaveManagementDbContext _context;
        public AccountRepository(LeaveManagementDbContext context)
        {
            _context = context;
        }
        public User ValidateUser(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        //public static string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
    }
}
