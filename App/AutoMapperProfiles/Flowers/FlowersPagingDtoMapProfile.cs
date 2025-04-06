using App.Dtos.Flowers;
using AutoMapper;
using ServiceLayer.ServiceDtos.Flowers;

namespace App.AutoMapperProfiles.Flowers
{
    public class FlowersPagingDtoMapProfile : Profile
    {
        public FlowersPagingDtoMapProfile()
        {
            CreateMap<SFlowersPagingDto, FlowersPagingDto>();
        }
    }
}
