using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.Tests.Helper;
using Xunit;

namespace TransIT.Tests.Repository
{
    public class ManufacturerRepositoryTests
    {
        [Fact]
        public async Task Manufacturer_Repository_Should_Add_Manufacturer()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new ManufacturerRepository(context);
            var expectedEntity = new Manufacturer()
            {
                Id = 4,
                Name = "TestName",
            };

            // Act
            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Equal(expectedEntity, actualEntity);
        }

        [Fact]
        public async Task Manufacturer_Repository_Should_Get_All_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new ManufacturerRepository(context);
            var expectedEntity = new Manufacturer()
            {
                Id = 4,
                Name = "TestName",
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();

            // Act
            var entities = await repository.GetAllAsync();

            // Assert
            Assert.Single(entities.ToList());
        }

        [Fact]
        public async Task Manufacturer_Repository_Should_Remove_Manufacturer_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new ManufacturerRepository(context);
            var expectedEntity = new Manufacturer()
            {
                Id = 4,
                Name = "TestName",
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();

            // Act
            await repository.RemoveAsync(expectedEntity.Id);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Null(actualEntity);
        }

        [Fact]
        public async Task Manufacturer_Repository_Should_Update_Manufacturer()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new ManufacturerRepository(context);
            var oldManufacturer = new Manufacturer()
            {
                Id = 4,
                Name = "TestName",
            };

            var newManufacturer = new Manufacturer()
            {
                Id = 4,
                Name = "NewTestName",
            };

            await repository.AddAsync(oldManufacturer);
            await context.SaveChangesAsync();

            // Act
            oldManufacturer.Name = newManufacturer.Name;
            repository.Update(oldManufacturer);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(newManufacturer.Name, oldManufacturer.Name);
        }
    }
}