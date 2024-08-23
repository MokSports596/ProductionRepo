using MokSportsApp.Models;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IDraftRepository
    {
        Task<Draft?> GetDraftByIdAsync(int draftId);
        Task<Draft?> GetActiveDraftByLeagueIdAsync(int leagueId);
        Task AddDraftAsync(Draft draft);
        Task UpdateDraftAsync(Draft draft);
    }

}
