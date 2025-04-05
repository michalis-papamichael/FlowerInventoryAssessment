using Azure;
using DataAccess.Repositories;
using Domain.Models;
using ServiceLayer.ServiceDtos.Flowers;
using ServiceLayer.ServiceResponder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer.Services
{
    public class FlowersServices
    {
        private readonly Repository _context;
        public FlowersServices(Repository context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<SFlowerDto>> GetFlowerByIdAsync(int id)
        {
            ServiceResponse<SFlowerDto> response = new();
            try
            {
                Flower? flower = await _context.Flowers.GetFlowerByIdAsync(id, include: "Category");
                if (flower != null)
                {
                    response.Data = new SFlowerDto()
                    {
                        Id = flower.Id,
                        Name = flower.Name,
                        Price = flower.Price,
                        Description = flower.Description,
                        CategoryId = flower.CategoryId,
                        CategoryName = flower.Category.Name,
                    };
                    response.Success = true;
                    response.Message = "Ok";
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "DoesNotExist";
                    response.ErrorMessages.Add("Flower does not exists");
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "GetFlowerByIdAsync";
                response.Exception = ex;
            }
            return response;
        }
        public async Task<ServiceResponse<SFlowerDto>> CreateFlower(SCreateFlowerDto dto)
        {
            ServiceResponse<SFlowerDto> response = new();
            try
            {
                Flower? flower = await _context.Flowers.GetFlowerByNameAndCategoryIdAsync(dto.Name, dto.CategoryId);
                if (flower == null)
                {
                    Category? category = await _context.Categories.GetCategoryByIdAsync(dto.CategoryId);
                    if (category != null)
                    {
                        Flower newFlower = new()
                        {
                            Timestamp = DateTime.Now,
                            Name = dto.Name,
                            CategoryId = dto.CategoryId,
                            Description = dto.Description,
                            Price = dto.Price,
                            IsActive = true,
                        };
                        await _context.Flowers.CreateFlowerAsync(newFlower);
                        await _context.SaveChangesAsync();

                        response.Data = new SFlowerDto()
                        {
                            Id = newFlower.Id,
                            Name = newFlower.Name,
                            Price = newFlower.Price,
                            Description = newFlower.Description,
                            CategoryId = newFlower.CategoryId,
                            CategoryName = category.Name,
                        };
                        response.Success = true;
                        response.Message = "Created";
                    }
                    else
                    {
                        response.Data = null;
                        response.Success = false;
                        response.Message = "DoesNotExist";
                        response.ErrorMessages.Add("Category does not exist");
                    }
                }
                else
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "AlreadyExist";
                    response.ErrorMessages.Add("Flower already exist");
                }
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "CreateFlower";
                response.Exception = ex;
            }
            return response;
        }
        public async Task<ServiceResponse<List<SFlowerDto>>> GetFlowersWithPaging(int skip, int take)
        {
            ServiceResponse<List<SFlowerDto>> response = new();
            try
            {
                List<SFlowerDto> flowers = (await _context.Flowers.GetFlowersWithPagingAsync(skip, take, include: "Category"))
                    .Select(x => new SFlowerDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        CategoryId = x.CategoryId,
                        CategoryName = x.Category.Name,
                    }).ToList();

                response.Data = flowers;
                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = "GetFlowersWithPaging";
                response.Exception = ex;
            }
            return response;
        }
    }
}
