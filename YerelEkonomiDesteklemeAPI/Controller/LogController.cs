using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using YerelEkonomiDestekleme.DataAcces.Abstract;
using YerelEkonomiDestekleme.DataAcces.Models;
using YerelEkonomiDestekleme.Business.Abstract;

namespace YerelEkonomiDesteklemeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogController : ControllerBase
    {
        private readonly ILogService _logService;

        public LogController(ILogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Log>>> GetAllLogs()
        {
            var logs = await _logService.GetAllLogs();
            return Ok(logs);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Log>> GetLogById(int id)
        {
            var log = await _logService.GetLogById(id);
            if (log == null)
            {
                return NotFound();
            }
            return Ok(log);
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<Log>>> GetLogsByUser(string userId)
        {
            var logs = await _logService.GetLogsByUser(userId);
            return Ok(logs);
        }

        [HttpPost]
        public async Task<ActionResult<Log>> CreateLog(Log log)
        {
            var createdLog = await _logService.AddLog(log);
            return CreatedAtAction(nameof(GetLogById), new { id = createdLog.LogId }, createdLog);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLog(int id)
        {
            var log = await _logService.GetLogById(id);
            if (log == null)
            {
                return NotFound();
            }

            await _logService.DeleteLog(id);
            return NoContent();
        }
    }
}
