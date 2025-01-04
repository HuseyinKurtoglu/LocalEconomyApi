using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using System.Collections.Generic;

namespace LocalEconomyApi.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public IEnumerable<Campaign> GetAllCampaigns()
        {
            return _campaignRepository.GetAll();
        }

        public Campaign GetCampaignById(int id)
        {
            return _campaignRepository.Get(c => c.CampaignId == id);
        }

        public void AddCampaign(Campaign campaign)
        {
            _campaignRepository.Add(campaign);
        }

        public void UpdateCampaign(Campaign campaign)
        {
            _campaignRepository.Update(campaign);
        }

        public void DeleteCampaign(int id)
        {
            var campaign = _campaignRepository.Get(c => c.CampaignId == id);
            if (campaign != null)
            {
                campaign.IsDeleted = true;
                _campaignRepository.Update(campaign);
            }
        }

        public IEnumerable<Campaign> GetActiveCampaigns()
        {
            return _campaignRepository.GetActiveCampaigns();
        }

        public IEnumerable<Campaign> GetCampaignsByCategory(int categoryId)
        {
            return _campaignRepository.GetCampaignsByCategory(categoryId);
        }

        public IEnumerable<Campaign> GetCampaignsByBusiness(int businessId)
        {
            return _campaignRepository.GetCampaignsByBusiness(businessId);
        }
    }
}
