using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using Xunit;

namespace TransIT.Tests
{
    public class EmployeeRepositoryTests
    {
        [Fact]
        public async Task Employee_Repository_Should_Add_Employee()
        {
            var context = CreateDbContext();
            var repository = new EmployeeRepository(context);
            var expectedEntity = new Employee(new Post() { Name = "Big Boss" , Id = 5 })
            {
                FirstName = "Vitalii",
                LastName = "Maksymiv",
                ShortName = "lv420",
                BoardNumber = 228,
                Id = 20
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            Assert.Equal(expectedEntity, actualEntity);
        }

        [Fact]
        public async Task Employee_Repository_Should_Get_All()
        {
            var context = CreateDbContext();
            var repository = new EmployeeRepository(context);
            var expectedEntity = new Employee(new Post() { Name = "Big Boss", Id = 5 })
            {
                FirstName = "Vitalii",
                LastName = "Maksymiv",
                ShortName = "lv420",
                BoardNumber = 228,
                Id = 20
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var entities = await repository.GetAllAsync();

            Assert.Single(entities.ToList());
        }

        private TransITDBContext CreateDbContext()
        {
            return new TransITDBContext(
                new DbContextOptionsBuilder<TransITDBContext>()
                   .UseInMemoryDatabase(Guid.NewGuid().ToString())
                   .EnableSensitiveDataLogging()
                   .Options
            );
        }
    }
}
