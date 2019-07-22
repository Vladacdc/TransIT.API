using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using TransIT.DAL.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


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
