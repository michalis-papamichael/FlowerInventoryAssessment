using App.Dtos.Categories;
using App.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos.Flowers
{
    public class CreateFlowerDto
    {
        public CreateFlowerDto()
        {
            Categories = new List<CategoryDto>();
            Messages = new List<ViewMessage>();
        }
        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Name { get; set; }
        [StringLength(750)]
        public string? Description { get; set; }
        [Precision(9, 2)]
        [Range(0, double.MaxValue)]
        public decimal Price { get; set; }
        public int TotalInventory { get; set; }
        public int CategoryId { get; set; }

        public IFormFile? ImageFormFile { get; set; }

        // Categories for the select
        public List<CategoryDto> Categories { get; set; }
        // Used to show message to user according to outcome
        public List<ViewMessage> Messages { get; set; }
    }
}
