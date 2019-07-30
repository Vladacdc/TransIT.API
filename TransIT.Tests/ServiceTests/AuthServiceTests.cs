using System.Threading.Tasks;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.Tests
{
    public class AuthServiceTests : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public AuthServiceTests(UnitOfWorkFixture unitOfWorkFixture)
        {
            _fixture = unitOfWorkFixture;
        }

        [Fact]
        public async Task AuthenticationService_Should_Sign_In_With_Valid_Data()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
        }
    }
}