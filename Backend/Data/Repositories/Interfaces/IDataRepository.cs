using System.Collections.Generic;
using System.Threading.Tasks;
using MokSportsApp.Models;
using MokSportsApp.Data;


public interface IDataRepository
{
    Task<IEnumerable<Game>> GetCompletedGamesByWeekAsync(int weekId);
    Task<IEnumerable<Franchise>> GetAllFranchisesAsync();
    Task<FranchiseLocksLoads> GetLocksLoadsForFranchiseAndWeekAsync(int franchiseId, int weekId);
}
