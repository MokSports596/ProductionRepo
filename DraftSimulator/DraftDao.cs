using System;
using System.Collections.Generic;
using Mok.Web.Data.Dto;

namespace Mok.Web.Data.Dao
{
    public class DraftDao : BaseDao
    {
        
        public bool Insert(BaseDto dto)
        {
            Draft draft = dto as Draft;
            if (draft == null)
                throw new ArgumentException("Invalid DTO type");

            
            Console.WriteLine($"Inserting Draft with ID: {draft.DraftId}");
            return true; 
        }

        
        public bool Read(BaseDto dto)
        {
            Draft draft = dto as Draft;
            if (draft == null)
                throw new ArgumentException("Invalid DTO type");

            
            Console.WriteLine($"Reading Draft with ID: {draft.DraftId}");
            return true; 
        }

        
        public bool Update(BaseDto dto)
        {
            Draft draft = dto as Draft;
            if (draft == null)
                throw new ArgumentException("Invalid DTO type");

            
            Console.WriteLine($"Updating Draft with ID: {draft.DraftId}");
            return true; 
        }

       
        public bool Delete(BaseDto dto)
        {
            Draft draft = dto as Draft;
            if (draft == null)
                throw new ArgumentException("Invalid DTO type");

            
            Console.WriteLine($"Deleting Draft with ID: {draft.DraftId}");
            return true;
        }

        
        public Draft GetDraftById(string draftId)
        {
           
            Dictionary<string, string> teams = new Dictionary<string, string>
            {
                { "Patriots", "AFC East" }, { "Bills", "AFC East" }, { "Dolphins", "AFC East" }, { "Jets", "AFC East" },
                { "Ravens", "AFC North" }, { "Bengals", "AFC North" }, { "Browns", "AFC North" }, { "Steelers", "AFC North" },
                { "Colts", "AFC South" }, { "Jaguars", "AFC South" }, { "Titans", "AFC South" }, { "Texans", "AFC South" },
                { "Broncos", "AFC West" }, { "Chiefs", "AFC West" }, { "Raiders", "AFC West" }, { "Chargers", "AFC West" },
                { "Giants", "NFC East" }, { "Cowboys", "NFC East" }, { "Eagles", "NFC East" }, { "Commanders", "NFC East" },
                { "Bears", "NFC North" }, { "Packers", "NFC North" }, { "Vikings", "NFC North" }, { "Lions", "NFC_North" },
                { "Falcons", "NFC South" }, { "Saints", "NFC South" }, { "Bucs", "NFC South" }, { "Panthers", "NFC South" },
                { "Carinals", "NFC West" }, { "Rams", "NFC West" }, { "49ers", "NFC West" }, { "Seahawks", "NFC West" }
            };

            List<Franchise> franchises = new List<Franchise>
            {
                new Franchise("F1", "Jai"),
                new Franchise("F2", "Megha"),
                new Franchise("F3", "Sky"),
                new Franchise("F4", "Dylan"),
                new Franchise("F5", "TJ"),
                new Franchise("F6", "Tilden")
            };

            foreach (var franchise in franchises)
            {
                franchise.DraftedConferences = new HashSet<string>();
                franchise.DraftedTeams = new List<string>();
            }

            return new Draft
            {
                DraftId = draftId,
                LeagueId = "TestLeagueId",
                LeagueName = "Test League",
                Franchises = franchises,
                Teams = teams,
                Status = "In Progress"
            };
        }
    }
}
