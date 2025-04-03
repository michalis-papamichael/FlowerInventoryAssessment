using App.Extensions;
using App.Helpers;
using Domain.Models;
using App.Seeders;
namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var app = builder.Build();

#if DEBUG
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
#endif

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
