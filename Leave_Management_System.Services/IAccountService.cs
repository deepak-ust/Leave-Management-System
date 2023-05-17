using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Services
{
    public interface IAccountService
    {
        User ValidateUser(string username, string password);
    }
}
