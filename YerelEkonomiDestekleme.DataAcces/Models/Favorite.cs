using System;
using System.ComponentModel.DataAnnotations;
using YerelEkonomiDestekleme.DataAcces.Entity;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class Favorite : BaseEntity
    {
        [Key]
        public int FavoriteId { get; set; }

        public string? UserId { get; set; }
        public User? User { get; set; }

        public int BusinessId { get; set; }
        public BusinessEntity? Business { get; set; }

        public new DateTime CreatedDate { get; set; }
        public new bool IsDeleted { get; set; }
    }
}