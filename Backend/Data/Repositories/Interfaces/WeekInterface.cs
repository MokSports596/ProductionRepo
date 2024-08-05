using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface WeekInterface
    {
        Task<IEnumerable<Week>> GetAllWeeks();
        Task<Week> GetWeekById(int weekId);
        Task AddWeek(Week week);
        Task UpdateWeek(Week week);
        Task DeleteWeek(int weekId);
    }
}
