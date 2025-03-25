using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Concrete;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Concrete
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        private new readonly AppDbContext _context;

        public LogRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Log>> GetByUserAsync(string userId)
        {
            return await _context.Set<Log>()
                .Include(l => l.User)
                .Where(l => l.UserId == userId && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Log>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _context.Set<Log>()
                .Include(l => l.User)
                .Where(l => l.CreatedAt >= startDate && l.CreatedAt <= endDate && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Log>> GetByActionTypeAsync(string actionType)
        {
            return await _context.Set<Log>()
                .Include(l => l.User)
                .Where(l => l.ActionType == actionType && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Log>> GetByEntityTypeAsync(string entityType)
        {
            return await _context.Set<Log>()
                .Include(l => l.User)
                .Where(l => l.EntityType == entityType && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }

        public async Task<List<Log>> GetByEntityIdAsync(int entityId)
        {
            return await _context.Set<Log>()
                .Include(l => l.User)
                .Where(l => l.EntityId == entityId && !l.IsDeleted)
                .OrderByDescending(l => l.CreatedAt)
                .ToListAsync();
        }
    }
}
