using Microsoft.EntityFrameworkCore;
using NFLGameEngine.Models;

namespace NFLGameEngine
{
    public class AppDbContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Use SQL Server with your RDS connection string
            optionsBuilder.UseSqlServer("Server=db-mok.cpm2gscycga9.us-east-1.rds.amazonaws.com;Database=mok;User ID=admin;Password=#MokSports445;Trusted_Connection=False;MultipleActiveResultSets=true;Encrypt=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
        }
    }
}
