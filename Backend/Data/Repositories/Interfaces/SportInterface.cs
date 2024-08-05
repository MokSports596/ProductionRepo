using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface SportInterface
    {
        Task<IEnumerable<Sport>> GetAllSports();
        Task<Sport> GetSportById(int sportId);
        Task AddSport(Sport sport);
        Task UpdateSport(Sport sport);
        Task DeleteSport(int sportId);
    }
}
