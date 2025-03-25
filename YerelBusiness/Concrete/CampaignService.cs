using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelBusiness.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelBusiness.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<Campaign>> GetAllCampaignsAsync()
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            return campaigns.ToList();
        }

        public async Task<Campaign> GetCampaignByIdAsync(int id)
        {
            return await _campaignRepository.GetByIdAsync(id);
        }

        public async Task<Campaign> AddCampaignAsync(Campaign campaign)
        {
            return await _campaignRepository.AddAsync(campaign);
        }

        public async Task<Campaign> UpdateCampaignAsync(Campaign campaign)
        {
            return await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task DeleteCampaignAsync(Campaign campaign)
        {
            await _campaignRepository.DeleteAsync(campaign);
        }

        public async Task<List<Campaign>> GetCampaignsByBusinessAsync(int businessId)
        {
            return await _campaignRepository.GetCampaignsByBusinessAsync(businessId);
        }

        public async Task<List<Campaign>> GetActiveCampaignsAsync()
        {
            return await _campaignRepository.GetActiveCampaignsAsync();
        }

        public async Task<List<Campaign>> GetCampaignsByCategoryAsync(int categoryId)
        {
            return await _campaignRepository.GetCampaignsByCategoryAsync(categoryId);
        }
    }
}
