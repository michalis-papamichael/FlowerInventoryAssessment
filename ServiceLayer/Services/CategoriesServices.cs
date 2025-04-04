using DataAccess.Repositories;
using ServiceLayer.ServiceDtos.Categories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class CategoriesServices
    {
        private readonly Repository _context;
        public CategoriesServices(Repository context)
        {
            _context = context;
        }
    }
}
