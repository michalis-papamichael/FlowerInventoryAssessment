using DataAccess.Repositories.TableRepositories;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public class Repository
    {
        private readonly FlowerInventoryAssessmentContext _context;
        public Repository(FlowerInventoryAssessmentContext context)
        {
            _context = context;
            Flowers = new FlowersRepository(_context);
            Categories = new CategoriesRepository(_context);
        }
        public FlowersRepository Flowers { get; private set; }
        public CategoriesRepository Categories { get; private set; }
        #region Base methods
        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
        #endregion
    }
}
