namespace LocalEconomyApi.Models
{
    public class Log
    {
        public int LogId { get; set; }
        public int? UserId { get; set; }
        public User User { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
