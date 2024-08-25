using System;


namespace Mok.Web.Data.Dto
{
	public class Team
	{
		public string? TeamId { get; set; }
		public string? TeamCode { get; set; }
		public string? TeamName { get; set; }
		public string? FutureOdds { get; set; }
		public string? FranchiseName { get; set; }
		public string? ConferenceName { get; set; }
		public int? ConferenceRank { get; set; }
		public string? DivisionName { get; set; }
		public int? PowerRank { get; set; }
		public int? Wins { get; set; }
		public int? Loses { get; set; }
		public int? Ties { get; set; }
	}
}