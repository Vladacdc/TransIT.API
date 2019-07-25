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
            app.SeedStatesAndTransitions(services);
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

        public static void SeedStatesAndTransitions(
            this IApplicationBuilder app,
            IServiceProvider services)
        {
            var context = services.GetRequiredService<TransITDBContext>();

            #region States
            var @new = new State { Name = "NEW", TransName = "Нова", IsFixed = true };
            var executing = new State { Name = "EXECUTING", TransName = "В роботі", IsFixed = true };
            var rejected = new State { Name = "REJECTED", TransName = "Відхилено", IsFixed = true };
            var done = new State { Name = "DONE", TransName = "Готово", IsFixed = true };
            var verified = new State { Name = "VERIFIED", TransName = "Верифіковано", IsFixed = false };
            var todo = new State { Name = "TODO", TransName = "До виконання", IsFixed = false };
            var confirmed = new State { Name = "CONFIRMED", TransName = "Підтверджено", IsFixed = false };
            var unconfirmed = new State { Name = "UNCONFIRMED", TransName = "Не підтверджено", IsFixed = false };

            if (!context.State.Any())
            {
                context.State.AddRange(@new, executing, rejected, done,
                    verified, todo, confirmed, unconfirmed);
            }
            #endregion

            #region ActionTypes
            var setExecuting = new ActionType() { Name = "Виконувати", IsFixed = true };
            var reject = new ActionType() { Name = "Скасувати", IsFixed = true };
            var finish = new ActionType() { Name = "Завершити", IsFixed = true };
            if (!context.ActionType.Any())
            {
                context.ActionType.AddRange(reject, finish, setExecuting);
            }
            #endregion

            #region Transitions
            var tsetExecuting = new Transition { FromState = @new, ToState = executing, ActionType = setExecuting, IsFixed = true };
            var treject1 = new Transition { FromState = @new, ToState = rejected, ActionType = reject, IsFixed = true };
            var treject2 = new Transition { FromState = executing, ToState = rejected, ActionType = reject, IsFixed = true };
            var tfinish = new Transition { FromState = executing, ToState = done, ActionType = finish, IsFixed = true };
            if (!context.Transition.Any())
            {
                context.Transition.AddRange(tsetExecuting, treject1, treject2, tfinish);
            }
            #endregion

            context.SaveChanges();
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

            if (context.Location.Any()
                || context.VehicleType.Any()
                || context.Vehicle.Any()
                || context.Post.Any()
                || context.Employee.Any()
                || context.Country.Any()
                || context.Currency.Any()
                || context.Supplier.Any()
                || context.Malfunction.Any()
                || context.MalfunctionGroup.Any()
                || context.MalfunctionSubgroup.Any()
                || context.Issue.Any())
            {
                return;
            }

            #region Locations
            var LKP1 = new Location()
            {
                Name = "ЛКП \"Львівелектротранс\"",
                Description = "м. Львів вул. Тролейбусна 1"
            };
            var LKP2 = new Location()
            {
                Name = "ЛКП \"Львівелектротранс\"",
                Description = "м. Львів вул.Сахарова 2а"
            };
            var LK = new Location()
            {
                Name = "ЛК АТП-1",
                Description = "м. Львів вул. Авіаційна 1"
            };

            context.Location.AddRange(LKP1, LKP2, LK);
            #endregion

            #region VechileTypes
            var A185 = new VehicleType() { Name = "Автобус А185" };
            var E191 = new VehicleType() { Name = "Електробус Е191" };
            var T3L = new VehicleType() { Name = "Трамвай Т3L" };
            var T191 = new VehicleType() { Name = "Тролейбус Т191" };

            context.VehicleType.AddRange(A185, E191, T3L, T191);
            #endregion

            #region Vehicles
            var electron = new Vehicle()
            {
                VehicleType = A185,
                Brand = "Електрон",
                Vincode = "WR0DA76963U153381",
                InventoryId = "12314",
                RegNum = "AC4131CC",
                Model = "S10",
                Location = LKP1,
                WarrantyEndDate = new DateTime(2019, 12, 10),
                CommissioningDate = new DateTime(2017, 12, 10)
            };
            var electron2 = new Vehicle()
            {
                VehicleType = E191,
                Brand = "Електрон",
                Vincode = "WP0CA36863U153382",
                InventoryId = "124",
                RegNum = "LV1234VL",
                Model = "S2",
                WarrantyEndDate = new DateTime(2019, 05, 09),
                CommissioningDate = new DateTime(2017, 05, 22)
            };

            context.Vehicle.AddRange(electron, electron2);
            #endregion

            #region Posts
            var engineer = new Post { Name = "Провідний інженер" };
            var boss = new Post { Name = "Начальник дільниці" };
            var locksmith = new Post { Name = "Слюсар механоскладальних робіт" };

            context.Post.AddRange(engineer, boss, locksmith);
            #endregion

            #region Employees
            var Ihora = new Employee()
            {
                FirstName = "Ігор",
                MiddleName = "Олександрович",
                LastName = "Баб'як",
                ShortName = "Ігора",
                BoardNumber = 1,
                Post = boss
            };
            var Yura = new Employee()
            {
                FirstName = "Юрій",
                MiddleName = "Васильович",
                LastName = "Медведь",
                ShortName = "Yurik",
                BoardNumber = 5,
                Post = locksmith
            };
            var Sania = new Employee()
            {
                FirstName = "Олександр",
                MiddleName = "Борисович",
                LastName = "Водзянський",
                ShortName = "Саня",
                BoardNumber = 2,
                Post = engineer
            };

            context.Employee.AddRange(Ihora, Yura, Sania);
            #endregion

            #region Countries
            var Ukraine = new Country() { Name = "Україна" };
            var Turkey = new Country() { Name = "Туреччина" };
            var Russia = new Country() { Name = "Росія" };
            var Polland = new Country() { Name = "Польща" };
            var German = new Country() { Name = "Німеччина" };

            context.Country.AddRange(Ukraine, Turkey, Russia, Polland, German);
            #endregion

            #region Currencies
            var USD = new Currency { ShortName = "USD", FullName = "Долар" };
            var UAH = new Currency { ShortName = "UAH", FullName = "Гривня" };
            var RUB = new Currency { ShortName = "RUB", FullName = "Рубль" };
            var GBR = new Currency { ShortName = "GBR", FullName = "Great British Pound" };
            var EUR = new Currency { ShortName = "EUR", FullName = "Євро" };

            context.Currency.AddRange(USD, UAH, RUB, GBR, EUR);
            #endregion

            #region Supplier
            var Yunona = new Supplier
            {
                Name = "Юнона",
                FullName = "ТОВ \"Юнона\"",
                Country = Ukraine,
                Currency = UAH,
                Edrpou = "20841865"
            };
            var Tekhnos = new Supplier
            {
                Name = "Технос",
                FullName = "ООО \"Технос\"",
                Country = Russia,
                Currency = RUB,
                Edrpou = "00703"
            };
            var Elephant = new Supplier
            {
                Name = "Elephant",
                FullName = "Elephant",
                Country = German,
                Currency = EUR,
                Edrpou = "04909"
            };

            context.Supplier.AddRange(Yunona, Tekhnos, Elephant);
            #endregion

            #region MalfunctionsWithGroups
            var body = new MalfunctionGroup()
            {
                Name = "Кузов",
            };

            var windows = new MalfunctionSubgroup()
            {
                Name = "Скління",
                MalfunctionGroup = body
            };
            var brokenWindow = new Malfunction
            {
                Name = "Розбите вікно",
                MalfunctionSubgroup = windows
            };
            var waterLeak = new Malfunction
            {
                Name = "Вікно протікає",
                MalfunctionSubgroup = windows
            };

            var handrails = new MalfunctionSubgroup
            {
                Name = "Поручні",
                MalfunctionGroup = body
            };
            var missingHandrail = new Malfunction()
            {
                Name = "Зникли поручні",
                MalfunctionSubgroup = handrails
            };
            var brokenHandrail = new Malfunction()
            {
                Name = "Зламані поручні",
                MalfunctionSubgroup = handrails
            };

            var engine = new MalfunctionGroup()
            {
                Name = "Двигун"
            };

            var gasEngine = new MalfunctionSubgroup()
            {
                Name = "Карбюраторний двигун",
                MalfunctionGroup = engine
            };
            var tooMuchGas = new Malfunction()
            {
                Name = "Залиті Свічки",
                MalfunctionSubgroup = gasEngine
            };
            var noGas = new Malfunction()
            {
                Name = "Перебитий бензонасос",
                MalfunctionSubgroup = gasEngine
            };

            var dieselEngine = new MalfunctionSubgroup()
            {
                Name = "Дизельний двигун",
                MalfunctionGroup = engine
            };
            var brokenPump = new Malfunction()
            {
                Name = "Зламаний насос великого тиску",
                MalfunctionSubgroup = dieselEngine
            };

            context.MalfunctionGroup.AddRange(body, engine);

            context.MalfunctionSubgroup.AddRange(windows, handrails, gasEngine, dieselEngine);

            context.Malfunction.AddRange(
                brokenWindow, waterLeak,
                missingHandrail, brokenHandrail,
                tooMuchGas, noGas,
                brokenPump);
            #endregion

            #region Issues
            var userController = services.GetRequiredService<UserManager<User>>();
            var findRegister = userController.FindByNameAsync("testRegister");
            findRegister.Wait();
            var register = findRegister.Result;
            var imissingHandrail = new Issue()
            {
                Malfunction = missingHandrail,
                Vehicle = electron2,
                Date = DateTime.Now.Subtract(
                    new TimeSpan(days: 1,hours: 0, minutes: 0, seconds: 0)),
                Warranty = 0,
                Summary = "ппц шо робити",
                Priority = 1,
                Create = register,
                Number = 1,
            };
            var ibrokenHandrail = new Issue()
            {
                Malfunction = brokenHandrail,
                Vehicle = electron2,
                Date = DateTime.Now,
                Warranty = 0,
                Summary = "ппц, спочатку пропали поручні, а тепер їх зламали, шо робити?",
                Priority = 2,
                Create = register,
                Number = 2,
            };
            var iwaterLeak = new Issue()
            {
                Malfunction = waterLeak,
                Vehicle = electron,
                Date = DateTime.Now,
                Warranty = 1,
                Summary = "Третє вікно праворуч протікає під час дощу",
                Priority = 0,
                Create = register,
                Number = 3
            };

            var ibrokenWindows = new Issue()
            {
                Malfunction = brokenWindow,
                Vehicle = electron,
                Date = DateTime.Now,
                Warranty = 0,
                Summary = "розбили два вікна в салоні з лівого боку",
                Priority = 2,
                Number = 4,
            };
            var inoGas = new Issue()
            {
                Malfunction = noGas,
                Vehicle = electron,
                Date = DateTime.Now,
                Warranty = 1,
                Summary = "Автобус не заводиться, видно, що перебитий бензонасос",
                Priority = 2,
                Number = 5
            };

            context.Issue.AddRange(imissingHandrail, ibrokenHandrail, iwaterLeak,
                ibrokenWindows, inoGas);
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
