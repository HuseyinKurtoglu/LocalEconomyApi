using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelBusiness.Abstract
{
    public interface ICampaignService
    {
        Task<List<Campaign>> GetAllCampaignsAsync();
        Task<Campaign> GetCampaignByIdAsync(int id);
        Task<Campaign> AddCampaignAsync(Campaign campaign);
        Task<Campaign> UpdateCampaignAsync(Campaign campaign);
        Task DeleteCampaignAsync(Campaign campaign);
        Task<List<Campaign>> GetCampaignsByBusinessAsync(int businessId);
        Task<List<Campaign>> GetActiveCampaignsAsync();
        Task<List<Campaign>> GetCampaignsByCategoryAsync(int categoryId);
    }
}
