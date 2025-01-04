using LocalEconomyApi.Models;

namespace LocalEconomyApi.DataAccess.Abstract
{
    public interface ILogRepository : IGenericRepository<Log>
    {
        IEnumerable<Log> GetLogsByUserId(int userId);
    }
}
