using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface IBusinessRepository : IGenericRepository<BusinessEntity>
    {
        Task<List<BusinessEntity>> GetByCategoryAsync(int categoryId);
        Task<List<BusinessEntity>> GetByUserAsync(string userId);
        Task<List<BusinessEntity>> GetByCityAsync(string city);
    }
}
