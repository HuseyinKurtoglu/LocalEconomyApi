using LocalEconomyApi.Models;

namespace LocalEconomyApi.Abstract
{
    public interface ILogService
    {
        IEnumerable<Log> GetAllLogs();
        Log GetLogById(int id);
        void AddLog(Log log);
        void UpdateLog(Log log);
        void DeleteLog(int id);
        IEnumerable<Log> GetLogsByUserId(int userId);
    }
}
