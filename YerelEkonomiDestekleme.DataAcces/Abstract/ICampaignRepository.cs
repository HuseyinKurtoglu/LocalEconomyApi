using LocalEconomyApi.Models;
using System.Collections.Generic;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface ICampaignRepository : IGenericRepository<Campaign>
    {
        IEnumerable<Campaign> GetActiveCampaigns();
        IEnumerable<Campaign> GetCampaignsByCategory(int categoryId);
        IEnumerable<Campaign> GetCampaignsByBusiness(int businessId);
    }
}