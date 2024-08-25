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
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<UserLeague> UserLeagues { get; set; }
        public DbSet<League> Leagues { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Draft> Drafts { get; set; }
        public DbSet<DraftPick> DraftPicks { get; set; }


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
            });




            modelBuilder.Entity<Team>().ToTable("Teams");

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


            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Games"); // Ensure this maps to the correct table in your database
                entity.HasKey(e => e.Id);

                entity.Property(e => e.GameId)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.Season)
                      .IsRequired();

                entity.Property(e => e.SeasonType)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.AwayTeam)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.HomeTeam)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.GameDate)
                      .IsRequired();

                entity.Property(e => e.GameTime)
                      .IsRequired()
                      .HasMaxLength(10);

                entity.Property(e => e.GameStatus)
                      .IsRequired()
                      .HasMaxLength(50);

                entity.Property(e => e.AwayPoints)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.HomePoints)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.Quarter1)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.Quarter2)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.Quarter3)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.Quarter4)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.TotalPoints)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.SportsBookOdds)
                      .HasMaxLength(255)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.ESPNLink)
                      .HasMaxLength(255)
                      .IsRequired(false); // Nullable in the database

                entity.Property(e => e.Week).IsRequired(); 
            });

            modelBuilder.Entity<Draft>(entity =>
            {
                  entity.ToTable("Drafts");
                  entity.HasKey(d => d.DraftId);

                  entity.HasOne(d => d.League)
                  .WithMany(l => l.Drafts)
                  .HasForeignKey(d => d.LeagueId);

            });

            modelBuilder.Entity<DraftPick>(entity =>
            {
                  entity.ToTable("DraftPicks");
                  entity.HasKey(dp => dp.DraftPickId);

                  entity.HasOne(dp => dp.Draft)
                  .WithMany(d => d.DraftPicks)
                  .HasForeignKey(dp => dp.DraftId);

                  entity.HasOne(dp => dp.Franchise)
                  .WithMany(f => f.DraftPicks)
                  .HasForeignKey(dp => dp.FranchiseId);

                  entity.HasOne(dp => dp.Team)
                  .WithMany(t => t.DraftPicks)
                  .HasForeignKey(dp => dp.TeamId);
            });
        }
    }
}
