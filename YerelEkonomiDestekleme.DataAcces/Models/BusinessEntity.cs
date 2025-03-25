using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using YerelEkonomiDestekleme.DataAcces.Entity;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class BusinessEntity : BaseEntity
    {
        [Key]
        public int BusinessId { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        
        public string? Description { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? ImageUrl { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public ICollection<Campaign>? Campaigns { get; set; }
        public ICollection<Favorite>? Favorites { get; set; }

        public BusinessEntity()
        {
            Campaigns = new List<Campaign>();
            Favorites = new List<Favorite>();
        }
    }
}
 