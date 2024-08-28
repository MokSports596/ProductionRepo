using MokSportsApp.Models;
using MokSportsApp.Data.Repositories.Interfaces;
using MokSportsApp.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using MokSportsApp.DTOs;
using System.Threading.Tasks;
using MokSportsApp.Helpers;

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
        
        public async Task<bool> MakeDraftPickAsync(int draftId, int franchiseId, string teamAbbreviation)
        {
            // Convert the team abbreviation to team ID using the helper
            var teamHelper = new TeamHelper();
            int? teamId = teamHelper.GetTeamIdByAbbreviation(teamAbbreviation);

            if (teamId == null)
            {
                Console.WriteLine("Error: Team not found.");
                return false; // Team not found
            }

            // Proceed with the rest of the draft logic using teamId.Value
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

            var franchise = await _franchiseRepository.GetFranchiseByIdAsync(franchiseId);
            if (franchise == null)
            {
                Console.WriteLine("Error: Franchise not found.");
                return false;
            }

            if (franchise.LeagueId != draft.LeagueId)
            {
                Console.WriteLine("Error: Franchise does not belong to the league associated with this draft.");
                return false;
            }

            var isTeamDrafted = await _draftPickRepository.IsTeamDraftedAsync(draftId, teamId.Value);
            if (isTeamDrafted)
            {
                Console.WriteLine("Error: Team is already drafted.");
                return false; // Team already drafted
            }

            // Assign the team to the next available slot in the franchise
            if (franchise.Team1Id == null)
            {
                franchise.Team1Id = teamId.Value;
            }
            else if (franchise.Team2Id == null)
            {
                franchise.Team2Id = teamId.Value;
            }
            else if (franchise.Team3Id == null)
            {
                franchise.Team3Id = teamId.Value;
            }
            else if (franchise.Team4Id == null)
            {
                franchise.Team4Id = teamId.Value;
            }
            else if (franchise.Team5Id == null)
            {
                franchise.Team5Id = teamId.Value;
            }
            else
            {
                Console.WriteLine("Error: No available slots in the franchise.");
                return false; // No available slots in the franchise
            }

            var draftPick = new DraftPick
            {
                DraftId = draftId,
                FranchiseId = franchiseId,
                TeamId = teamId.Value,
                PickOrder = draft.CurrentPickIndex + 1,
                PickTime = DateTime.UtcNow
            };

            await _draftPickRepository.AddDraftPickAsync(draftPick);
            await _franchiseRepository.UpdateFranchiseAsync(franchise);

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

            await _draftRepository.UpdateDraftAsync(draft);

            Console.WriteLine("Draft pick successful: Franchise {0} picked Team {1}", franchiseId, teamId.Value);
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
                return null; // Draft not found
            }

            var draftOrderIds = draft.DraftOrder.Split(',').Select(int.Parse).ToList();
            var draftOrderNames = new List<string>();

            foreach (var franchiseId in draftOrderIds)
            {
                var franchise = await _franchiseRepository.GetFranchiseByIdAsync(franchiseId);
                draftOrderNames.Add(franchise?.FranchiseName); // Use FranchiseName instead of Name
            }

            var currentFranchiseId = draftOrderIds[draft.CurrentPickIndex];
            var currentFranchise = await _franchiseRepository.GetFranchiseByIdAsync(currentFranchiseId);

            return new DraftStateDto
            {
                CurrentRound = draft.CurrentRound,
                CurrentPickIndex = draft.CurrentPickIndex,
                CurrentFranchiseName = currentFranchise?.FranchiseName, // Use FranchiseName here
                IsCompleted = draft.IsCompleted,
                DraftOrder = draftOrderNames, // Return the full draft order with franchise names
            };
        }


        public async Task<int?> GetDraftIdByUserIdAndLeagueIdAsync(int userId, int leagueId)
        {
            var draft = await _draftRepository.GetDraftIdByUserIdAndLeagueIdAsync(userId, leagueId);
            return draft?.DraftId;
        }


        public async Task<List<string>> GetDraftOrderForRoundAsync(int draftId)
        {
            var draft = await _draftRepository.GetDraftByIdAsync(draftId);
            if (draft == null)
            {
                return null; // Draft not found
            }

            var draftOrderIds = draft.DraftOrder.Split(',').Select(int.Parse).ToList();
            var draftOrderNames = new List<string>();

            // Determine the order for the current round
            var roundOrder = (draft.CurrentRound % 2 == 1) 
                            ? draftOrderIds 
                            : draftOrderIds.AsEnumerable().Reverse().ToList();

            // Fetch franchise names for the determined order
            foreach (var franchiseId in roundOrder)
            {
                var franchise = await _franchiseRepository.GetFranchiseByIdAsync(franchiseId);
                draftOrderNames.Add(franchise?.FranchiseName); // Use FranchiseName instead of Name
            }

            return draftOrderNames;
        }


    }
}
