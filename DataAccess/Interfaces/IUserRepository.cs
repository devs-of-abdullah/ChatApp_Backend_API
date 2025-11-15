
using Entities;

namespace DataAccess
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        Task SaveAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByEmailAsync(string email);
        Task<bool> UserExistsAsync(string email);
        Task<bool> RemoveUserAsync(int id);
    }
}
