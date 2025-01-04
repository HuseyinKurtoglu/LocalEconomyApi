using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalEconomyApi.DataAccess.Concrete.EntityFramework
{
    public class CampaignRepository : GenericRepository<Campaign>, ICampaignRepository
    {
        public CampaignRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Campaign> GetActiveCampaigns()
        {
            return _context.Set<Campaign>().Where(c => !c.IsDeleted && c.StartDate <= DateTime.Now && c.EndDate >= DateTime.Now).ToList();
        }

        public IEnumerable<Campaign> GetCampaignsByCategory(int categoryId)
        {
            return _context.Set<Campaign>().Where(c => c.CategoryId == categoryId && !c.IsDeleted).ToList();
        }

        public IEnumerable<Campaign> GetCampaignsByBusiness(int businessId)
        {
            return _context.Set<Campaign>().Where(c => c.BusinessId == businessId && !c.IsDeleted).ToList();
        }
    }
}
