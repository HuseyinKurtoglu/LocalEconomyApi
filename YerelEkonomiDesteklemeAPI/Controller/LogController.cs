using LocalEconomyApi.Abstract;
using LocalEconomyApi.Models;
using Microsoft.AspNetCore.Mvc;
using System;

namespace LocalEconomyApi.Controllers
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
        public IActionResult GetAllLogs()
        {
            try
            {
                var logs = _logService.GetAllLogs();
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetLogById(int id)
        {
            try
            {
                var log = _logService.GetLogById(id);
                return Ok(log);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPost]
        public IActionResult AddLog([FromBody] Log log)
        {
            try
            {
                _logService.AddLog(log);
                return CreatedAtAction(nameof(GetLogById), new { id = log.LogId }, log);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateLog(int id, [FromBody] Log log)
        {
            try
            {
                if (id != log.LogId)
                    return BadRequest(new { message = "Log ID mismatch." });

                _logService.UpdateLog(log);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteLog(int id)
        {
            try
            {
                _logService.DeleteLog(id);
                return NoContent();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        [HttpGet("User/{userId}")]
        public IActionResult GetLogsByUserId(int userId)
        {
            try
            {
                var logs = _logService.GetLogsByUserId(userId);
                return Ok(logs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
