using Microsoft.EntityFrameworkCore;
using MokSportsApp.Models;

namespace MokSportsApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<FranchiseEarning> FranchiseEarning { get; set; }
        public DbSet<Feed> Feeds { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Notice> Notices { get; set; }
        public DbSet<Trade> Trades { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Standing> Standings { get; set; }
        public DbSet<Sport> Sports { get; set; }
        public DbSet<SportEarning> SportEarnings { get; set; }
        public DbSet<SportPoint> SportPoints { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<Point> Points { get; set; }
        public DbSet<Opponent> Opponents { get; set; }
        public DbSet<Score> Scores { get; set; } // Add this line
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("user_id").ValueGeneratedOnAdd();
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.Status).HasColumnName("status");
            });
        }
    }
}
