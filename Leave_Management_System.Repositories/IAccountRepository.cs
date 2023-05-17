using Leave_Management_System.Data.Models;
namespace Leave_Management_System.Repositories
{
    public interface IAccountRepository
    {
        User ValidateUser(string email);
    }
}
