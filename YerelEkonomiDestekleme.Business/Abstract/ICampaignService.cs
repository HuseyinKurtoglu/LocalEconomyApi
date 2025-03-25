using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface ICampaignService
    {
        Task<List<Campaign>> GetAllCampaigns();
        Task<Campaign?> GetCampaignById(int id);
        Task<List<Campaign>> GetCampaignsByCategory(int categoryId);
        Task<List<Campaign>> GetCampaignsByBusiness(int businessId);
        Task<Campaign> AddCampaign(Campaign campaign);
        Task<Campaign> UpdateCampaign(Campaign campaign);
        Task DeleteCampaign(int id);
    }
} 