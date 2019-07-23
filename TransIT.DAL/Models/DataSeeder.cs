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
            await app.SeedAdminAsync(services, configuration);
            app.SeedStates(services);
        }

        public static async Task SeedAdditionalAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration)
        {
            await app.SeeUsersAsync(services, configuration);
            app.SeedData(services);
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
            await app.SeedAdminAsync(services, configuration);

            await app.SeedUserAsync(services, configuration, "Register", "REGISTER");
            await app.SeedUserAsync(services, configuration, "Engineer", "ENGINEER");
            await app.SeedUserAsync(services, configuration, "Analyst", "ANALYST");
        }

        public static void SeedData(this IApplicationBuilder app, IServiceProvider services)
        {
            var context = services.GetRequiredService<TransITDBContext>();

            #region Locations
            var LKP1 = new Location() { Name = "LKP  \"Lvivelektrotrans\"",
                Description = "Lviv, Troleibusna Street, 1"
            };
            var LKP2 = new Location() { Name = "LKP \"Lvivelektrotrans\"",
                Description = "Lviv, Sakharova St, 2"
            };
            var LK = new Location() { Name = "LK ATP-1",
                Description = "Lviv, Aviatsiina St, 1" };
            if (!context.Location.Any())
            {
                context.Location.AddRange(LKP1, LKP2, LK);
            }
            #endregion

            #region VechileTypes
            var A185 = new VehicleType(){ Name = "Bus A185" };
            var E191 = new VehicleType() { Name = "Electrobus E191" };
            var T3L = new VehicleType() { Name = "Tram T3L" };
            var T191 = new VehicleType() { Name = "Trolely T191" };
            if (!context.VehicleType.Any())
            {
                context.VehicleType.AddRange(A185,E191, T3L,T191);
            }
            #endregion

            #region Vehicles
            var vehicle1 = new Vehicle()
            {
                VehicleType = A185,
                Brand = "Electron",
                Vincode = "WR0DA76963U153381",
                InventoryId = "12314",
                RegNum = "AC4131CC",
                Model = "S10",
                Location = LKP1,
                WarrantyEndDate = new DateTime(2019, 12, 10),
                CommissioningDate = new DateTime(2017, 12, 10)
            };
            var vehicle2 = new Vehicle()
            {
                VehicleType = E191,
                Brand = "Bohdan",
                Vincode = "WP0CA36863U153382",
                InventoryId = "124",
                RegNum = "LV1234VL",
                Model = "S2",
                WarrantyEndDate     = new DateTime(2019, 05, 09),
                CommissioningDate = new DateTime(2017, 05, 22)
            };
            if (!context.Vehicle.Any())
            {
                context.Vehicle.AddRange(vehicle1, vehicle2);
            }
            #endregion

            #region Posts
            var engineer = new Post { Name = "Engineer" };
            var boss = new Post { Name = "Boss" };
            var locksmith = new Post { Name = "Locksmith" };
            if (!context.Post.Any())
            {
                context.Post.AddRange(engineer, boss, locksmith);
            }
            #endregion

            #region Employees
            var Ihora = new Employee()
            {
                FirstName = "Ihor",
                MiddleName = "Oleksandrovych",
                LastName = "Babiak",
                ShortName = "Ihora",
                BoardNumber = 1,
                Post = boss
            };
            var Yura = new Employee()
            {
                FirstName = "Yurii",
                MiddleName = "Vasylovych",
                LastName = "Medved",
                ShortName = "Yurik",
                BoardNumber = 5,
                Post = locksmith
            };
            var Sania = new Employee()
            {
                FirstName = "Oleksandr",
                MiddleName = "Borysovych",
                LastName = "Vodzianskyi",
                ShortName = "Sania",
                BoardNumber = 2,
                Post = engineer
            };
            if (!context.Employee.Any())
            {
                context.Employee.AddRange(Ihora, Yura, Sania);
            }
            #endregion

            #region Countries
            var Ukraine = new Country() { Name = "Ukraine" };
            var Turkey = new Country() { Name = "Turkey" };
            var Russia = new Country() { Name = "Russia" };
            var Polland = new Country() { Name = "Polland" };
            var German = new Country() { Name = "German" };
            if (!context.Country.Any())
            {
                context.AddRange(Ukraine, Turkey, Russia, Polland, German);
            }
            #endregion

            #region Currencies
            var USD = new Currency { ShortName = "USD", FullName = "Dollar" };
            var UAH = new Currency { ShortName = "UAH", FullName = "Hryvnia" };
            var RUB = new Currency { ShortName = "RUB", FullName = "Ruble" };
            var GBR = new Currency { ShortName = "GBR", FullName = "Great British Pound" };
            var EUR = new Currency { ShortName = "EUR", FullName = "Euro" };

            if (!context.Country.Any())
            {
                context.AddRange(USD,UAH,RUB,GBR,EUR);
            }
            #endregion

            context.SaveChanges();
        }


        private static async Task SeedUserAsync(
            this IApplicationBuilder app,
            IServiceProvider services,
            IConfiguration configuration,
            string jsonRecord,
            string role)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            string username = configuration["Users:" + jsonRecord + ":UserName"];
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
