using LocalEconomyApi.Models;
using LocalEconomyApi.Models.Concrete;

namespace LocalEconomyApi.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }

        public ICollection<Business> Businesses { get; set; }
        public ICollection<Campaign> Campaigns { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }

    }
}
