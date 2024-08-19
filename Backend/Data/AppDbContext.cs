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
        public DbSet<UserStats> UserStats { get; set; }
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

            modelBuilder.Entity<UserStats>(entity =>
            {
                entity.ToTable("UserStats");
                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId).HasColumnName("UserId");
                entity.Property(e => e.LeagueId).HasColumnName("LeagueId");
                entity.Property(e => e.WeekId).HasColumnName("WeekId");  // New column for Week

                entity.Property(e => e.SeasonPoints).HasColumnName("SeasonPoints");
                entity.Property(e => e.WeekPoints).HasColumnName("WeekPoints");
                entity.Property(e => e.LoksUsed).HasColumnName("LoksUsed");
                entity.Property(e => e.Skins).HasColumnName("Skins");

                entity.HasOne(us => us.User)
                    .WithMany(u => u.UserStats)
                    .HasForeignKey(us => us.UserId);

                entity.HasOne(us => us.League)
                    .WithMany(l => l.UserStats)
                    .HasForeignKey(us => us.LeagueId);

                entity.HasOne(us => us.Week)  // Define the relationship between UserStats and Week
                    .WithMany(w => w.UserStats)
                    .HasForeignKey(us => us.WeekId);
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

            // Configure UserLeague
            modelBuilder.Entity<UserLeague>(entity =>
            {
                entity.ToTable("UserLeagues");

                entity.HasKey(ul => ul.Id); // Primary key on the Id column

                entity.Property(ul => ul.UserId).HasColumnName("user_id");
                entity.Property(ul => ul.LeagueId).HasColumnName("league_id");

                entity.HasOne(ul => ul.User)
                      .WithMany(u => u.UserLeagues)
                      .HasForeignKey(ul => ul.UserId);

                entity.HasOne(ul => ul.League)
                      .WithMany(l => l.UserLeagues)
                      .HasForeignKey(ul => ul.LeagueId);
            });
            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("Leagues");

                entity.HasKey(e => e.LeagueId);
                entity.Property(e => e.LeagueId).HasColumnName("LeagueId");
                entity.Property(e => e.LeagueName).HasColumnName("LeagueName");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.Pin).HasColumnName("pin");

            });
        }
    }
}
