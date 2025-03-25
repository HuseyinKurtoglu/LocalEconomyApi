using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Abstract
{
    public interface ILogService
    {
        Task<List<Log>> GetAllLogs();
        Task<Log> GetLogById(int id);
        Task<List<Log>> GetLogsByUser(string userId);
        Task<Log> AddLog(Log log);
        Task<Log> UpdateLog(Log log);
        Task DeleteLog(int id);
    }
} 