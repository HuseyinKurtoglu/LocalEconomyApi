using LocalEconomyApi.Data;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace LocalEconomyApi.DataAccess.Concrete.EntityFramework
{
    public class LogRepository : GenericRepository<Log>, ILogRepository
    {
        public LogRepository(AppDbContext context) : base(context) { }

        public IEnumerable<Log> GetLogsByUserId(int userId)
        {
            return _context.Set<Log>().Where(l => l.UserId == userId && !l.IsDeleted).ToList();
        }
    }
}
