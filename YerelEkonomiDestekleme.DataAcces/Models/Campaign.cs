using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;

namespace LocalEconomyApi.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal DiscountRate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }
        public int BusinessId { get; set; }
        public Business Business { get; set; }
    }
}
