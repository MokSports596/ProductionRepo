using MokSportsApp.Models;
using System.Threading.Tasks;
using MokSportsApp.DTOs;


namespace MokSportsApp.Services.Interfaces
{
    public interface IDraftService
    {
        Task<Draft?> StartDraftAsync(int leagueId);
        Task<Draft?> GetDraftByIdAsync(int draftId);
        Task<bool> MakeDraftPickAsync(int draftId, int franchiseId, string teamAbbreviation);
        Task<IEnumerable<int>> GetAvailableTeamsAsync(int draftId);
        Task<List<int>> GetDraftOrderAsync(int draftId);
        Task<DraftStateDto> GetDraftStateAsync(int draftId);
        Task<int?> GetDraftIdByUserIdAndLeagueIdAsync(int userId, int leagueId);
        Task<List<string>> GetDraftOrderForRoundAsync(int draftId);
    }

}
