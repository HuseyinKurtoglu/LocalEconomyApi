using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.Business.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.Business.Concrete
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<List<Log>> GetAllLogs()
        {
            var logs = await _logRepository.GetAllAsync();
            return logs.ToList();
        }

        public async Task<Log> GetLogById(int id)
        {
            return await _logRepository.GetByIdAsync(id);
        }

        public async Task<List<Log>> GetLogsByUserId(string userId)
        {
            var logs = await _logRepository.GetLogsByUserId(userId);
            return logs.ToList();
        }

        public async Task<List<Log>> GetLogsByAction(string action)
        {
            var logs = await _logRepository.GetLogsByAction(action);
            return logs.ToList();
        }

        public async Task<Log> AddLog(Log log)
        {
            return await _logRepository.AddAsync(log);
        }

        public async Task<Log> UpdateLog(Log log)
        {
            return await _logRepository.UpdateAsync(log);
        }

        public async Task DeleteLog(int id)
        {
            var log = await _logRepository.GetByIdAsync(id);
            if (log != null)
            {
                await _logRepository.DeleteAsync(log);
            }
        }
    }
}