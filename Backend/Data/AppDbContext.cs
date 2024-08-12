using Microsoft.EntityFrameworkCore;
using MokSportsApp.Models;

namespace MokSportsApp.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<FranchiseTeam> FranchiseTeams { get; set; }
        public DbSet<UserStats> UserStats { get; set; }  // New UserStats DbSet
        public DbSet<UserLeague> UserLeagues { get; set; }
        public DbSet<League> Leagues { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.HasKey(e => e.UserId);
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.FirstName).HasColumnName("first_name");
                entity.Property(e => e.LastName).HasColumnName("last_name");
                entity.Property(e => e.Email).HasColumnName("email");
                entity.Property(e => e.PhoneNumber).HasColumnName("phone_number");
                entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
                entity.Property(e => e.PasswordSalt).HasColumnName("password_salt");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.Status).HasColumnName("status");
            });

            modelBuilder.Entity<Franchise>().ToTable("Franchises");
            modelBuilder.Entity<Team>().ToTable("Teams");
            modelBuilder.Entity<FranchiseTeam>().ToTable("FranchiseTeams");

            // Removed Stat configuration and added UserStats configuration
            modelBuilder.Entity<UserStats>(entity =>
            {
                entity.ToTable("UserStats");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).HasColumnName("id");
                entity.Property(e => e.UserId).HasColumnName("user_id");
                entity.Property(e => e.SeasonPoints).HasColumnName("season_points");
                entity.Property(e => e.WeekPoints).HasColumnName("week_points");
                entity.Property(e => e.LoksUsed).HasColumnName("loks_used");
                entity.Property(e => e.Skins).HasColumnName("skins");

                // Define the relationship between UserStats and User
                entity.HasOne(us => us.User)
                      .WithMany(u => u.UserStats)
                      .HasForeignKey(us => us.UserId);
            });

            modelBuilder.Entity<FranchiseTeam>()
                .HasKey(ft => new { ft.FranchiseId, ft.TeamId });
            modelBuilder.Entity<FranchiseTeam>()
                .HasOne(ft => ft.Franchise)
                .WithMany(f => f.FranchiseTeams)
                .HasForeignKey(ft => ft.FranchiseId);
            modelBuilder.Entity<FranchiseTeam>()
                .HasOne(ft => ft.Team)
                .WithMany(t => t.FranchiseTeams)
                .HasForeignKey(ft => ft.TeamId);
        }
    }
}
