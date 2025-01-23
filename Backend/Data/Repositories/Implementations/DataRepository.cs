using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Models;
using MokSportsApp.Data;

public class DataRepository : IDataRepository
{
    private readonly AppDbContext _context;

    public DataRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Game>> GetCompletedGamesByWeekAsync(int weekId)
    {
        return await _context.Games
            .Where(g => g.Week == weekId && g.GameStatus == "Final")
            .ToListAsync();
    }

    public async Task<IEnumerable<Franchise>> GetAllFranchisesAsync()
    {
        return await _context.Franchises
            .Include(f => f.Team1)
            .Include(f => f.Team2)
            .Include(f => f.Team3)
            .Include(f => f.Team4)
            .Include(f => f.Team5)
            .ToListAsync();
    }

    public async Task<FranchiseLocksLoads> GetLocksLoadsForFranchiseAndWeekAsync(int franchiseId, int weekId)
    {
        return await _context.FranchiseLocksLoads
            .FirstOrDefaultAsync(ll => ll.FranchiseId == franchiseId && ll.WeekId == weekId);
    }
}
