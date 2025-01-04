using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalEconomyApi.DataAccess.Concrete.EntityFramework
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private new readonly AppDbContext _context;
        public CategoryRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Business> GetBusinessesByCategoryId(int categoryId)
        {
            return _context.Set<Business>()
                           .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
                           .ToList();
        }

        public IEnumerable<Campaign> GetCampaignsByCategoryId(int categoryId)
        {
            return _context.Set<Campaign>()
                           .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                           .ToList();
        }

        public IEnumerable<Category> GetActiveCategories()
        {
            return _context.Set<Category>()
                           .Where(c => !c.IsDeleted)
                           .ToList();
        }

        public Category FindCategoryByName(string name)
        {
            return _context.Set<Category>()
                           .FirstOrDefault(c => c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) && !c.IsDeleted);
        }
    }
}
