using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class UserStatsRepository : IUserStatsRepository
    {
        private readonly AppDbContext _context;

        public UserStatsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<UserStats> GetUserStatsByIdAsync(int id)
        {
            return await _context.UserStats.FindAsync(id);
        }

        public async Task<IEnumerable<UserStats>> GetAllUserStatsAsync()
        {
            return await _context.UserStats.ToListAsync();
        }

        public async Task AddUserStatsAsync(UserStats userStats)
        {
            await _context.UserStats.AddAsync(userStats);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUserStatsAsync(UserStats userStats)
        {
            _context.Entry(userStats).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserStatsAsync(int id)
        {
            var userStats = await _context.UserStats.FindAsync(id);
            if (userStats != null)
            {
                _context.UserStats.Remove(userStats);
                await _context.SaveChangesAsync();
            }
        }
    }
}
