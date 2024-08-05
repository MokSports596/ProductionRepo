namespace MokSportsApp.Models
{
    public class Role
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; } = null!; // Non-nullable, must be initialized
        public string RoleDescription { get; set; } = null!; // Non-nullable, must be initialized
    }
}
