// Models/Franchise.cs
public class Franchise
{
    public List<Team> Teams { get; set; } = new List<Team>();
    public int SeasonScore { get; set; } = 0;
}