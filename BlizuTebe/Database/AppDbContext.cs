using BlizuTebe.Models;
using Microsoft.EntityFrameworkCore;

namespace BlizuTebe.Database
{
    public class AppDbContext : DbContext
    {
        protected readonly IConfiguration Configuration;

        public AppDbContext(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseNpgsql(
            Configuration.GetConnectionString("WebApiDatabase"),
            x => x.UseNetTopologySuite() 
        );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LocalCommunity>(entity =>
            {
                entity.Property(e => e.Boundary).HasColumnType("geography");
                entity.Property(e => e.CenterPoint).HasColumnType("geography(Point)");
            });
            modelBuilder.Entity<Announcement>(entity =>
            {
                entity.Property(e => e.Picture).IsRequired(false); 
            });
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Announcement> Announcements { get; set; }

        public DbSet<LocalCommunity> LocalCommunities { get; set; }
    }
}
