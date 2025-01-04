using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using System;
using System.Collections.Generic;

namespace LocalEconomyApi.Concrete
{
    public class FavoriteService : IFavoriteService
    {
        private readonly IFavoriteRepository _favoriteRepository;

        public FavoriteService(IFavoriteRepository favoriteRepository)
        {
            _favoriteRepository = favoriteRepository;
        }

        public IEnumerable<Favorite> GetFavoritesByUserId(int userId)
        {
            return _favoriteRepository.GetFavoritesByUserId(userId);
        }

        public void AddFavorite(Favorite favorite)
        {
            favorite.UpdatedDate = DateTime.Now;
            favorite.UpdatedBy = "System"; // Giriş yapan kullanıcı bilgisi burada setlenebilir
            _favoriteRepository.Add(favorite);
        }

        public void RemoveFavorite(int favoriteId)
        {
            var favorite = _favoriteRepository.Get(f => f.FavoriteId == favoriteId);
            if (favorite == null) throw new KeyNotFoundException("Favori bulunamadı.");

            favorite.IsDeleted = true;
            favorite.UpdatedDate = DateTime.Now;
            favorite.UpdatedBy = "System"; // Giriş yapan kullanıcı bilgisi burada setlenebilir
            _favoriteRepository.Update(favorite);
        }
    }
}