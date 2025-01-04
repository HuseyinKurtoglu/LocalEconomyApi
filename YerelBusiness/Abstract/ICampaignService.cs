using LocalEconomyApi.Models;
using System.Collections.Generic;

namespace LocalEconomyApi.Abstract
{
    public interface ICampaignService
    {
        IEnumerable<Campaign> GetAllCampaigns();
        Campaign GetCampaignById(int id);
        void AddCampaign(Campaign campaign);
        void UpdateCampaign(Campaign campaign);
        void DeleteCampaign(int id);
        IEnumerable<Campaign> GetActiveCampaigns();
        IEnumerable<Campaign> GetCampaignsByCategory(int categoryId);
        IEnumerable<Campaign> GetCampaignsByBusiness(int businessId);
    }
}
