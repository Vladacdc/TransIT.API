using System.Linq;
using System.Threading.Tasks;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.Tests.Helper;
using Xunit;

namespace TransIT.Tests.Repository
{
    public class WorkTypeRepositoryTests
    {
        [Fact]
        public async Task Work_Type_Repository_Should_Add_Work_Type()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new WorkTypeRepository(context);
            var expectedEntity = new WorkType()
            {
                Id = 40,
                Name = "TestName",
                EstimatedTime = 1,
                EstimatedCost = 100
            };

            // Act
            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();
            var actualEntity = await repository.GetByIdAsync(expectedEntity.Id);

            // Assert
            Assert.Equal(expectedEntity, actualEntity);
        }

        [Fact]
        public async Task Work_Type_Repository_Should_Get_All_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new WorkTypeRepository(context);
            var expectedEntity = new WorkType()
            {
                Id = 40,
                Name = "TestName",
                EstimatedTime = 1,
                EstimatedCost = 100
            };

            await repository.AddAsync(expectedEntity);
            await context.SaveChangesAsync();

            // Act
            var entities = await repository.GetAllAsync();

            // Assert
            Assert.Single(entities.ToList());
        }

        [Fact]
        public async Task Work_Type_Repository_Should_Remove_Work_Type_Async()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new WorkTypeRepository(context);
            var expectedEntity = new WorkType()
            {
                Id = 40,
                Name = "TestName",
                EstimatedTime = 1,
                EstimatedCost = 100
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
        public async Task Work_Type_Repository_Should_Update_Work_Type()
        {
            // Arrange
            var context = TestSetUpHelper.CreateDbContext();
            var repository = new WorkTypeRepository(context);
            var oldUnit = new WorkType()
            {
                Id = 40,
                Name = "TestName",
                EstimatedTime = 1,
                EstimatedCost = 100
            };

            var newUnit = new WorkType()
            {
                Id = 40,
                Name = "TestName",
                EstimatedTime = 1.5,
                EstimatedCost = 150
            };

            await repository.AddAsync(oldUnit);
            await context.SaveChangesAsync();

            // Act
            oldUnit.Name = newUnit.Name;
            oldUnit.EstimatedCost = newUnit.EstimatedCost;
            oldUnit.EstimatedTime = newUnit.EstimatedTime;
            repository.Update(oldUnit);
            await context.SaveChangesAsync();

            // Assert
            Assert.Equal(newUnit.Name, oldUnit.Name);
            Assert.Equal(newUnit.EstimatedCost, oldUnit.EstimatedCost);
            Assert.Equal(newUnit.EstimatedTime, oldUnit.EstimatedTime);
        }
    }
}
