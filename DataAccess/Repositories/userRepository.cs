
using Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{  

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user) => await _context.users.AddAsync(user);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<User?> GetUserByIdAsync(int id)
            => await _context.users.FindAsync(id);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<bool> UserExistsAsync(string email)
            => await _context.users.AnyAsync(u => u.Email == email);
    }
}