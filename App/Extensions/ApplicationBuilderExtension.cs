using App.Seeders;
using Domain.Models;
using Serilog;

namespace App.Extensions
{
    public static class ApplicationBuilderExtension
    {
        public static void ConfigureGlobalLogger(this IApplicationBuilder app)
        {
            app.Use(async (context, next) =>
            {
                try
                {
                    await next(context);
                    if (context.Response.StatusCode >= 400)
                    {
                        Log.Warning($"{context.Request.Method}: {context.Request.Path} - Code: {context.Response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Log.Error($"Exception on {context.Request.Method}: {context.Request.Path}", ex);
                }
            });
        }
        public static void ConfigureSeeder(this WebApplication app)
        {
            using (IServiceScope scope = app.Services.CreateScope())
            {
                FlowerInventoryAssessmentContext? context = scope.ServiceProvider.GetService<FlowerInventoryAssessmentContext>();
                if (context == null)
                {
                    throw new Exception("Unable to get service context");
                }
                DataSeeder seeder = new DataSeeder(context);
                seeder.Seed();
            }
        }
    }
}
