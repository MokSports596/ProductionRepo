using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.DTO;
using MokSportsApp.Models;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class SeasonRepository : ISeasonRepository
    {
        private readonly AppDbContext _context;
        public SeasonRepository(AppDbContext context)
        {
            _context = context;
        }


        public async Task AddSeason(Season season)
        {
            season.CreationTime = DateTime.UtcNow;
            _context.Seasons.Add(season);
            await _context.SaveChangesAsync();
        }

        public async Task<Season> GetSeason(int id)
        {
            return await _context.Seasons.FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Season> GetSeasonByName(string name)
        {
            return await _context.Seasons.FirstOrDefaultAsync(a => a.Name == name);
        }

        public async Task<bool> CheckActiveSeason()
        {
            return await _context.Seasons.AnyAsync(a => a.Status == SeasonStatus.Active);
        }

        public async Task<Season> GetActiveSeason()
        {
            return await _context.Seasons.FirstOrDefaultAsync(a => a.Status == SeasonStatus.Active);
        }

        public async Task<List<Season>> GetAllSeasons()
        {
            return await _context.Seasons.OrderByDescending(a => a.CreationTime).ToListAsync();
        }

        public async Task UpdateSeason(Season season)
        {
            _context.Seasons.Update(season);
            await SaveChangesAsync();
        }

        private async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}