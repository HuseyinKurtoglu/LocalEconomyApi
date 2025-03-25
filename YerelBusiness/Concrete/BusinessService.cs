using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelBusiness.Abstract;

namespace YerelBusiness.Concrete
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<List<Business>> GetAllBusinessesAsync()
        {
            var businesses = await _businessRepository.GetAllAsync();
            return businesses.ToList();
        }

        public async Task<Business> GetBusinessByIdAsync(int id)
        {
            return await _businessRepository.GetByIdAsync(id);
        }

        public async Task<Business> AddBusinessAsync(Business business)
        {
            return await _businessRepository.AddAsync(business);
        }

        public async Task<Business> UpdateBusinessAsync(Business business)
        {
            return await _businessRepository.UpdateAsync(business);
        }

        public async Task<List<Business>> GetBusinessesByCategoryAsync(int categoryId)
        {
            return await _businessRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<List<Business>> GetBusinessesByUserAsync(string userId)
        {
            return await _businessRepository.GetByUserAsync(userId);
        }

        public async Task DeleteBusinessAsync(Business business)
        {
            await _businessRepository.DeleteAsync(business);
        }

        public List<Business> GetBusinessesByCity(string city)
        {
            return _businessRepository.GetByCity(city).ToList();
        }
    }
}

