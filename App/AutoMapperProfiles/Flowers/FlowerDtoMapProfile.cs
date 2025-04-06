using App.Dtos.Flowers;
using AutoMapper;
using ServiceLayer.ServiceDtos.Flowers;

namespace App.AutoMapperProfiles.Flowers
{
    public class FlowerDtoMapProfile : Profile
    {
        public FlowerDtoMapProfile()
        {
            CreateMap<SFlowerDto, FlowerDto>();
        }
    }
}
