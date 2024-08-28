namespace MokSportsApp.Helpers
{
    public static class TeamMapping
    {
        public static readonly Dictionary<int, string> TeamIdToAbbreviation = new Dictionary<int, string>
        {
            { 65, "ARI" },
            { 66, "ATL" },
            { 67, "BAL" },
            { 68, "BUF" },
            { 69, "CAR" },
            { 70, "CHI" },
            { 71, "CIN" },
            { 72, "CLE" },
            { 73, "DAL" },
            { 74, "DEN" },
            { 75, "DET" },
            { 76, "GB" },
            { 77, "HOU" },
            { 78, "IND" },
            { 79, "JAX" },
            { 80, "KC" },
            { 81, "LV" },
            { 82, "LAC" },
            { 83, "LAR" },
            { 84, "MIA" },
            { 85, "MIN" },
            { 86, "NE" },
            { 87, "NO" },
            { 88, "NYG" },
            { 89, "NYJ" },
            { 90, "PHI" },
            { 91, "PIT" },
            { 92, "SF" },
            { 93, "SEA" },
            { 94, "TB" },
            { 95, "TEN" },
            { 96, "WAS" }
        };

        public static readonly Dictionary<string, int> AbbreviationToTeamId = new Dictionary<string, int>
        {
            { "ARI", 65 },
            { "ATL", 66 },
            { "BAL", 67 },
            { "BUF", 68 },
            { "CAR", 69 },
            { "CHI", 70 },
            { "CIN", 71 },
            { "CLE", 72 },
            { "DAL", 73 },
            { "DEN", 74 },
            { "DET", 75 },
            { "GB", 76 },
            { "HOU", 77 },
            { "IND", 78 },
            { "JAX", 79 },
            { "KC", 80 },
            { "LV", 81 },
            { "LAC", 82 },
            { "LAR", 83 },
            { "MIA", 84 },
            { "MIN", 85 },
            { "NE", 86 },
            { "NO", 87 },
            { "NYG", 88 },
            { "NYJ", 89 },
            { "PHI", 90 },
            { "PIT", 91 },
            { "SF", 92 },
            { "SEA", 93 },
            { "TB", 94 },
            { "TEN", 95 },
            { "WAS", 96 }
        };
    }
}
