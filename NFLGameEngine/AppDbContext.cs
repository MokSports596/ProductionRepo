using Microsoft.EntityFrameworkCore;
using NFLGameEngine.Models;

namespace NFLGameEngine
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        public DbSet<Franchise> Franchises { get; set; }
        public DbSet<UserStats> UserStats { get; set; }
        public DbSet<FranchiseLocksLoads> FranchiseLocksLoads { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQL Server with your RDS connection string
            optionsBuilder.UseSqlServer("Server=db-mok.cpm2gscycga9.us-east-1.rds.amazonaws.com;Database=mok;User ID=admin;Password=#MokSports445;Trusted_Connection=False;MultipleActiveResultSets=true;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Game entity
            modelBuilder.Entity<Game>(entity =>
            {
                entity.ToTable("Games");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.GameId).IsRequired().HasMaxLength(50);
                entity.Property(e => e.Season).IsRequired();
                entity.Property(e => e.SeasonType).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AwayTeam).IsRequired().HasMaxLength(50);
                entity.Property(e => e.HomeTeam).IsRequired().HasMaxLength(50);
                entity.Property(e => e.GameDate).IsRequired();
                entity.Property(e => e.GameTime).IsRequired().HasMaxLength(10);
                entity.Property(e => e.GameStatus).IsRequired().HasMaxLength(50);
                entity.Property(e => e.AwayPoints);
                entity.Property(e => e.HomePoints);
                entity.Property(e => e.Quarter1);
                entity.Property(e => e.Quarter2);
                entity.Property(e => e.Quarter3);
                entity.Property(e => e.Quarter4);
                entity.Property(e => e.TotalPoints);
                entity.Property(e => e.SportsBookOdds).HasMaxLength(500);
                entity.Property(e => e.ESPNLink).HasMaxLength(500);
                entity.Property(e => e.Week).IsRequired();
            });

            // Configure the Franchise entity
            modelBuilder.Entity<Franchise>(entity =>
            {
                entity.ToTable("Franchises");

                entity.HasKey(e => e.FranchiseId);

                entity.Property(e => e.FranchiseName).IsRequired().HasMaxLength(100);
                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.LeagueId).IsRequired();

                // Optional Team slots
                entity.Property(e => e.Team1Id);
                entity.Property(e => e.Team2Id);
                entity.Property(e => e.Team3Id);
                entity.Property(e => e.Team4Id);
                entity.Property(e => e.Team5Id);
            });

            // Configure the UserStats entity
            modelBuilder.Entity<UserStats>(entity =>
            {
                entity.ToTable("UserStats");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.UserId).IsRequired();
                entity.Property(e => e.LeagueId).IsRequired();
                entity.Property(e => e.FranchiseId).IsRequired();
                entity.Property(e => e.WeekId).IsRequired();
                entity.Property(e => e.SeasonPoints).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.WeekPoints).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.LoksUsed).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.LoadsUsed).IsRequired().HasDefaultValue(0);
                entity.Property(e => e.Skins).IsRequired().HasDefaultValue(0);
            });

            // Configure the FranchiseLocksLoads entity
            modelBuilder.Entity<FranchiseLocksLoads>(entity =>
            {
                entity.ToTable("FranchiseLocksLoads");

                entity.HasKey(e => e.Id);

                entity.Property(e => e.FranchiseId).IsRequired();
                entity.Property(e => e.WeekId).IsRequired();
                entity.Property(e => e.LOKTeamId);
                entity.Property(e => e.LOADTeamId);

                // Relationships
                entity.HasOne(e => e.Franchise)
                    .WithMany()
                    .HasForeignKey(e => e.FranchiseId);
            });
        }
    }
}
