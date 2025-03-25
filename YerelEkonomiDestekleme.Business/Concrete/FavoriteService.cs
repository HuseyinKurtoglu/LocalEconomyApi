using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public async Task<List<Favorite>> GetAllFavorites()
        {
            var favorites = await _favoriteRepository.GetAllAsync();
            return favorites.ToList();
        }

        public async Task<Favorite> GetFavoriteById(int id)
        {
            return await _favoriteRepository.GetByIdAsync(id);
        }

        public async Task<List<Favorite>> GetFavoritesByUser(string userId)
        {
            return await _favoriteRepository.GetByUserAsync(userId);
        }

        public async Task<Favorite> AddFavorite(Favorite favorite)
        {
            return await _favoriteRepository.AddAsync(favorite);
        }

        public async Task<Favorite> UpdateFavorite(Favorite favorite)
        {
            return await _favoriteRepository.UpdateAsync(favorite);
        }

        public async Task DeleteFavorite(int id)
        {
            var favorite = await _favoriteRepository.GetByIdAsync(id);
            if (favorite != null)
            {
                await _favoriteRepository.DeleteAsync(favorite);
            }
        }
    }
} 