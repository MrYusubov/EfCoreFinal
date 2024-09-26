﻿namespace MarketApp.Models
{
    public class Cart
    {
        public int CartId { get; set; } 
        public int UserId { get; set; } 
        public User User { get; set; } 
        public ICollection<CartItem> Items { get; set; } = new List<CartItem>();
    }

}
