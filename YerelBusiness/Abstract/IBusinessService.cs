using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelBusiness.Abstract
{
    public interface IBusinessService
    {
        Task<List<Business>> GetAllBusinessesAsync();
        Task<Business> GetBusinessByIdAsync(int id);
        Task<Business> AddBusinessAsync(Business business);
        Task<Business> UpdateBusinessAsync(Business business);
        Task<List<Business>> GetBusinessesByCategoryAsync(int categoryId);
        Task<List<Business>> GetBusinessesByUserAsync(string userId);
        Task DeleteBusinessAsync(Business business);
        List<Business> GetBusinessesByCity(string city);
    }
}
