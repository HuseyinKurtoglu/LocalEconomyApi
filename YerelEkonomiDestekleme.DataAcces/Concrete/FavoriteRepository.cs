using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalEconomyApi.DataAccess.Concrete.EntityFramework
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        public FavoriteRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Favorite> GetFavoritesByUserId(int userId)
        {
            return _context.Set<Favorite>()
                .Include(f => f.Campaign)
                .Where(f => f.UserId == userId && !f.IsDeleted)
                .ToList();
        }
    }
}
