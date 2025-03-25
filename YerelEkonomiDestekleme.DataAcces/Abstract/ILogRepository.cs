using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Abstract
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        Task<List<Log>> GetByUserAsync(string userId);
        Task<List<Log>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<List<Log>> GetByActionTypeAsync(string actionType);
        Task<List<Log>> GetByEntityTypeAsync(string entityType);
        Task<List<Log>> GetByEntityIdAsync(int entityId);
    }
}
