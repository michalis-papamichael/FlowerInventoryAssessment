﻿using DataAccess.Repositories;
using ServiceLayer.ServiceDtos.Categories;
using ServiceLayer.ServiceResponder;

namespace ServiceLayer.Services
{
    public class CategoriesServices
    {
        private readonly Repository _context;
        public CategoriesServices(Repository context)
        {
            _context = context;
        }
        /// <summary>
        /// Get list of flower categories
        /// </summary>
        /// <returns></returns>
        public async Task<ServiceResponse<List<SCategoryDto>>> GetCategories()
        {
            ServiceResponse<List<SCategoryDto>> response = new();
            try
            {
                List<SCategoryDto> categories = (await _context.Categories
                    .GetCategoriesAsync())
                    .Select(x => new SCategoryDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                    })
                    .ToList();

                response.Data = categories;
                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "GetCategories";
                response.Exception = ex;
            }
            return response;
        }
    }
}
