using App.Dtos.Categories;
using App.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace App.Dtos.Flowers
{
    public class EditFlowerDto
    {
        public EditFlowerDto()
        {
            Categories = new List<CategoryDto>();
            Messages = new List<ViewMessage>();
        }
        [Required]
        public int Id { get; set; }
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

        // Categories for the select
        public List<CategoryDto> Categories { get; set; }
        // Used to show message to user according to outcome
        public List<ViewMessage> Messages { get; set; }
    }
}
