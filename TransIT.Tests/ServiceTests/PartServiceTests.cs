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
    public class PartServiceTests : IClassFixture<MapperFixture>
    {
        private readonly IMapper _mapper;

        private Mock<IUnitOfWork> _unitOfWork;

        public PartServiceTests(MapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task PartService_Should_Get_Null_When_Part_Not_Exist()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new PartService(_unitOfWork.Object, _mapper);

            // Act
            var entity = await service.GetAsync((new Random()).Next());

            // Assert
            Assert.Null(entity);
        }

        [Fact]
        public async Task PartService_Should_Create_Part_Async()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new PartService(_unitOfWork.Object, _mapper);
            var expectedEntity = new PartDTO()
            {
                Name = "TestName",
                Code = "12345",
            };

            // Act
            var actualEntity = await service.CreateAsync(expectedEntity);
            await _unitOfWork.Object.SaveAsync();

            // Assert
            Assert.NotNull(actualEntity);
            Assert.Equal(expectedEntity.Name, actualEntity.Name);
            Assert.Equal(expectedEntity.Code, actualEntity.Code);
        }

        [Fact]
        public async Task PartService_Should_Update_Part()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new PartService(_unitOfWork.Object, _mapper);
            var entity = new PartDTO()
            {
                Name = "TestName",
                Code = "12345",
            };

            var newEntity = new PartDTO()
            {
                Name = "NewTestName",
                Code = "12345678",
            };

            await service.CreateAsync(entity);
            await _unitOfWork.Object.SaveAsync();

            // Act
            entity.Name = newEntity.Name;
            entity.Code = newEntity.Code;
            await service.UpdateAsync(entity);
            await _unitOfWork.Object.SaveAsync();

            // Assert
            Assert.NotNull(entity);
            Assert.Equal(newEntity.Name, entity.Name);
            Assert.Equal(newEntity.Code, entity.Code);
        }

        [Fact]
        public async Task PartService_Should_Delete_Part_Async()
        {
            // Arrange
            SetUpUnitOfWork();
            var service = new PartService(_unitOfWork.Object, _mapper);
            var entity = new PartDTO()
            {
                Name = "TestName",
                Code = "12345"
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
            SetUpPartRepository(TestSetUpHelper.CreateDbContext());
        }

        private void SetUpPartRepository(TransITDBContext context)
        {
            var partRepository = new PartRepository(context);

            _unitOfWork.Setup(u => u.SaveAsync()).Returns(() => context.SaveChangesAsync());
            _unitOfWork.Setup(u => u.PartRepository).Returns(() => partRepository);
        }
    }
}