using App.Dtos.Flowers;
using AutoMapper;
using ServiceLayer.ServiceDtos.Flowers;

namespace App.AutoMapperProfiles.Flowers
{
    public class EditFlowerDtoMapProfile : Profile
    {
        public EditFlowerDtoMapProfile()
        {
            CreateMap<SEditFlowerDto, EditFlowerDto>();
            CreateMap<EditFlowerDto, SEditFlowerDto>();
        }
    }
}
