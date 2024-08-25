using System;
using System.Collections.Generic;

namespace Mok.Web.Data.Dto
{
    public class Franchise
    {
        // Properties
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string FranchiseName { get; set; } = string.Empty;
        public List<string> DraftedTeams { get; set; } = new List<string>();
        public HashSet<string> DraftedConferences { get; set; } = new HashSet<string>();

        // Constructor that takes two arguments
        public Franchise(string id, string name)
        {
            Id = id;
            Name = name;
            FranchiseName = name; // Assuming FranchiseName is the same as Name
        }

        // Default constructor
        public Franchise()
        {
        }
    }
}
