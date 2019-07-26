using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Threading.Tasks;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.Tests
{
    public class UserServiceTests : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public UserServiceTests(UnitOfWorkFixture unitOfWorkFixture)
        {
            this._fixture = unitOfWorkFixture;
        }

        [Fact]
        public async Task UserService_Should_Create_User()
        {
            IUnitOfWork unitOfWork = _fixture.MockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO user = new TestUser();
            UserDTO result = await userService.CreateAsync(user);
            Assert.NotNull(result);
            Assert.NotEqual(result.Id, default(Guid).ToString());
        }
    }
}
