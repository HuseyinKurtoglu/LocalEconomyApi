using Microsoft.EntityFrameworkCore;
using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;

namespace LocalEconomyApi.Data
{
    public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Business> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Unique Constraint for Email (Örnek)
            modelBuilder.Entity<User>()
                        .HasIndex(u => u.Email)
                        .IsUnique();

            // IsDeleted için Global Query Filter
            modelBuilder.Entity<Business>().HasQueryFilter(b => !b.IsDeleted);
            modelBuilder.Entity<Category>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Campaign>().HasQueryFilter(c => !c.IsDeleted);
            modelBuilder.Entity<Log>().HasQueryFilter(l => !l.IsDeleted);
            modelBuilder.Entity<Favorite>().HasQueryFilter(f => !f.IsDeleted);
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        }
    }
}
