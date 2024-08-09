using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;
using MokSportsApp.Services.Interfaces;
using MokSportsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace MokSportsApp.Services.Implementations
{
    public class TeamService : ITeamService
    {
        private readonly AppDbContext _context;

        public TeamService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(int id)
        {
            return await _context.Teams.FindAsync(id);
        }

        public async Task AddTeam(Team team)
        {
            await _context.Teams.AddAsync(team);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTeam(Team team)
        {
            _context.Teams.Update(team);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTeam(int id)
        {
            var team = await _context.Teams.FindAsync(id);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
