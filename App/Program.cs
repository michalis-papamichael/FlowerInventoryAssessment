using App.Extensions;
using App.Helpers;
using App.Statics;
using Microsoft.Extensions.FileProviders;
namespace App
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            bool isTesting = builder.Configuration.GetValue<bool>("IsTesting");
            Scripts.ConfigureEnvViaPowershell(isTesting);

            // Add services to the container.
            builder.Services.ConfigureAutoMapper();
            builder.Services.AddMvc().AddRazorRuntimeCompilation();
            builder.Services.ConfigureDatabase();
            builder.Services.ConfigureRepository();
            builder.Services.ConfigureServiceLayer();
            builder.Services.ConfigureLogger(builder.Configuration);
            var app = builder.Build();

#if DEBUG
            app.ConfigureSeeder();
#endif

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.ConfigureGlobalLogger();

            app.UseHttpsRedirection();

            if (!Directory.Exists(AppPaths.FLOWERS_PHYSICAL_STORAGE))
            {
                Directory.CreateDirectory(AppPaths.FLOWERS_PHYSICAL_STORAGE);
            }
            app.UseStaticFiles();
            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(AppPaths.FLOWERS_PHYSICAL_STORAGE),
                RequestPath = AppPaths.STATIC_FILES
            });

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
