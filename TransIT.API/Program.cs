using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace TransIT.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //var result2 = await _roleManager.CreateAsync(new Role() {Name = "ADMIN", CreatedById = "1", ModifiedById = "1", TransName = "Адмін"});
            //var result3 = await _roleManager.CreateAsync(new Role() { Name = "ENGINEER", CreatedById = "1", ModifiedById = "1" , TransName = "Інженер"});
            //var result4 = await _roleManager.CreateAsync(new Role() { Name = "WORKER", CreatedById = "1", ModifiedById = "1", TransName = "Працівник" });
            //var result5 = await _roleManager.CreateAsync(new Role() { Name = "REGISTER", CreatedById = "1", ModifiedById = "1", TransName = "Реєстратор" });
            //var result6 = await _roleManager.CreateAsync(new Role() { Name = "ANALYST", CreatedById = "1", ModifiedById = "1", TransName = "Aналітик" });
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.AddConsole();
                });
    }
}
