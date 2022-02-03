using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
 
#pragma warning disable 1591
namespace {{cookiecutter.project_spa_pkg}}.Middleware
{
    public static class CorsMiddleware
    {
        public static IApplicationBuilder AddCorsServices(this IApplicationBuilder app, IWebHostEnvironment envn)
        {
         
            // Must be called before UseMVC
            app.UseCors(builder => builder
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod());
            return app;
        }
    }
}
