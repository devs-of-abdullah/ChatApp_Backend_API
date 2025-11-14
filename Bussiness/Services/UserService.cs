using Entities;
using DataAccess;

namespace Business
{

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> RegisterUserAsync(string name, string email, string password)
        {
            if (await _userRepository.UserExistsAsync(email))
                throw new Exception("User with this email already exists");

            
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(password);


            var user = new User
            {   Fullname = name,
                Email = email,
                PasswordHash = passwordHash
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }

        public async Task<User?> LoginUserAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email);
            if (user == null) return null;

            if (BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;
            return null;
            
        } 
    }
}