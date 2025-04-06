using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public async Task<Flower?> GetFlowerByNameAndCategoryIdAsync(string name, int categoryId, bool isActive, string? include = null)
        {
            return await _context.Flowers.FirstOrDefaultAsync(x => x.Name.ToLower() == name.ToLower() && x.CategoryId == categoryId && x.IsActive == isActive);
        }
        public List<Flower> GetFlowersWithPaging(Func<Flower, bool> wherePred, int skip, int take, bool isDesc,string orderProp, string? search, string? include = null)
        {
            var set = _context.Flowers;
            IEnumerable<Flower> query;
            if (!string.IsNullOrEmpty(include))
            {
                query = set.Include(include).Where(wherePred);
            }
            else
            {
                query = set.Where(wherePred);
            }
            PropertyDescriptor? prop = TypeDescriptor.GetProperties(typeof(Flower)).Find(orderProp, false);
            if (isDesc)
            {
                query = query.OrderByDescending(x => prop.GetValue(x));
            }
            else
            {
                query = query.OrderBy(x => prop.GetValue(x));
            }
            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(x => x.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                                || x.Category.Name.Contains(search, StringComparison.CurrentCultureIgnoreCase)
                                || x.Price.ToString().Contains(search, StringComparison.CurrentCultureIgnoreCase)
                                || x.TotalInventory.ToString().Contains(search, StringComparison.CurrentCultureIgnoreCase))
                    .ToList();

            }
            return query.Skip(skip).Take(take).ToList();
        }
        public int CountFlowers(Func<Flower,bool> wherePred)
        {
            return _context.Flowers.Where(wherePred).Count();
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
