using Microsoft.EntityFrameworkCore;
using MokSportsApp.Models;
using System.Diagnostics;

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
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<UserLeague> UserLeagues { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<DraftPick> DraftPicks { get; set; }
        public DbSet<Season> Seasons { get; set; }
        public DbSet<TradeTeam> Trades { get; set; }
        public DbSet<UserDevice> UserDevices { get; set; }
        public DbSet<Week> Weeks { get; set; }
        public DbSet<Skin> Skins { get; set; } 
        public DbSet<FranchiseLocksLoads> FranchiseLocksLoads { get; set; } // Added FranchiseLocksLoads DbSet
        public DbSet<Score> Scores { get; set; } // Added Scores DbSet

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

            modelBuilder.Entity<Franchise>(entity =>
            {
                entity.ToTable("Franchises");

                entity.HasKey(e => e.FranchiseId);

                entity.Property(e => e.FranchiseName).HasColumnName("FranchiseName");

                entity.HasOne(f => f.User)
                      .WithMany(u => u.Franchises)
                      .HasForeignKey(f => f.UserId);

                entity.HasOne(f => f.League)
                      .WithMany(l => l.Franchises)
                      .HasForeignKey(f => f.LeagueId);

                entity.HasOne(f => f.Team1)
                      .WithMany()
                      .HasForeignKey(f => f.Team1Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Team2)
                      .WithMany()
                      .HasForeignKey(f => f.Team2Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Team3)
                      .WithMany()
                      .HasForeignKey(f => f.Team3Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Team4)
                      .WithMany()
                      .HasForeignKey(f => f.Team4Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(f => f.Team5)
                      .WithMany()
                      .HasForeignKey(f => f.Team5Id)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(f => f.WinningSkins) // Added navigation property
                      .WithOne(s => s.Winner)
                      .HasForeignKey(s => s.WinnerId);

                entity.Property(f => f.TotalSkinsWon) // Added scalar property
                      .HasDefaultValue(0);
            });

            modelBuilder.Entity<League>(entity =>
            {
                entity.ToTable("Leagues");

                entity.HasKey(e => e.LeagueId);
                entity.Property(e => e.LeagueId).HasColumnName("LeagueId");
                entity.Property(e => e.CreatedAt).HasColumnName("created_at");
                entity.Property(e => e.CreatedBy).HasColumnName("created_by");
                entity.Property(e => e.Pin).HasColumnName("pin");

                entity.HasMany(l => l.Skins) // Added navigation property
                      .WithOne(s => s.League)
                      .HasForeignKey(s => s.LeagueId);
            });

            modelBuilder.Entity<Skin>(entity =>
            {
                entity.ToTable("Skins");

                entity.HasKey(s => s.SkinId);

                entity.Property(s => s.LeagueId).HasColumnName("LeagueId");
                entity.Property(s => s.Week).HasColumnName("Week");
                entity.Property(s => s.Score).HasColumnName("Score");
                entity.Property(s => s.WinnerId).HasColumnName("WinnerId");
                entity.Property(s => s.RolledOver).HasColumnName("RolledOver");
                entity.Property(s => s.CreatedAt).HasColumnName("CreatedAt");
            });
        }
    }
}
