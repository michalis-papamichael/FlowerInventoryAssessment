using DataAccess.Repositories;
using Domain.Models;
using ServiceLayer.Helpers;
using ServiceLayer.ServiceDtos.Flowers;
using ServiceLayer.ServiceResponder;

namespace ServiceLayer.Services
{
    public class FlowersServices
    {
        private readonly Repository _context;
        public FlowersServices(Repository context)
        {
            _context = context;
        }
        /// <summary>
        /// Gets te flowers by id if exists
        /// </summary>
        /// <param name="id">Flower Id</param>
        /// <returns></returns>
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
                        TotalInventory = flower.TotalInventory,
                        CategoryName = flower.Category.Name,
                        ImageUri = flower.ImageUri,
                        IsActive = flower.IsActive,
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
                response.Message = nameof(GetFlowerByIdAsync);
                response.Exception = ex;
            }
            return response;
        }
        /// <summary>
        /// Create the flower by passing the flower dto, searches if flower exists with the same name & category if not performs the operation
        /// </summary>
        /// <param name="dto">Create Flower dto object</param>
        /// <returns></returns>
        public async Task<ServiceResponse<SFlowerDto>> CreateFlower(SCreateFlowerDto dto, string? physicalPath = null)
        {
            ServiceResponse<SFlowerDto> response = new();
            try
            {
                Flower? flower = await _context.Flowers.GetFlowerByNameAndCategoryIdAsync(dto.Name, dto.CategoryId, isActive: true, "Category");
                if (flower == null)
                {
                    Category? category = await _context.Categories.GetCategoryByIdAsync(dto.CategoryId);
                    if (category != null)
                    {
                        string? imageUri = await ImageHelpers.TryStoreImage(dto.ImageFormFile, physicalPath);
                        Flower newFlower = new()
                        {
                            Timestamp = DateTime.Now,
                            Name = dto.Name,
                            CategoryId = dto.CategoryId,
                            Description = dto.Description,
                            Price = dto.Price,
                            TotalInventory = dto.TotalInventory,
                            ImageUri = imageUri,
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
                            TotalInventory = newFlower.TotalInventory,
                            IsActive = newFlower.IsActive,
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
                response.Message = nameof(CreateFlower);
                response.Exception = ex;
            }
            return response;
        }
        /// <summary>
        /// Get flowers' page by skipping <c>skip</c> amount and taking <c>take</c> amount.
        /// </summary>
        /// <param name="skip">Amount of data to skip</param>
        /// <param name="take">Amount of data to take</param>
        /// <returns></returns>
        public ServiceResponse<SFlowersPagingDto> GetFlowersWithPaging(int skip, int take, bool isDesc, string orderProp, string? search)
        {
            ServiceResponse<SFlowersPagingDto> response = new();
            try
            {
                SFlowersPagingDto flowersPaging = new();

                List<SFlowerDto> flowers = _context.Flowers.GetFlowersWithPaging(x => x.IsActive == true, skip, take, isDesc, orderProp, search, include: "Category")
                    .Select(x => new SFlowerDto()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Description = x.Description,
                        CategoryId = x.CategoryId,
                        TotalInventory = x.TotalInventory,
                        CategoryName = x.Category.Name,
                        ImageUri = x.ImageUri,
                        IsActive = x.IsActive,
                    }).ToList();

                flowersPaging.Flowers = flowers;
                flowersPaging.TotalFlowers = _context.Flowers.CountFlowers(x => x.IsActive == true);

                response.Data = flowersPaging;
                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = nameof(GetFlowersWithPaging);
                response.Exception = ex;
            }
            return response;
        }
        /// <summary>
        /// Edit the flower by passing the flower dto with updated data, searches with id and if exists performs the edit operation.
        /// </summary>
        /// <param name="dto">Edit Flower dto object</param>
        /// <returns></returns>
        public async Task<ServiceResponse<SFlowerDto>> EditFlower(SEditFlowerDto dto, string? physicalPath = null)
        {
            ServiceResponse<SFlowerDto> response = new();
            try
            {
                Flower? flower = await _context.Flowers.GetFlowerByIdAsync(dto.Id, "Category");
                if (flower != null)
                {
                    string? imageUri = await ImageHelpers.TryStoreImage(dto.ImageFormFile, physicalPath);
                    flower.Name = dto.Name;
                    flower.Description = dto.Description;
                    flower.Price = dto.Price;
                    flower.TotalInventory = dto.TotalInventory;
                    flower.CategoryId = dto.CategoryId;
                    flower.ImageUri = imageUri;
                    _context.Flowers.UpdateFlower(flower);
                    await _context.SaveChangesAsync();

                    response.Data = new SFlowerDto()
                    {
                        Id = flower.Id,
                        Name = flower.Name,
                        Price = flower.Price,
                        Description = flower.Description,
                        CategoryId = flower.CategoryId,
                        CategoryName = flower.Category.Name,
                        TotalInventory = flower.TotalInventory,
                        ImageUri = flower.ImageUri,
                        IsActive = flower.IsActive,
                    };
                    response.Success = true;
                    response.Message = "Editted";
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
                response.Message = nameof(EditFlower);
                response.Exception = ex;
            }
            return response;
        }
        /// <summary>
        /// Delete flower by searching by id & if exists, deletes it (softly).
        /// </summary>
        /// <param name="id">Flower Id</param>
        /// <returns></returns>
        public async Task<ServiceResponse<SFlowerDto>> DeleteFlowerById(int id)
        {
            ServiceResponse<SFlowerDto> response = new();
            try
            {
                Flower? flower = await _context.Flowers.DeleteFlowerByIdAsync(id, "Category");
                if (flower != null)
                {
                    await _context.SaveChangesAsync();

                    response.Data = new SFlowerDto()
                    {
                        Id = flower.Id,
                        Name = flower.Name,
                        Price = flower.Price,
                        Description = flower.Description,
                        CategoryId = flower.CategoryId,
                        CategoryName = flower.Category.Name,
                        IsActive = flower.IsActive,
                    };
                    response.Success = true;
                    response.Message = "Deleted";
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
                response.Message = nameof(DeleteFlowerById);
                response.Exception = ex;
            }
            return response;
        }
    }
}
