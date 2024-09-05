namespace MokSportsApp.Helpers
{
    public class TeamHelper
    {
        public int? GetTeamIdByAbbreviation(string abbreviation)
        {
            if (TeamMapping.AbbreviationToTeamId.TryGetValue(abbreviation.ToUpper(), out int teamId))
            {
                return teamId;
            }

            return null; // Return null if the abbreviation is not found
        }

        public string GetAbbreviationByTeamId(int teamId)
        {
            if (TeamMapping.TeamIdToAbbreviation.TryGetValue(teamId, out string abbreviation))
            {
                return abbreviation;
            }

            return null; // Return null if the teamId is not found
        }
    }
}
