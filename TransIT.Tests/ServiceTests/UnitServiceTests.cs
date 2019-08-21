using AutoMapper;
using Moq;
using System;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;
using TransIT.Tests.Helper;
using Xunit;

namespace TransIT.Tests.ServiceTests
{
    public class UnitServiceTests : IClassFixture<MapperFixture>
    {
        private readonly IMapper _mapper;
        private Mock<IUnitOfWork> _unitOfWork;

        public UnitServiceTests(MapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task UnitService_Should_Get_Null_When_Unit_Not_Exist()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new UnitService(_unitOfWork.Object, _mapper);

            // Act
            var entity = await service.GetAsync((new Random()).Next());

            // Assert
            Assert.Null(entity);
        }

        [Fact]
        public async Task UnitService_Should_Create_Unit_Async()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new UnitService(_unitOfWork.Object, _mapper);
            var expectedEntity = new UnitDTO()
            {
                Name = "TestName",
                ShortName = "TestSN",
            };

            // Act
            var actualEntity = await service.CreateAsync(expectedEntity);
            await _unitOfWork.Object.SaveAsync();

            // Assert
            Assert.NotNull(actualEntity);
            Assert.Equal(expectedEntity.Name, actualEntity.Name);
            Assert.Equal(expectedEntity.ShortName, actualEntity.ShortName);
        }

        [Fact]
        public async Task UnitService_Should_Update_Unit()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new UnitService(_unitOfWork.Object, _mapper);
            var entity = new UnitDTO()
            {
                Name = "TestName",
                ShortName = "TestSN",
            };

            var newEntity = new UnitDTO()
            {
                Name = "NewTestName",
                ShortName = "NewTestSN",
            };

            await service.CreateAsync(entity);
            await _unitOfWork.Object.SaveAsync();

            // Act
            entity.Name = newEntity.Name;
            entity.ShortName = newEntity.ShortName;
            await service.UpdateAsync(entity);
            await _unitOfWork.Object.SaveAsync();

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(newEntity.Name, entity.Name);
            Assert.Equal(newEntity.ShortName, entity.ShortName);
        }

        [Fact]
        public async Task UnitService_Should_Delete_Unit_Async()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new UnitService(_unitOfWork.Object, _mapper);
            var entity = new UnitDTO()
            {
                Name = "TestName",
                ShortName = "TestSN",
            };

            entity = await service.CreateAsync(entity);
            await _unitOfWork.Object.SaveAsync();

            // Act
            await service.DeleteAsync(entity.Id);
            await _unitOfWork.Object.SaveAsync();

            // Assert
            Assert.Null(await service.GetAsync(entity.Id));
        }

        private void SetUpUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            SetUpUnitRepository(TestSetUpHelper.CreateDbContext());
        }

        private void SetUpUnitRepository(TransITDBContext context)
        {
            var unitRepository = new UnitRepository(context);

            _unitOfWork.Setup(u => u.SaveAsync()).Returns(() => context.SaveChangesAsync());
            _unitOfWork.Setup(u => u.UnitRepository).Returns(() => unitRepository);
        }
    }
}