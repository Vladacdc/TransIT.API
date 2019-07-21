using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace TransIT.DAL.Models
{
    public static class DataSeeder
    {
        public static async Task SeedEssentialAsync(this IApplicationBuilder app, IServiceProvider services) {
            await app.SeedRolesAsync(services);
            app.SeedStates(services);
        }
        public static async Task SeedRolesAsync(this IApplicationBuilder app, IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            await CreateIfNotExsits(roleManager, new Role() { Name = "ADMIN", TransName = "Адмін" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "WORKER", TransName = "Працівник" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ENGINEER", TransName = "Інженер" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "REGISTER", TransName = "Реєстратор" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ANALYST", TransName = "Аналітик" });
        }
        public static void SeedStates(this IApplicationBuilder app, IServiceProvider services)
        {
            var context = services.GetRequiredService<TransITDBContext>();
            if (!context.State.Any())
            {
                context.State.AddRange(
                    new State { Id = 1, Name = "NEW", TransName = "Нова" },
                    new State { Id = 2, Name = "VERIFIED", TransName = "Верифіковано" },
                    new State { Id = 3, Name = "REJECTED", TransName = "Відхилено" },
                    new State { Id = 4, Name = "TODO", TransName = "До виконання" },
                    new State { Id = 5, Name = "EXECUTING", TransName = "В роботі" },
                    new State { Id = 6, Name = "DONE", TransName = "Готово" },
                    new State { Id = 7, Name = "CONFIRMED", TransName = "Підтверджено" },
                    new State { Id = 8, Name = "UNCONFIRMED", TransName = "Не підтверджено" });
            }
        }
        private static async Task CreateIfNotExsits(RoleManager<Role> roleManager, Role role)
        {
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}
