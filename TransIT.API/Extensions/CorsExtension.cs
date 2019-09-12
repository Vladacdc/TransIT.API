using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TransIT.API.Extensions
{
    public static class CorsExtension
    {
        public static void ConfigureCors(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options => options
                .AddPolicy("CorsPolicy", builder =>
                {
                    builder.AllowAnyHeader()
                           .AllowCredentials()
                           .WithMethods("GET", "POST", "PUT", "DELETE");
                    var adresses = configuration.GetSection("CORS").GetChildren();
                    foreach (var address in adresses)
                    {
                        builder.WithOrigins(address.Value);
                    }
                }));
        }
    }
}
