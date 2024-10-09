using MokSportsApp.Models;

namespace MokSportsApp.Services.Interfaces
{
    public interface ISeasonService
    {
        void ValidateSeasonName(string name);
        Task AddSeason(Season season);
        Task<Season> GetSeason(int id);
        Task<List<Season>> GetAllSeasons();
        Task UpdateSeason(Season season);
        Task<Season> GetSeasonByName(string name);
        Task<bool> CheckActiveSeason();
        void ValidateSeasonStatus(int value);
        Task<Season> GetActiveSeason();
    }
}