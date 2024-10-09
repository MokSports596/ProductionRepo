using MokSportsApp.Models;
using System.ComponentModel.DataAnnotations;

namespace MokSportsApp.DTO
{
    public class SeasonDTO
    {
        [Required]
        [StringLength(4)]
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public SeasonStatus Status { get; set; }
    }
}