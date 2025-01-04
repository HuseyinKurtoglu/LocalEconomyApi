﻿namespace LocalEconomyApi.Models
{
    public class Favorite
    {
        public int FavoriteId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}