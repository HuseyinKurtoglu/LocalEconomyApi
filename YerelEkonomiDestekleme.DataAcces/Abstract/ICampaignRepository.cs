using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        Task<List<Campaign>> GetByBusinessAsync(int businessId);
        Task<List<Campaign>> GetActiveAsync();
        Task<List<Campaign>> GetExpiredAsync();
        Task<List<Campaign>> GetUpcomingAsync();
        Task<List<Campaign>> GetCampaignsByBusinessAsync(int businessId);
        Task<List<Campaign>> GetActiveCampaignsAsync();
        Task<List<Campaign>> GetCampaignsByCategoryAsync(int categoryId);
        Task<List<Campaign>> GetByCategoryAsync(int categoryId);
    }
}