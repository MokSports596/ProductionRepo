using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class UserImplementation : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserImplementation(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Users
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team1)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team2)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team3)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team4)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team5)
                                 .ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team1)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team2)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team3)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team4)
                                 .Include(u => u.Franchises)
                                 .ThenInclude(f => f.Team5)
                                 .FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // New method to get user by username
        public async Task<User?> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddUser(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUser(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
