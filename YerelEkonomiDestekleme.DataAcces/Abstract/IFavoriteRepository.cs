using LocalEconomyApi.Models;
namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface IFavoriteRepository : IGenericRepository<Favorite>
    {
        IEnumerable<Favorite> GetFavoritesByUserId(int userId);
    }
}
