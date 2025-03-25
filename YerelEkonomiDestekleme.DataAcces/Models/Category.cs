using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class Category : BaseEntity
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [StringLength(50)]
        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<BusinessEntity>? Businesses { get; set; }

        public ICollection<Campaign>? Campaigns { get; set; }

        public Category()
        {
            Businesses = new List<BusinessEntity>();
            Campaigns = new List<Campaign>();
        }
    }
}
