using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using YerelEkonomiDestekleme.DataAcces.Context;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDesteklemeAPI.Data
{
    public static class SeedData
    {
        public static async Task Initialize(IServiceProvider serviceProvider)
        {
            var context = serviceProvider.GetRequiredService<AppDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<User>>();

            // Kategorileri ekle
            if (!context.Categories.Any())
            {
                var categories = new[]
                {
                    new Category { Name = "Restoran", Description = "Yemek mekanları" },
                    new Category { Name = "Kafe", Description = "Kafeler" },
                    new Category { Name = "Market", Description = "Yerel marketler" },
                    new Category { Name = "Eczane", Description = "Eczaneler" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Test kullanıcısını ekle
            if (!context.Users.Any())
            {
                var testUser = new User
                {
                    UserName = "test@example.com",
                    Email = "test@example.com",
                    FirstName = "Test",
                    LastName = "Kullanıcı",
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(testUser, "Test123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(testUser, "User");
                }

                var adminUser = new User
                {
                    UserName = "admin@example.com",
                    Email = "admin@example.com",
                    FirstName = "Admin",
                    LastName = "Kullanıcı",
                    EmailConfirmed = true
                };

                result = await userManager.CreateAsync(adminUser, "Admin123!");
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(adminUser, "Admin");
                }
            }

            // Test işletmelerini ekle
            if (!context.Businesses.Any())
            {
                var businesses = new[]
                {
                    new BusinessEntity
                    {
                        Name = "Test Restoran",
                        Description = "Lezzetli yemekler",
                        Address = "Test Mahallesi, Test Sokak No:1",
                        Phone = "555-0001",
                        Email = "test@restoran.com",
                        City = "İstanbul",
                        CategoryId = 1
                    },
                    new BusinessEntity
                    {
                        Name = "Test Kafe",
                        Description = "Sıcak içecekler",
                        Address = "Test Mahallesi, Test Sokak No:2",
                        Phone = "555-0002",
                        Email = "test@kafe.com",
                        City = "İstanbul",
                        CategoryId = 2
                    }
                };

                await context.Businesses.AddRangeAsync(businesses);
                await context.SaveChangesAsync();
            }
        }
    }
} 