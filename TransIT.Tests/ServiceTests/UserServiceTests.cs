using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.DTOs;
using TransIT.DAL.Models;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;
using Xunit;
using TransIT.BLL.Comparers;

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
        public async Task UserService_Should_Get_Single_User()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());

            Assert.Equal(new TestUser(), result, new UserComparer());
        }

        [Fact]
        public async Task UserService_Should_Update_User()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());
            result.Email = "arsendomanich228@gmail.com";
            result.FirstName = "Arsen";
            result.LastName = "Domanich";
            result.UserName = "arsenchik";
            UserDTO updated = await userService.UpdateAsync(result);
            Assert.Equal(result, updated, new UserComparer());
        }

        [Fact]
        public async Task UserService_Should_Create_User()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());
            Assert.NotNull(result);
            Assert.NotNull(result.Role);
            Assert.NotEqual(result.Id, default(Guid).ToString());
        }

        [Fact]
        public async Task UserService_Should_Delete_User()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());
            await userService.DeleteAsync(result.Id);
            Assert.Null(await userService.GetAsync(result.Id));
        }
    }
}
