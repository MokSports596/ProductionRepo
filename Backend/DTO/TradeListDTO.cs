using MokSportsApp.Models;

namespace MokSportsApp.DTO
{
    public class TradeListDTO
    {
        public int Id { get; set; }
        public int OwnTeamId { get; set; }
        public string OwnTeamAbbrr { get; set; }

        public int RequestedTeamId { get; set; }
        public string RequestedTeamAbbrr { get; set; }

        public DateTime CreationTime { get; set; }
        public TradeStatus Status { get; set; }
    }
}
