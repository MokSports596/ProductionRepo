using MokSportsApp.Models;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Interfaces
{
    public interface IDraftService
    {
        Task<Draft?> StartDraftAsync(int leagueId);
        Task<Draft?> GetDraftByIdAsync(int draftId);
        Task<bool> MakeDraftPickAsync(int draftId, int franchiseId, int teamId);
        Task<IEnumerable<int>> GetAvailableTeamsAsync(int draftId);
        Task<List<int>> GetDraftOrderAsync(int draftId);
    }

}
