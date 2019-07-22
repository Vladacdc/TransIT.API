using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using TransIT.DAL.Models;
using Microsoft.Extensions.Configuration;

namespace TransIT.API.Extensions
{
    public static class SeedExtension
    {
        public static void Seed(this IApplicationBuilder app, IServiceProvider services,IConfiguration configuration)
        {
            app.SeedEssentialAsync(services,configuration).Wait();
            app.SeedAdditionalAsync(services, configuration).Wait();
        }
    }
}
