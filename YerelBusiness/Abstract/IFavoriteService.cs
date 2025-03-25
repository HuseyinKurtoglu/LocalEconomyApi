using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface IFavoriteService
    {
        Task<List<Favorite>> GetAllFavorites();
        Task<Favorite> GetFavoriteById(int id);
        Task<List<Favorite>> GetFavoritesByUser(string userId);
        Task<Favorite> AddFavorite(Favorite favorite);
        Task DeleteFavorite(int id);
    }
}
