using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.Tests.Helper;
using Xunit;

namespace TransIT.Tests.Repository
{
    public class PartRepositoryTests
    {
        [Fact]
        public async Task Part_Repository_Should_Add_Part()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartRepository(context);
            var expectedEntity = new Part()
            {
                Id = 4,
                Name = "TestName",
                Code = "123456",
                UnitId = 1,
                Unit = new Unit { Id=1, Name="Kilo", ShortName = "kg", },
                ManufacturerId = 1,
                Manufacturer = new Manufacturer { Id=1, Name="Vasylko", }
            };

            // Act
            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Equal(expectedEntity, actualEntity);
        }

        [Fact]
        public async Task Part_Repository_Should_Get_All_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartRepository(context);
            var expectedEntity = new Part()
            {
                Id = 4,
                Name = "TestName",
                Code = "123456",
                UnitId = 1,
                Unit = new Unit { Id = 1, Name = "Kilo", ShortName = "kg", },
                ManufacturerId = 1,
                Manufacturer = new Manufacturer { Id = 1, Name = "Vasylko", }
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();

            // Act
            var entities = await repository.GetAllAsync();

            // Assert
            Assert.Single(entities.ToList());
        }

        [Fact]
        public async Task Part_Repository_Should_Remove_Part_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartRepository(context);
            var expectedEntity = new Part()
            {
                Id = 4,
                Name = "TestName",
                Code = "123456",
                UnitId = 1,
                Unit = new Unit { Id = 1, Name = "Kilo", ShortName = "kg", },
                ManufacturerId = 1,
                Manufacturer = new Manufacturer { Id = 1, Name = "Vasylko", }
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
        public async Task Part_Repository_Should_Update_Part()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new PartRepository(context);
            var oldPart = new Part()
            {
                Id = 4,
                Name = "TestName",
                Code = "123456",
                UnitId = 1,
                Unit = new Unit { Id = 1, Name = "Kilo", ShortName = "kg", },
                ManufacturerId = 1,
                Manufacturer = new Manufacturer { Id = 1, Name = "Vasylko", }
            };

            var newPart = new Part()
            {
                Id = 4,
                Name = "NewTestName",
                Code = "123456678",
                UnitId = 1,
                Unit = new Unit { Id = 1, Name = "Kilo", ShortName = "kg", },
                ManufacturerId = 1,
                Manufacturer = new Manufacturer { Id = 1, Name = "Vasylko", }
            };

            await repository.AddAsync(oldPart);
            await context.SaveChangesAsync();

            // Act
            oldPart.Name = newPart.Name;
            oldPart.Code = newPart.Code;
            repository.Update(oldPart);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(newPart.Name, oldPart.Name);
            Assert.Equal(newPart.Code, oldPart.Code);
        }
    }
}