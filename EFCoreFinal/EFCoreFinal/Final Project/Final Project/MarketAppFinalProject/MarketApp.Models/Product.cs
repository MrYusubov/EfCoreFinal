﻿namespace MarketApp.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double Price { get; set; }
        public string Description { get; set; }
        public int Stock { get; set; }
        public int CategId { get; set; }
        public Category Category { get; set; }
    }
}
