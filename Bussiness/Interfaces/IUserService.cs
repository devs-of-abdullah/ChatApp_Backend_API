

using Entities;

namespace Business
{
    public interface IUserService
    {
        Task<User> RegisterUserAsync(string name, string email, string password);
        Task<User?> LoginUserAsync(string email, string password);
    }
}
