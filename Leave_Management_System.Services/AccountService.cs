using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public User ValidateUser(string email, string password)
        {
            var user = _accountRepository.ValidateUser(email);
            if (user == null)
            {
                return null;
            }
            else
            {
                var decryptpassword = Base64Decode(user.Password);
                if (String.Equals(password, decryptpassword))
                {
                    return user;
                }
                else
                {
                    return null;
                }
            }
        }
        public static string Base64Decode(string base64EncodedData)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
    }
}
