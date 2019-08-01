using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransIT.BLL;

namespace TransIT.API.Extensions
{
    public static class ConfigureApplicationExtension
    {
        public static void ConfigureApplication(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            services.ConfigureBLL(configuration, hostingEnvironment);
        }
    }
}