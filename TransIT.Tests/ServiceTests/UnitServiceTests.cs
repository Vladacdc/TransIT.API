using AutoMapper;
using Moq;
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