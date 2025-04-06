using DataAccess.Repositories;
using ServiceLayer.ServiceDtos.Statistics;
using ServiceLayer.ServiceResponder;

namespace ServiceLayer.Services
{
    public class StatisticsServices
    {
        private readonly Repository _context;
        public StatisticsServices(Repository context)
        {
            _context = context;
        }
        /// <summary>
        /// Get dashboard statistics
        /// </summary>
        /// <returns></returns>
        public ServiceResponse<SDashboardStatisticsDto> GetDashboardStatistics()
        {
            ServiceResponse<SDashboardStatisticsDto> response = new();
            try
            {
                response.Data = new SDashboardStatisticsDto()
                {
                    TotalFlowersInInventory = _context.Flowers.CountFlowers(x => x.IsActive == true),
                    TotalKindsOfFlowers = _context.Categories.CountCategories(x => x.IsActive == true),
                };
                response.Success = true;
                response.Message = "Ok";
            }
            catch (Exception ex)
            {
                response.Data = null;
                response.Success = false;
                response.Message = nameof(GetDashboardStatistics);
                response.Exception = ex;
            }
            return response;
        }
    }
}
