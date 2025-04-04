using DataAccess.Repositories;
using Domain.Models;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Serilog;
using ServiceLayer.Services;

namespace App.Extensions
{
    public static class ServicesExtensions
    {
        public static void ConfigureDatabase(this IServiceCollection services)
        {
            string connection = Environment.GetEnvironmentVariable("FlowerInventoryAssessment:connection", EnvironmentVariableTarget.Machine);
            services.AddDbContext<FlowerInventoryAssessmentContext>(options => options.UseSqlServer(connection));
        }
        public static void ConfigureRepository(this IServiceCollection services)
        {
            services.AddTransient<Repository>();
        }
        public static void ConfigureServiceLayer(this IServiceCollection services)
        {
            services.AddTransient<FlowersServices>();
            services.AddTransient<CategoriesServices>();
        }
        public static void ConfigureLogger(this IServiceCollection services, IConfiguration configuration)
        {
            try
            {
                string connection = Environment.GetEnvironmentVariable("FlowerInventoryAssessmentLogs:connection", EnvironmentVariableTarget.Machine);
                configuration["Serilog:WriteTo:0:Args:connectionString"] = connection;
                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration, readerOptions: new Serilog.Settings.Configuration.ConfigurationReaderOptions()
                    {
                        SectionName = "Serilog"
                    })
                    .Enrich.FromLogContext()
                    .CreateLogger();
            }
            catch (Exception ex)
            {
                // sent email for immediate action (restart/reconfigure)
            }
        }
    }
}
