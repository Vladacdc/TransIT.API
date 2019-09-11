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
                    .WithMethods("GET", "POST", "PUT", "DELETE")
                    .WithOrigins("https://localhost")));
        }
    }
}
