using App.Dtos.Categories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceLayer.ServiceDtos.Categories;

namespace App.AutoMapperProfiles.Categories
{
    public class CategoryDtoMapProfile : Profile
    {
        public CategoryDtoMapProfile()
        {
            CreateMap<SCategoryDto, CategoryDto>();
        }
    }
}
