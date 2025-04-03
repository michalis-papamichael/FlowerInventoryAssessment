using Domain.Models;

namespace App.Seeders
{
    public class DataSeeder
    {
        private readonly FlowerInventoryAssessmentContext _context;
        public DataSeeder(FlowerInventoryAssessmentContext context)
        {
            _context = context;
        }
        public void Seed()
        {
            if (!_context.Categories.Any())
            {
                var categories = new List<Category>()
                {
                    new Category()
                    {
                        Id = 1,
                        Name = "Annuals",
                        Description = "Annual plants complete their life cycle lifecycle within a single year—from germination, flowering, and seed production, to eventual death. They’re perfect for those who want to see quick results and incorporate varied colors and textures into their gardens every year. Some gardens can benefit from two successions of annuals in the same season, maximizing blooming.",
                        Timestamp = DateTime.Now,
                    },
                    new Category()
                    {
                        Id = 2,
                        Name = "Bulbs",
                        Description = "Bulbous plants, including tubers and corms, form a unique category. They store energy in an underground bulb and flower every year. Spring-flowering bulbs are planted in the autumn and need a cold spell to bloom, whereas autumn-flowering bulbs are planted in the spring. They are essential for continuous flowering from early spring to late autumn.",
                        Timestamp = DateTime.Now,
                    },
                    new Category()
                    {
                        Id = 3,
                        Name = "Perennials",
                        Description = "Perennials come back every year, surviving winters and resuming growth in spring. Their ability to bloom for several years makes them an economical, long-lasting choice for any garden. They require less replacement and offer permanent structure to the landscape.",
                        Timestamp = DateTime.Now,
                    }
                };
                _context.Categories.AddRange(categories);
                _context.SaveChanges();
            }

            if (!_context.Flowers.Any())
            {
                var flowers = new List<Flower>()
                {
                    new Flower()
                    {
                        Name= "Sweet Pea",
                        Description= "This charming annual is a vining flower that blooms in profusion in cooler weather. Some varieties are sweetly scented, too, making them a lovely addition to the garden.",
                        CategoryId=1,
                        Timestamp=DateTime.Now,
                        Price=2.5m,
                    },
                    new Flower()
                    {
                        Name = "Sweet Alyssum",
                        Description = "This honey-scented low-growing annual should be in every garden! It comes in white, pink and pale purple and looks amazing tumbling over walls or spilling out of window boxes.",
                        CategoryId = 1,
                        Timestamp = DateTime.Now,
                        Price = 2.8m,
                    },
                    new Flower()
                    {
                        Name = "Ammi",
                        Description = "Reminiscent of the roadside wildflower Queen Anne's lace, these delicate flowers make a spectacular addition to cut bouquets. They grow easily from seed and will self-seed for future years.",
                        CategoryId = 1,
                        Timestamp = DateTime.Now,
                        Price = 3.00m,
                    },
                    new Flower()
                    {
                        Name = "Allium",
                        Description = "Allium is a large genus of monocotyledonous flowering plants with around 1000 accepted species, making Allium the largest genus in the family Amaryllidaceae and amongst the largest plant genera in the world.",
                        CategoryId = 2,
                        Timestamp = DateTime.Now,
                        Price = 3.10m,
                    },
                    new Flower()
                    {
                        Name = "Anemone",
                        Description = "Anemone is a genus of flowering plants in the buttercup family Ranunculaceae. Plants of the genus are commonly called windflowers. They are native to the temperate and subtropical regions of all regions except Australia, New Zealand, and Antarctica.",
                        CategoryId = 2,
                        Timestamp = DateTime.Now,
                        Price = 2.95m,
                    },
                    new Flower()
                    {
                        Name = "Clematis",
                        Description = "Clematis is a popular perennial climbing plant defined by its vibrant petals that are purple, pink or blue. The vining plant, which looks beautiful winded along a fence, trellis or pergola, blooms twice a year — once during early summer and once again late summer or late fall.",
                        CategoryId = 3,
                        Timestamp = DateTime.Now,
                        Price = 4.20m,
                    }
                };
                _context.Flowers.AddRange(flowers);
                _context.SaveChanges();
            }
        }
    }
}
