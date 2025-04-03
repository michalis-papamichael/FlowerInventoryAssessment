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
    }
}
