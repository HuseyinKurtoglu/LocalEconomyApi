using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface IBusinessService
    {
        Task<List<BusinessEntity>> GetAllBusinesses();
        Task<BusinessEntity?> GetBusinessById(int id);
        Task<List<BusinessEntity>> GetBusinessesByCategory(int categoryId);
        Task<List<BusinessEntity>> GetBusinessesByCity(string city);
        Task<BusinessEntity> AddBusiness(BusinessEntity business);
        Task<BusinessEntity> UpdateBusiness(BusinessEntity business);
        Task DeleteBusiness(int id);
        Task<bool> SoftDeleteBusiness(int id);
    }
} 