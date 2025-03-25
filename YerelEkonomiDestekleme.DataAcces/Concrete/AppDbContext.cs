using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<BusinessEntity> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Favorite> Favorites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Campaign - Business ilişkisi
            modelBuilder.Entity<Campaign>()
                .HasOne(c => c.Business)
                .WithMany(b => b.Campaigns)
                .HasForeignKey(c => c.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);

            // Campaign - Category ilişkisi
            modelBuilder.Entity<Campaign>()
                .HasOne(c => c.Category)
                .WithMany(c => c.Campaigns)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Business - Category ilişkisi
            modelBuilder.Entity<BusinessEntity>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Businesses)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Favorite - Business ilişkisi
            modelBuilder.Entity<Favorite>()
                .HasOne(f => f.Business)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);

            // Campaign.DiscountRate için hassasiyet ayarı
            modelBuilder.Entity<Campaign>()
                .Property(c => c.DiscountRate)
                .HasPrecision(5, 2);

            // Title için unique index'i kaldır
            modelBuilder.Entity<Campaign>()
                .HasIndex(c => c.Title)
                .IsUnique(false);

            // Business.Name için unique index'i kaldır
            modelBuilder.Entity<BusinessEntity>()
                .HasIndex(b => b.Name)
                .IsUnique(false);
        }
    }
} 