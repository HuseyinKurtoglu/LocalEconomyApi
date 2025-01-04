using LocalEconomyApi.Models;
using System.Collections.Generic;

namespace LocalEconomyApi.Abstract
{
    public interface IFavoriteService
    {
        IEnumerable<Favorite> GetFavoritesByUserId(int userId);
        void AddFavorite(Favorite favorite);
        void RemoveFavorite(int favoriteId);
    }
}
