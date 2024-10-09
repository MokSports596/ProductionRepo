using MokSportsApp.Models;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface ISeasonRepository
    {
        Task AddSeason(Season season);
        Task<Season> GetSeasonByName(string name);
        Task<Season> GetSeason(int id);
        Task<List<Season>> GetAllSeasons();
        Task UpdateSeason(Season season);
        Task<bool> CheckActiveSeason();

        Task<Season> GetActiveSeason();
    }
}