using System.ComponentModel.DataAnnotations.Schema;

namespace MokSportsApp.Models
{
    [Table("UserDevices")]
    public class UserDevice
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public User UserFk { get; set; }
        public string Token { get; set; }
        public DateTime CreationTime { get; set; }
    }
}
