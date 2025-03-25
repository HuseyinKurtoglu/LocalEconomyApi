using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        Task<List<Favorite>> GetByUserAsync(string userId);
        Task<List<Favorite>> GetByBusinessAsync(int businessId);
        Task<Favorite> GetByUserAndBusinessAsync(string userId, int businessId);
        Task<bool> IsFavoriteAsync(string userId, int businessId);
    }
}
