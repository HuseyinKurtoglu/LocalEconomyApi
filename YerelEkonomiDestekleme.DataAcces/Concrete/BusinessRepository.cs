using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class BusinessRepository : GenericRepository<BusinessEntity>, IBusinessRepository
    {
        private new readonly AppDbContext _context;

        public BusinessRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<IEnumerable<BusinessEntity>> GetAllAsync()
        {
            return await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<BusinessEntity>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => b.CategoryId == categoryId && !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<BusinessEntity>> GetByUserAsync(string userId)
        {
            return await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => !b.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<BusinessEntity>> GetByCityAsync(string city)
        {
            return await _context.Businesses
                .Include(b => b.Category)
                .Include(b => b.Campaigns)
                .Where(b => b.City != null && b.City.ToLower() == city.ToLower() && !b.IsDeleted)
                .ToListAsync();
        }
    }
}
