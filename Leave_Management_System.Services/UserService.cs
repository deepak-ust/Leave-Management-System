using Leave_Management_System.Data.Models;
using Leave_Management_System.Repositories;
namespace Leave_Management_System.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public IEnumerable<User> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return users.Select(u => new User
            {
                Id = u.Id,
                EmployeeId = u.EmployeeId,
                Email = u.Email,
                Password = u.Password
            });
        }
        public bool IsManagerExist(int managerId)
        {
            return _userRepository.IsManagerExist(managerId);
        }
        public void AddUser(User user)
        {
            _userRepository.AddUser(user);
        }
        public User GetUserById(int id)
        {
            return _userRepository.GetUserById(id);
        }
        public User GetUserByEmployeeId(int id)
        {
            return _userRepository.GetUserByEmployeeId(id);
        }
        public void UpdateUser(User userObj)
        {
            var user = _userRepository.GetUserById(userObj.Id);
            //var encryptedPassword=Base64Encode(userObj.Password);
            if (user != null)
            {
                user.Id = userObj.Id;
                user.EmployeeId = userObj.EmployeeId;
                user.Email = userObj.Email;
                user.Password = userObj.Password;
                _userRepository.UpdateUser(user);
            }
        }
        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
        //public string Base64Encode(string plainText)
        //{
        //    var plainTextBytes = Encoding.UTF8.GetBytes(plainText);
        //    return System.Convert.ToBase64String(plainTextBytes);
        //}
        //public string Base64Decode(string base64EncodedData)
        //{
        //    var base64EncodedBytes = Convert.FromBase64String(base64EncodedData);
        //    return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        //}
    }
}
