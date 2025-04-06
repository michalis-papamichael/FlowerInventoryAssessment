using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.TableRepositories
{
    //// 18:37 04/04
    // if time use EntityEntry for better check of records
    public class FlowersRepository : GenericRepository
    {
        private readonly FlowerInventoryAssessmentContext _context;
        public FlowersRepository(FlowerInventoryAssessmentContext context)
        {
            _context = context;
        }
        public async Task<Flower?> GetFlowerByIdAsync(int id, string? include = null)
        {
            if (!string.IsNullOrEmpty(include))
            {
                return await _context.Flowers.Include(include).FirstOrDefaultAsync(x => x.Id == id);
            }
            return await _context.Flowers.FindAsync(id);
        }
        public async Task<Flower?> GetFlowerByNameAndCategoryIdAsync(string name, int categoryId, string? include = null)
        {
            return await _context.Flowers.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.CategoryId == categoryId);
        }
        public async Task<List<Flower>> GetFlowersWithPagingAsync(int skip, int take, string? include = null)
        {
            if (!string.IsNullOrEmpty(include))
            {
                return await _context.Flowers.Include(include).Skip(skip).Take(take).ToListAsync();
            }
            return await _context.Flowers.Skip(skip).Take(take).ToListAsync();
        }
        public async Task<int> CountFlowers()
        {
            return await _context.Flowers.CountAsync();
        }
        public async Task CreateFlowerAsync(Flower flower)
        {
            await _context.Flowers.AddAsync(flower);
        }
        public void UpdateFlower(Flower updatedFlwoer)
        {
            updatedFlwoer.LastUpdateTimestamp = DateTime.Now;
            _context.Flowers.Update(updatedFlwoer);
        }
        public async Task<Flower?> DeleteFlowerByIdAsync(int id,string? include = null)
        {
            Flower? flower = await GetFlowerByIdAsync(id, include);
            if (flower != null)
            {
                flower.IsActive = false;
                flower.DeletionTimestamp = DateTime.Now;

                _context.Flowers.Update(flower);
            }
            return flower;
        }
    }
}
