using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private new readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithBusinesses()
        {
            var categories = await _context.Categories
                .Include(c => c.Businesses!.Where(b => !b.IsDeleted))
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.Businesses ??= new List<BusinessEntity>();
            }

            return categories;
        }

        public async Task<Category> GetCategoryWithBusinesses(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Businesses!.Where(b => !b.IsDeleted))
                .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);

            if (category == null)
                throw new InvalidOperationException($"ID {id} olan kategori bulunamadı.");

            category.Businesses ??= new List<BusinessEntity>();

            return category;
        }

        public async Task<IEnumerable<BusinessEntity>> GetBusinessesByCategoryId(int categoryId)
        {
            return await _context.Businesses
                .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Campaign>> GetCampaignsByCategoryId(int categoryId)
        {
            return await _context.Campaigns
                .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<Category>> GetActiveCategories()
        {
            var categories = await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
            
            return categories ?? new List<Category>();
        }

        public async Task<Category?> FindCategoryByName(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted);
        }

        public async Task<List<Category>> GetAllWithBusinessesAsync()
        {
            return await _context.Categories
                .Include(c => c.Businesses)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category?> GetByIdWithBusinessesAsync(int id)
        {
            return await _context.Categories
                .Include(c => c.Businesses)
                .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);
        }

        public async Task<List<BusinessEntity>> GetBusinessesByCategoryAsync(int categoryId)
        {
            return await _context.Businesses
                .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name != null && c.Name == name && !c.IsDeleted);
        }

        public async Task<List<Category>> GetActiveCategoriesAsync()
        {
            return await _context.Categories
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<Category?> FindCategoryByNameAsync(string name)
        {
            return await _context.Categories
                .FirstOrDefaultAsync(c => c.Name != null && c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted);
        }

        public async Task<Category> GetCategoryWithCampaigns(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Campaigns!.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);

            if (category == null)
                throw new InvalidOperationException($"ID {id} olan kategori bulunamadı.");

            category.Campaigns ??= new List<Campaign>();

            return category;
        }

        public async Task<Category> GetCategoryWithBusinessesAndCampaigns(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Businesses!.Where(b => !b.IsDeleted))
                .Include(c => c.Campaigns!.Where(c => !c.IsDeleted))
                .FirstOrDefaultAsync(c => c.CategoryId == id && !c.IsDeleted);

            if (category == null)
                throw new InvalidOperationException($"ID {id} olan kategori bulunamadı.");

            category.Businesses ??= new List<BusinessEntity>();
            category.Campaigns ??= new List<Campaign>();

            return category;
        }

        public async Task<Category> GetByNameAsync(string name)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(nameof(name));

            var category = await _context.Categories
                .FirstOrDefaultAsync(c => c.Name == name && !c.IsDeleted);

            if (category == null)
                throw new InvalidOperationException($"İsim {name} olan kategori bulunamadı.");

            return category;
        }

        public async Task<IEnumerable<Category>> GetCategoriesWithCampaigns()
        {
            var categories = await _context.Categories
                .Include(c => c.Campaigns!.Where(c => !c.IsDeleted))
                .Where(c => !c.IsDeleted)
                .ToListAsync();

            foreach (var category in categories)
            {
                category.Campaigns ??= new List<Campaign>();
            }

            return categories;
        }
    }
}
