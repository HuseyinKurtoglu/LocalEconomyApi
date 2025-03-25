using System;

namespace YerelEkonomiDestekleme.DataAcces.Entity
{
    public abstract class BaseEntity
    {
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
    }
} 