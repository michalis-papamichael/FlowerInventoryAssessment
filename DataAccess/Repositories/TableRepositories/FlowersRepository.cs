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
        public async Task<List<Flower>> GetFlowersWithPagingAsync(int skip, int take)
        {
            return await _context.Flowers.Skip(skip).Take(take).ToListAsync();
        }
        public void UpdateFlower(Flower updatedFlwoer)
        {
            updatedFlwoer.LastUpdateTimestamp = DateTime.Now;
            _context.Flowers.Update(updatedFlwoer);
        }
        public async Task DeleteFlowerByIdAsync(int id)
        {
            Flower? flower = await GetFlowerByIdAsync(id);
            if (flower != null)
            {
                flower.IsActive = false;
                flower.DeletionTimestamp = DateTime.Now;

                _context.Flowers.Update(flower);
            }
        }
    }
}
