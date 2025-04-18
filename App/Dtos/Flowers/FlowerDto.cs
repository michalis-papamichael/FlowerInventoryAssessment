﻿namespace App.Dtos.Flowers
{
    public class FlowerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int TotalInventory { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string? ImageUri { get; set; }
        public bool IsActive { get; set; }
    }
}
