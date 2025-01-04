using LocalEconomyApi.Abstract;
using LocalEconomyApi.DataAccess.Abstract;
using LocalEconomyApi.Models;
using System;
using System.Collections.Generic;

namespace LocalEconomyApi.Concrete
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public IEnumerable<Log> GetAllLogs()
        {
            return _logRepository.GetAll();
        }

        public Log GetLogById(int id)
        {
            var log = _logRepository.Get(l => l.LogId == id);
            if (log == null) throw new KeyNotFoundException($"Log with ID {id} not found.");
            return log;
        }

        public void AddLog(Log log)
        {
            _logRepository.Add(log);
        }

        public void UpdateLog(Log log)
        {
            var existingLog = _logRepository.Get(l => l.LogId == log.LogId);
            if (existingLog == null) throw new KeyNotFoundException($"Log with ID {log.LogId} not found.");

            _logRepository.Update(log);
        }

        public void DeleteLog(int id)
        {
            var log = _logRepository.Get(l => l.LogId == id);
            if (log == null) throw new KeyNotFoundException($"Log with ID {id} not found.");

            _logRepository.Delete(log);
        }

        public IEnumerable<Log> GetLogsByUserId(int userId)
        {
            return _logRepository.GetLogsByUserId(userId);
        }
    }
}