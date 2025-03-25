using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class BusinessService : IBusinessService
    {
        private readonly IBusinessRepository _businessRepository;

        public BusinessService(IBusinessRepository businessRepository)
        {
            _businessRepository = businessRepository;
        }

        public async Task<List<BusinessEntity>> GetAllBusinesses()
        {
            var businesses = await _businessRepository.GetAllAsync();
            return businesses.ToList();
        }

        public async Task<BusinessEntity?> GetBusinessById(int id)
        {
            return await _businessRepository.GetByIdAsync(id);
        }

        public async Task<List<BusinessEntity>> GetBusinessesByCategory(int categoryId)
        {
            return await _businessRepository.GetByCategoryAsync(categoryId);
        }

        public async Task<List<BusinessEntity>> GetBusinessesByCity(string city)
        {
            var businesses = await _businessRepository.GetByCityAsync(city);
            return businesses.ToList();
        }

        public async Task<BusinessEntity> AddBusiness(BusinessEntity business)
        {
            return await _businessRepository.AddAsync(business);
        }

        public async Task<BusinessEntity> UpdateBusiness(BusinessEntity business)
        {
            return await _businessRepository.UpdateAsync(business);
        }

        public async Task DeleteBusiness(int id)
        {
            var business = await _businessRepository.GetByIdAsync(id);
            if (business != null)
            {
                await _businessRepository.DeleteAsync(business);
            }
        }

        public async Task<bool> SoftDeleteBusiness(int id)
        {
            var business = await _businessRepository.GetByIdAsync(id);
            if (business == null)
            {
                return false;
            }

            business.IsDeleted = true;
            await _businessRepository.UpdateAsync(business);
            return true;
        }
    }
} 