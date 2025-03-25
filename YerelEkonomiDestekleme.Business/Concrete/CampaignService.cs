using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class CampaignService : ICampaignService
    {
        private readonly ICampaignRepository _campaignRepository;

        public CampaignService(ICampaignRepository campaignRepository)
        {
            _campaignRepository = campaignRepository;
        }

        public async Task<List<Campaign>> GetAllCampaigns()
        {
            var campaigns = await _campaignRepository.GetAllAsync();
            return campaigns.Where(c => !c.IsDeleted).ToList();
        }

        public async Task<Campaign?> GetCampaignById(int id)
        {
            try
            {
                return await _campaignRepository.GetByIdAsync(id);
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }

        public async Task<List<Campaign>> GetCampaignsByCategory(int categoryId)
        {
            return await _campaignRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<List<Campaign>> GetCampaignsByBusiness(int businessId)
        {
            return await _campaignRepository.GetByBusinessAsync(businessId);
        }

        public async Task<Campaign> AddCampaign(Campaign campaign)
        {
            return await _campaignRepository.AddAsync(campaign);
        }

        public async Task<Campaign> UpdateCampaign(Campaign campaign)
        {
            return await _campaignRepository.UpdateAsync(campaign);
        }

        public async Task DeleteCampaign(int id)
        {
            var campaign = new Campaign { CampaignId = id };
            await _campaignRepository.DeleteAsync(campaign);
        }
    }
} 