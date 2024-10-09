using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.Models
{
    public enum SeasonStatus
    {
        Upcoming,
        Active,
        InActive,
    }

    [Table("Seasons")]
    public class Season
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }

        [Required]
        public SeasonStatus Status { get; set; }
    }
}