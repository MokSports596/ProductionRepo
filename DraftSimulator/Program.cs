using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mok.Web.Data.Dao;  // Namespace for DraftDao
using Mok.Web.Data.Dto;  // Namespace for Draft and Franchise

namespace DraftSimulator
{
    class Program
    {
        static Timer autoDraftTimer;
        static bool timerStarted = false;

        static void Main(string[] args)
        {
            SimulateDraft();
        }

        static void SimulateDraft()
        {
            DraftDao dao = new DraftDao();
            Draft draft = dao.GetDraftById("test-draft-id");

            int totalTeams = 5;  // Number of teams each franchise will draft
            int totalFranchises = draft.Franchises.Count;

            for (int round = 0; round < totalTeams; round++)
            {
                Console.WriteLine($"Round {round + 1}:\n");

                for (int i = 0; i < totalFranchises; i++)
                {
                    int franchiseIndex = (round % 2 == 0) ? i : totalFranchises - 1 - i;
                    Franchise franchise = draft.Franchises[franchiseIndex];

                    if (!timerStarted)
                    {
                        StartAutoDraftTimer(draft, franchise, round, totalFranchises);
                        timerStarted = true;
                    }

                    string nextTeam = GetUserSelectedTeam(franchise, draft.Teams, round == totalTeams - 1);

                    if (!string.IsNullOrEmpty(nextTeam))
                    {
                        string conference = draft.Teams[nextTeam];
                        franchise.DraftedTeams.Add(nextTeam);
                        franchise.DraftedConferences.Add(conference);
                        draft.Teams.Remove(nextTeam);  // Remove the drafted team from the pool

                        Console.WriteLine($"{franchise.Name} picks {nextTeam} from {conference} conference");

                        if (autoDraftTimer != null)
                        {
                            autoDraftTimer.Dispose();  // Dispose of the timer if the user picks before the timer expires
                        }
                    }
                }

                Console.WriteLine();  // Add a blank line between rounds
            }

            // Display the final draft results
            DisplayDraftResults(draft);
        }

        static void StartAutoDraftTimer(Draft draft, Franchise franchise, int round, int totalFranchises)
        {
            autoDraftTimer = new Timer(AutoDraftRemainingTeams, new TimerState { Draft = draft, Round = round, TotalFranchises = totalFranchises }, TimeSpan.FromSeconds(86400), Timeout.InfiniteTimeSpan);
        }

        static void AutoDraftRemainingTeams(object state)
        {
            var timerState = (TimerState)state;
            var draft = timerState.Draft;
            int round = timerState.Round;
            int totalFranchises = timerState.TotalFranchises;

            Console.WriteLine("\n24 hours have passed. Automatically drafting remaining teams...\n");

            // Auto draft remaining teams
            for (; round < 5; round++)
            {
                for (int i = 0; i < totalFranchises; i++)
                {
                    int franchiseIndex = (round % 2 == 0) ? i : totalFranchises - 1 - i;
                    Franchise franchise = draft.Franchises[franchiseIndex];

                    string nextTeam = FindNextAvailableTeam(franchise, draft.Teams, round == 4);

                    if (!string.IsNullOrEmpty(nextTeam))
                    {
                        string conference = draft.Teams[nextTeam];
                        franchise.DraftedTeams.Add(nextTeam);
                        franchise.DraftedConferences.Add(conference);
                        draft.Teams.Remove(nextTeam);

                        Console.WriteLine($"{franchise.Name} auto-picks {nextTeam} from {conference} conference");
                    }
                }

                Console.WriteLine();  // Add a blank line between rounds
            }

            // Display the final draft results
            DisplayDraftResults(draft);
        }

        static string FindNextAvailableTeam(Franchise franchise, Dictionary<string, string> teams, bool isFinalRound)
        {
            foreach (var team in teams)
            {
                if (isFinalRound || !franchise.DraftedConferences.Contains(team.Value))
                {
                    return team.Key;
                }
            }
            return null;
        }

        static string GetUserSelectedTeam(Franchise franchise, Dictionary<string, string> teams, bool isFinalRound)
        {
            List<string> availableTeams = teams.Keys.ToList();

            while (true)
            {
                Console.WriteLine($"Franchise {franchise.Name}, please select a team to draft:");
                for (int i = 0; i < availableTeams.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableTeams[i]} ({teams[availableTeams[i]]} conference)");
                }

                int selectedOption;
                string userInput = Console.ReadLine();

                if (int.TryParse(userInput, out selectedOption) && selectedOption > 0 && selectedOption <= availableTeams.Count)
                {
                    string selectedTeam = availableTeams[selectedOption - 1];
                    string selectedConference = teams[selectedTeam];

                    if (isFinalRound || !franchise.DraftedConferences.Contains(selectedConference))
                    {
                        return selectedTeam;
                    }
                    else
                    {
                        Console.WriteLine($"You cannot draft another team from the {selectedConference} conference. Please choose a different team.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid selection. Please try again.");
                }
            }
        }

        static void DisplayDraftResults(Draft draft)
        {
            Console.WriteLine("Final Draft Results:\n");

            foreach (var franchise in draft.Franchises)
            {
                Console.WriteLine($"Franchise: {franchise.Name}");
                foreach (var team in franchise.DraftedTeams)
                {
                    Console.WriteLine($"  - {team} ({team}) conference");
                }
                Console.WriteLine();  // Add a blank line between franchises
            }
        }

        class TimerState
        {
            public Draft Draft { get; set; }
            public int Round { get; set; }
            public int TotalFranchises { get; set; }
        }
    }
}
