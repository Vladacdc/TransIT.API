using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using TransIT.DAL.Models;

namespace TransIT.API.Extensions
{
    public static class SeedExtension
    {
        public static void Seed(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration,
            IHostingEnvironment env)
        {
            app.SeedEssentialAsync(services, configuration).Wait();
            if (env.IsDevelopment())
            {
                app.SeedAdditionalAsync(services, configuration).Wait();
            }
        }
    }
}
