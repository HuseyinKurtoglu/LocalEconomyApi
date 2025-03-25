using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class Campaign : BaseEntity
    {
        [Key]
        public int CampaignId { get; set; }

        [StringLength(100)]
        public string? Title { get; set; }

        public string? Description { get; set; }

        [Range(0, 100)]
        public decimal DiscountRate { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BusinessId { get; set; }
        public BusinessEntity? Business { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public new DateTime CreatedDate { get; set; }
        public new bool IsDeleted { get; set; }
        public new DateTime? UpdatedDate { get; set; }
        public new string? UpdatedBy { get; set; }
    }
}
