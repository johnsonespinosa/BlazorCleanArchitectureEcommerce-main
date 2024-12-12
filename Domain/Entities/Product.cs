﻿namespace Domain.Entities
{
    public class Product
    {
        public Guid CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
    }
}
