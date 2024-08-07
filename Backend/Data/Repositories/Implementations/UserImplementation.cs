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
            return await _context.Users.Include(u => u.Franchises)
                                        .ThenInclude(f => f.FranchiseTeams)
                                        .ThenInclude(ft => ft.Team)
                                        .ToListAsync();
        }

        public async Task<User> GetUserById(int userId)
        {
            return await _context.Users.Include(u => u.Franchises)
                                        .ThenInclude(f => f.FranchiseTeams)
                                        .ThenInclude(ft => ft.Team)
                                        .FirstOrDefaultAsync(u => u.UserId == userId);
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
