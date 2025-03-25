using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YerelEkonomiDestekleme.DataAcces.Entity;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class Log : BaseEntity
    {
        public int LogId { get; set; }
        public string? Message { get; set; }
        public string? Level { get; set; }
        public string? UserId { get; set; }
        public User? User { get; set; }
        public string? Action { get; set; }
        public string? Details { get; set; }
        public string? ActionType { get; set; }
        public string? EntityType { get; set; }
        public int? EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
        public new DateTime CreatedDate { get; set; }
        public new bool IsDeleted { get; set; }

        public Log()
        {
            Message = string.Empty;
            Level = string.Empty;
            UserId = string.Empty;
            Action = string.Empty;
            CreatedAt = DateTime.UtcNow;
        }
    }
}