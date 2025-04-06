using App.Dtos.Statistics;
using AutoMapper;
using ServiceLayer.ServiceDtos.Statistics;

namespace App.AutoMapperProfiles.Statistics
{
    public class DashboardStatisticsMapProfile : Profile
    {
        public DashboardStatisticsMapProfile()
        {
            CreateMap<SDashboardStatisticsDto, DashboardStatisticsDto>();
        }
    }
}
