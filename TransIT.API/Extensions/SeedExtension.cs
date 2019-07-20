using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using TransIT.DAL.Models;

namespace TransIT.API.Extensions
{
    public static class SeedExtension
    {
        public static void Seed(this IApplicationBuilder app)
        {
            app.SeedRolesAsync().Wait();
        }
    }
}
