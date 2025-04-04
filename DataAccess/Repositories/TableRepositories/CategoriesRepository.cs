using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories.TableRepositories
{
    public class CategoriesRepository : GenericRepository
    {
        private readonly FlowerInventoryAssessmentContext _context;
        public CategoriesRepository(FlowerInventoryAssessmentContext context)
        {
            _context = context;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _context.Categories.FindAsync(id);
        }
        public async Task<List<Category>> GetCategoriesAsync()
        {
            return await _context.Categories.ToListAsync();
        }
    }
}
