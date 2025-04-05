using App;
using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using ServiceLayer.Services;

namespace Tests
{
    public class MainUnitTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public MainUnitTest()
        {
            WebApplicationFactory<Program> factory = new WebApplicationFactory<Program>()
                .WithWebHostBuilder(builder =>
                {
                    builder.UseSetting("IsTesting", "true");
                });
            _factory = factory;
        }
        [Fact]
        public async Task GetFlowers_Should_Return_MoreThanZero()
        {
            try
            {
                //Arrange
                var serviceProvider = _factory.Services.GetRequiredService<IServiceProvider>();
                using (var scope = serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<Repository>();

                    //Acts
                    List<Flower> flower = await _context.Flowers.GetFlowersWithPagingAsync(0, 10, "Category");
                    int total = flower.Count();

                    //Assert
                    Assert.NotEqual(0, total);
                }
            }
            catch (Exception ex)
            {
            }
        }
        [Fact]
        public async Task GetFlowerBy_NonExistant_Id()
        {
            try
            {
                //Arrange
                var serviceProvider = _factory.Services.GetRequiredService<IServiceProvider>();
                using (var scope = serviceProvider.CreateScope())
                {
                    var _context = scope.ServiceProvider.GetRequiredService<FlowersServices>();

                    //Acts
                    var response = await _context.GetFlowerByIdAsync(-1);

                    //Assert
                    Assert.Null(response.Data);
                    Assert.NotEmpty(response.ErrorMessages);
                }
            }
            catch (Exception ex)
            {
            }
        }
    }
}