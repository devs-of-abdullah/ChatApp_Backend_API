
using Entities;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace DataAccess
{  

    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user) => await _context.Users.AddAsync(user);
        public async Task SaveAsync() => await _context.SaveChangesAsync();

        public async Task<User?> GetUserByIdAsync(int id)
            => await _context.Users.FindAsync(id);

        public async Task<User?> GetUserByEmailAsync(string email)
            => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

        public async Task<bool> UserExistsAsync(string email)
            => await _context.Users.AnyAsync(u => u.Email == email);
        public async Task<bool> RemoveUserAsync(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user == null) return false;

            _context.Users.Remove(user);
           
            return true; 
        }
       

    }
}