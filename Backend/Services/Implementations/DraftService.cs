using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MokSportsApp.DTOs;
using System.Threading.Tasks;

namespace MokSportsApp.Services.Implementations
{
    public class DraftService : IDraftService
    {
        private readonly IDraftRepository _draftRepository;
        private readonly IFranchiseRepository _franchiseRepository;
        private readonly IDraftPickRepository _draftPickRepository;
        private readonly ITeamRepository _teamRepository;

        public DraftService(IDraftRepository draftRepository, IFranchiseRepository franchiseRepository, IDraftPickRepository draftPickRepository, ITeamRepository teamRepository)
        {
            _draftRepository = draftRepository;
            _franchiseRepository = franchiseRepository;
            _draftPickRepository = draftPickRepository;
            _teamRepository = teamRepository;
        }

        public async Task<Draft?> StartDraftAsync(int leagueId)
        {
            // Check for an existing active draft
            Console.WriteLine($"Attempting to start draft for LeagueId: {leagueId}");

            var activeDraft = await _draftRepository.GetActiveDraftByLeagueIdAsync(leagueId);
            if (activeDraft != null)
            {
                Console.WriteLine("Draft is already in service.");
                return null; // A draft is already active for this league
            }

            // Fetch franchises in the league
            var franchises = await _franchiseRepository.GetFranchisesByLeagueIdAsync(leagueId); // Corrected method name
            if (franchises == null || !franchises.Any())
            {
                return null; // No franchises available for drafting
            }

            // Randomize the draft order
            var franchiseIds = franchises.Select(f => f.FranchiseId).OrderBy(_ => Guid.NewGuid()).ToList();
            var draftOrder = string.Join(",", franchiseIds);

            // Create a new draft
            var draft = new Draft
            {
                LeagueId = leagueId,
                DraftOrder = draftOrder,
                CurrentRound = 1,
                CurrentPickIndex = 0,
                IsCompleted = false // Removed any mention of status, just using IsCompleted
            };

            await _draftRepository.AddDraftAsync(draft);
            return draft;
        }

        public async Task<Draft?> GetDraftByIdAsync(int draftId)
        {
            return await _draftRepository.GetDraftByIdAsync(draftId);
        }

        public async Task<bool> MakeDraftPickAsync(int draftId, int franchiseId, int teamId)
        {
            // Check if the draft exists and is active
            var draft = await _draftRepository.GetDraftByIdAsync(draftId);
            if (draft == null)
            {
                Console.WriteLine("Error: Draft not found.");
                return false;
            }
            if (draft.IsCompleted)
            {
                Console.WriteLine("Error: Draft is already completed.");
                return false;
            }

            // Check if the franchise exists
            var franchise = await _franchiseRepository.GetFranchiseByIdAsync(franchiseId);
            if (franchise == null)
            {
                Console.WriteLine("Error: Franchise not found.");
                return false;
            }

            // Ensure the franchise belongs to the league associated with the draft
            if (franchise.LeagueId != draft.LeagueId)
            {
                Console.WriteLine("Error: Franchise does not belong to the league associated with this draft.");
                return false;
            }

            // Check if the team is already drafted
            var isTeamDrafted = await _draftPickRepository.IsTeamDraftedAsync(draftId, teamId);
            if (isTeamDrafted)
            {
                Console.WriteLine("Error: Team is already drafted.");
                return false; // Team already drafted
            }

            // Find the next available team slot in the franchise
            if (franchise.Team1Id == null)
            {
                franchise.Team1Id = teamId;
            }
            else if (franchise.Team2Id == null)
            {
                franchise.Team2Id = teamId;
            }
            else if (franchise.Team3Id == null)
            {
                franchise.Team3Id = teamId;
            }
            else if (franchise.Team4Id == null)
            {
                franchise.Team4Id = teamId;
            }
            else if (franchise.Team5Id == null)
            {
                franchise.Team5Id = teamId;
            }
            else
            {
                Console.WriteLine("Error: No available slots in the franchise.");
                return false; // No available slots in the franchise
            }
 
            // Create the draft pick
            var draftPick = new DraftPick
            {
                DraftId = draftId,
                FranchiseId = franchiseId,
                TeamId = teamId,
                PickOrder = draft.CurrentPickIndex + 1,
                PickTime = DateTime.UtcNow
            };

            // Save the draft pick and update the franchise
            await _draftPickRepository.AddDraftPickAsync(draftPick);
            await _franchiseRepository.UpdateFranchiseAsync(franchise);

            // Update the draft status (advance to the next pick or round)
            draft.CurrentPickIndex++;
            if (draft.CurrentPickIndex >= draft.DraftOrder.Split(',').Length)
            {
                draft.CurrentPickIndex = 0;
                draft.CurrentRound++;
            }

            if (draft.CurrentRound > 5) // Assuming 5 rounds
            {
                draft.IsCompleted = true;
            }

            // Save the updated draft
            await _draftRepository.UpdateDraftAsync(draft);

            Console.WriteLine("Draft pick successful: Franchise {0} picked Team {1}", franchiseId, teamId);
            return true;
        }


        public async Task<IEnumerable<int>> GetAvailableTeamsAsync(int draftId)
        {
            // Fetch the draft by ID
            var draft = await _draftRepository.GetDraftByIdAsync(draftId);
            if (draft == null)
            {
                // Return an empty collection if the draft doesn't exist
                return Enumerable.Empty<int>();
            }

            // Fetch teams that have been drafted
            var draftedTeams = await _draftPickRepository.GetDraftedTeamsAsync(draftId);
            if (draftedTeams == null)
            {
                // If no teams have been drafted yet, use an empty collection
                draftedTeams = Enumerable.Empty<int>();
            }

            // Fetch all teams and find those that haven't been drafted
            var allTeams = await _teamRepository.GetAllTeamsAsync();
            if (allTeams == null)
            {
                // If there are no teams in the database, return an empty collection
                return Enumerable.Empty<int>();
            }

            // Find the teams that haven't been drafted yet
            var availableTeams = allTeams.Select(t => t.TeamId).Except(draftedTeams);

            return availableTeams;
        }

        public async Task<List<int>> GetDraftOrderAsync(int draftId)
        {
            var draft = await _draftRepository.GetDraftByIdAsync(draftId);
            if (draft == null)
            {
                return null;
            }

            var draftOrder = draft.DraftOrder.Split(',').Select(int.Parse).ToList();
            return draftOrder;
        }

        public async Task<DraftStateDto> GetDraftStateAsync(int draftId)
        {
            var draft = await _draftRepository.GetDraftByIdAsync(draftId);
            if (draft == null)
            {
                return null;
            }

            var draftOrder = draft.DraftOrder.Split(',').Select(int.Parse).ToList();
            int currentFranchiseId = draftOrder[draft.CurrentPickIndex];

            return new DraftStateDto
            {
                CurrentRound = draft.CurrentRound,
                CurrentPickIndex = draft.CurrentPickIndex,
                CurrentFranchiseId = currentFranchiseId,
                IsCompleted = draft.IsCompleted
            };
        }

        public async Task<int?> GetDraftIdByUserIdAndLeagueIdAsync(int userId, int leagueId)
        {
            var draft = await _draftRepository.GetDraftIdByUserIdAndLeagueIdAsync(userId, leagueId);
            return draft?.DraftId;
        }


    }
}
