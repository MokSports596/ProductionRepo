using MokSportsApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MokSportsApp.Data.Repositories.Interfaces
{
    public interface IDraftPickRepository
    {
        Task<bool> IsTeamDraftedAsync(int draftId, int teamId);
        Task<IEnumerable<DraftPick>> GetDraftPicksByDraftIdAsync(int draftId);
        Task<DraftPick?> GetNextDraftPickForFranchiseAsync(int draftId, int franchiseId);
        Task AddDraftPickAsync(DraftPick draftPick);
        Task<IEnumerable<int>> GetDraftedTeamsAsync(int draftId);
    }

}
