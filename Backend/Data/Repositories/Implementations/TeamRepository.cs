using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Implementations
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllTeams()
        {
            return await _context.Teams.ToListAsync();
        }

        public async Task<Team> GetTeamById(int teamId)
        {
            return await _context.Teams.FindAsync(teamId);
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

        public async Task DeleteTeam(int teamId)
        {
            var team = await _context.Teams.FindAsync(teamId);
            if (team != null)
            {
                _context.Teams.Remove(team);
                await _context.SaveChangesAsync();
            }
        }
    }
}
