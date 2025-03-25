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
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        private new readonly AppDbContext _context;

        public CampaignRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public override async Task<Campaign> GetByIdAsync(int id)
        {
            var campaign = await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .FirstOrDefaultAsync(c => c.CampaignId == id);

            if (campaign == null || campaign.IsDeleted)
            {
                throw new InvalidOperationException($"ID {id} olan Campaign bulunamadı.");
            }

            return campaign;
        }

        public override async Task DeleteAsync(Campaign campaign)
        {
            var entity = await _context.Campaigns.FindAsync(campaign.CampaignId);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _context.Campaigns.Update(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Campaign>> GetByCategoryAsync(int categoryId)
        {
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => c.CategoryId == categoryId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetByBusinessAsync(int businessId)
        {
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => c.BusinessId == businessId && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetActiveAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => c.StartDate <= now && c.EndDate >= now && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetExpiredAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => c.EndDate < now && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetUpcomingAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => c.StartDate > now && !c.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetCampaignsByBusinessAsync(int businessId)
        {
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => !c.IsDeleted && c.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetActiveCampaignsAsync()
        {
            var now = DateTime.UtcNow;
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => !c.IsDeleted && c.StartDate <= now && c.EndDate >= now)
                .ToListAsync();
        }

        public async Task<List<Campaign>> GetCampaignsByCategoryAsync(int categoryId)
        {
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => !c.IsDeleted && c.CategoryId == categoryId)
                .ToListAsync();
        }

        public override async Task<IEnumerable<Campaign>> GetAllAsync()
        {
            return await _context.Campaigns
                .Include(c => c.Business)
                .Include(c => c.Category)
                .Where(c => !c.IsDeleted)
                .ToListAsync();
        }
    }
}
