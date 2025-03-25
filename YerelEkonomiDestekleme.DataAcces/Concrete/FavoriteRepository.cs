using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class FavoriteRepository : GenericRepository<Favorite>, IFavoriteRepository
    {
        private new readonly AppDbContext _context;

        public FavoriteRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public new async Task<List<Favorite>> GetAllAsync()
        {
            return await _context.Set<Favorite>()
                .Include(f => f.Business)
                .Include(f => f.User)
                .Where(f => !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetFavoritesByUserId(int userId)
        {
            return await _context.Set<Favorite>()
                .Where(f => !f.IsDeleted && f.UserId == userId.ToString())
                .Include(f => f.Business)
                .Include(f => f.User)
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetFavoritesByBusinessId(int businessId)
        {
            return await _context.Set<Favorite>()
                .Where(f => !f.IsDeleted && f.BusinessId == businessId)
                .Include(f => f.Business)
                .Include(f => f.User)
                .ToListAsync();
        }

        public async Task<Favorite?> GetFavoriteById(int id)
        {
            return await _context.Set<Favorite>()
                .Include(f => f.Business)
                .Include(f => f.User)
                .FirstOrDefaultAsync(f => f.FavoriteId == id && !f.IsDeleted);
        }

        public new async Task<Favorite> GetByIdAsync(int id)
        {
            return await GetFavoriteById(id) ?? throw new InvalidOperationException($"ID {id} olan favori bulunamadı.");
        }

        public new async Task<Favorite> AddAsync(Favorite entity)
        {
            await _context.Set<Favorite>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public new async Task DeleteAsync(Favorite entity)
        {
            var favorite = await GetFavoriteById(entity.FavoriteId);
            if (favorite != null)
            {
                favorite.IsDeleted = true;
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Favorite>> GetByUserIdAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Business)
                .Where(f => f.UserId == userId)
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetByBusinessIdAsync(int businessId)
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Where(f => f.BusinessId == businessId)
                .ToListAsync();
        }

        public async Task<Favorite> GetByUserAndBusinessAsync(string userId, int businessId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var favorite = await _context.Favorites
                .FirstOrDefaultAsync(f => f.UserId == userId && f.BusinessId == businessId && !f.IsDeleted);
            
            return favorite ?? throw new InvalidOperationException($"Kullanıcı ID {userId} ve İşletme ID {businessId} için favori bulunamadı.");
        }

        public async Task<bool> IsFavoriteAsync(string userId, int businessId)
        {
            return await _context.Favorites
                .AnyAsync(f => f.UserId == userId && f.BusinessId == businessId && !f.IsDeleted);
        }

        public async Task<List<Favorite>> GetByUserAsync(string userId)
        {
            return await _context.Favorites
                .Include(f => f.Business)
                .Where(f => f.UserId == userId && !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<List<Favorite>> GetByBusinessAsync(int businessId)
        {
            return await _context.Favorites
                .Include(f => f.User)
                .Where(f => f.BusinessId == businessId && !f.IsDeleted)
                .ToListAsync();
        }

        public async Task<Favorite> GetFavoriteWithDetails(int id)
        {
            var favorite = await _context.Favorites
                .Include(f => f.User!)
                .Include(f => f.Business!)
                .FirstOrDefaultAsync(f => f.FavoriteId == id && !f.IsDeleted);

            if (favorite == null)
                throw new InvalidOperationException($"ID {id} olan favori bulunamadı.");

            return favorite;
        }

        public async Task<List<Favorite>> GetUserFavorites(string userId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new ArgumentNullException(nameof(userId));

            var favorites = await _context.Favorites
                .Include(f => f.Business!)
                .Where(f => f.UserId == userId && !f.IsDeleted)
                .ToListAsync();

            return favorites ?? new List<Favorite>();
        }
    }
}
