using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using YerelEkonomiDestekleme.DataAcces.Entity;
using YerelEkonomiDestekleme.DataAcces.Models;

namespace YerelEkonomiDestekleme.DataAcces.Models
{
    public class User : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Phone { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public DateTime? DeletedAt { get; set; }

        public virtual ICollection<Favorite> Favorites { get; set; }
        public virtual ICollection<Log> Logs { get; set; }

        public User()
        {
            Favorites = new List<Favorite>();
            Logs = new List<Log>();
            CreatedDate = DateTime.UtcNow;
            IsActive = true;
            IsDeleted = false;
        }
    }
}
