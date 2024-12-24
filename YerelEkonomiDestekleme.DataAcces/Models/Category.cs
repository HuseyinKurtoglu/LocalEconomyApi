using LocalEconomyApi.Models;

namespace LocalEconomyApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Business> Businesses { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
    }
}
