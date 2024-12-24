using LocalEconomyApi.Models;

namespace LocalEconomyApi.Models
{
    public class Business
    {
        public int BusinessId { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string PhoneNumber { get; set; }
        public int? CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Campaign> Campaigns { get; set; }
    }
}
