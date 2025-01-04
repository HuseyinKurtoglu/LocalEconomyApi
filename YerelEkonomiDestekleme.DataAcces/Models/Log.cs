namespace LocalEconomyApi.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}