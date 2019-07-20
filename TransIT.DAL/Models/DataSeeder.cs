using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace TransIT.DAL.Models
{
    public static class DataSeeder
    {
        public static async Task SeedRolesAsync(this IApplicationBuilder app)
        {
            var roleManager = app.ApplicationServices.GetRequiredService<RoleManager<Role>>();
            await CreateIfNotExsits(roleManager, new Role() { Name = "ADMIN", TransName = "Адмін" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "WORKER", TransName = "Працівник" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ENGINEER", TransName = "Інженер" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "REGISTER", TransName = "Реєстратор" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ANALYST", TransName = "Аналітик" });
        }
        private static async Task CreateIfNotExsits(RoleManager<Role> roleManager, Role role)
        {
            if(await roleManager.FindByNameAsync(role.Name) == null)
            {
               await roleManager.CreateAsync(role);
            }
        }
    }
}
