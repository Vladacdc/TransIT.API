using Microsoft.Extensions.DependencyInjection;

namespace TransIT.API.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options => options
                .AddPolicy("CorsPolicy", x => x
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
                    .WithOrigins("https://transitclient.azurewebsites.net")
                    .WithOrigins("https://transitapi.azurewebsites.net")));
        }
    }
}
