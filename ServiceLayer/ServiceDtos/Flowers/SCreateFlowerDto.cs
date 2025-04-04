using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.ServiceDtos.Flowers
{
    public class SCreateFlowerDto
    {
        [Key]
        public int Id { get; set; }
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(750)]
        public string? Description { get; set; }
        [Precision(9, 2)]
        public decimal Price { get; set; }
        public int CategoryId { get; set; }
    }
}
