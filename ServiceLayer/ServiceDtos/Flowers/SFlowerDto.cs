﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceDtos.Flowers
{
    public class SFlowerDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int TotalInventory { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName {  get; set; }
    }
}
