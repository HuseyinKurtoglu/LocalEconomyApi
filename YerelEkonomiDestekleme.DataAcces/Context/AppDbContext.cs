using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Context
{
    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public AppDbContext()
        {
        }

        public DbSet<BusinessEntity> Businesses { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Campaign> Campaigns { get; set; }
        public DbSet<Favorite> Favorites { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<BusinessEntity>()
                .HasOne(b => b.Category)
                .WithMany(c => c.Businesses)
                .HasForeignKey(b => b.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Campaign>()
                .HasOne(c => c.Business)
                .WithMany(b => b.Campaigns)
                .HasForeignKey(c => c.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Campaign>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Campaigns)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
                .HasOne(f => f.Business)
                .WithMany(b => b.Favorites)
                .HasForeignKey(f => f.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.Entity<Favorite>()
                .HasOne(f => f.User)
                .WithMany(u => u.Favorites)
                .HasForeignKey(f => f.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Log>()
                .HasOne(l => l.User)
                .WithMany(u => u.Logs)
                .HasForeignKey(l => l.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasIndex(u => u.Email)
                .IsUnique();

            builder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            builder.Entity<BusinessEntity>()
                .HasIndex(b => b.Name)
                .IsUnique(false);

            builder.Entity<Category>()
                .HasIndex(c => c.Name)
                .IsUnique();

            builder.Entity<Campaign>()
                .HasIndex(c => c.Title)
                .IsUnique();

            builder.Entity<Campaign>()
                .Property(c => c.DiscountRate)
                .HasPrecision(5, 2);

            // Örnek kategoriler
            builder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Restoran", Description = "Yemek hizmeti veren işletmeler" },
                new Category { CategoryId = 2, Name = "Kafe", Description = "Kahve ve atıştırmalık hizmeti veren işletmeler" },
                new Category { CategoryId = 3, Name = "Market", Description = "Gıda ve günlük ihtiyaç ürünleri satan işletmeler" },
                new Category { CategoryId = 4, Name = "Giyim", Description = "Tekstil ve moda ürünleri satan işletmeler" },
                new Category { CategoryId = 5, Name = "Elektronik", Description = "Elektronik ürün ve hizmet sunan işletmeler" },
                new Category { CategoryId = 6, Name = "Spor", Description = "Spor malzemeleri ve hizmetleri sunan işletmeler" },
                new Category { CategoryId = 7, Name = "Sağlık", Description = "Sağlık hizmeti ve ürünleri sunan işletmeler" },
                new Category { CategoryId = 8, Name = "Eğitim", Description = "Eğitim hizmeti veren işletmeler" },
                new Category { CategoryId = 9, Name = "Hizmet", Description = "Çeşitli hizmet sektörü işletmeleri" },
                new Category { CategoryId = 10, Name = "Eğlence", Description = "Eğlence ve aktivite hizmeti veren işletmeler" }
            );
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=LocalEconomyDB2;Trusted_Connection=True;");
            }
        }
    }
} 