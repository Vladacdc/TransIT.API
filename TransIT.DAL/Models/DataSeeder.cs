using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using TransIT.DAL.Models.Entities;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace TransIT.DAL.Models
{
    public static class DataSeeder
    {
        public static async Task SeedEssentialAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration)
        {
            services.GetRequiredService<TransITDBContext>().Database.Migrate();

            await app.SeedRolesAsync(services);
            await app.SeedAdminAsync(services,configuration);
            app.SeedStates(services);
        }

        public static async Task SeedAdditionalAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration)
        {
            await app.SeeUsersAsync(services, configuration);
        }

        public static async Task SeedRolesAsync(
            this IApplicationBuilder app,
            IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            await CreateIfNotExsits(roleManager, new Role() { Name = "ADMIN", TransName = "Адмін" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "WORKER", TransName = "Працівник" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ENGINEER", TransName = "Інженер" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "REGISTER", TransName = "Реєстратор" });
            await CreateIfNotExsits(roleManager, new Role() { Name = "ANALYST", TransName = "Аналітик" });
        }

        public static async Task SeedAdminAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration)
        {
            await app.SeedUserAsync(services, configuration, "Admin", "ADMIN");
        }

        public static void SeedStates(
            this IApplicationBuilder app,
            IServiceProvider services)
        {
            var context = services.GetRequiredService<TransITDBContext>();
            if (!context.State.Any())
            {
                context.State.AddRange(
                    new State { Name = "NEW", TransName = "Нова" },
                    new State { Name = "VERIFIED", TransName = "Верифіковано" },
                    new State { Name = "REJECTED", TransName = "Відхилено" },
                    new State { Name = "TODO", TransName = "До виконання" },
                    new State { Name = "EXECUTING", TransName = "В роботі" },
                    new State { Name = "DONE", TransName = "Готово" },
                    new State { Name = "CONFIRMED", TransName = "Підтверджено" },
                    new State { Name = "UNCONFIRMED", TransName = "Не підтверджено" });
            }
        }

        public static async Task SeeUsersAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration)
        {
            await app.SeedAdminAsync(services,configuration);

            await app.SeedUserAsync(services, configuration, "Register", "REGISTER");
            await app.SeedUserAsync(services, configuration, "Engineer", "ENGINEER");
            await app.SeedUserAsync(services, configuration, "Analyst", "ANALYST");
        }

        public static void SeedLocation(this IApplicationBuilder app, IServiceProvider services)
        {
            
        }

        private static async Task SeedUserAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration,
            string jsonRecord,
            string role)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            string username = configuration["Users:"+jsonRecord+":UserName"];
            string password = configuration["Users:" + jsonRecord + ":Password"];
            string firstname = configuration["Users:" + jsonRecord + ":FirstName"];
            string middlename = configuration["Users:" + jsonRecord + ":MiddleName"];
            string lastname = configuration["Users:" + jsonRecord + ":LastName"];
            if (await userManager.FindByNameAsync(username) == null)
            {
                var user = new User()
                {
                    UserName = username,
                    FirstName = firstname,
                    MiddleName = middlename,
                    LastName = lastname
                };

                IdentityResult result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }

        private static async Task CreateIfNotExsits(
            RoleManager<Role> roleManager,
            Role role)
        {
            if (await roleManager.FindByNameAsync(role.Name) == null)
            {
                await roleManager.CreateAsync(role);
            }
        }
    }
}
