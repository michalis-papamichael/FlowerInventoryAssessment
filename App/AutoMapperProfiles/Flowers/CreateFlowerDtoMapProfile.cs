using App.Dtos.Flowers;
using AutoMapper;
using ServiceLayer.ServiceDtos.Flowers;

namespace App.AutoMapperProfiles.Flowers
{
    public class CreateFlowerDtoMapProfile : Profile
    {
        public CreateFlowerDtoMapProfile()
        {
            CreateMap<SCreateFlowerDto, CreateFlowerDto>();
            CreateMap<CreateFlowerDto, SCreateFlowerDto>();
        }
    }
}
