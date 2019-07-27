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
using System.Collections.Generic;

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
        public async Task UserService_Should_Get_Range_Users()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            TestUser[] users = new TestUser[] {
                new TestUser() { Email = "aaaaa@aa.c" },
                new TestUser() { Email = "Bohdan@fff.d", Role = new RoleDTO() {
                    Name = "ENGINEER" , TransName = "Інженер"}
                },
                new TestUser() { Email = "pbbbc@gmail.com" },
            };
            foreach (UserDTO user in users)
            {
                await userService.CreateAsync(user);
            }

            var list = await userService.GetRangeAsync(0, 100);

            Assert.True(list.OrderBy(u => u.Email).SequenceEqual(users, new UserComparer()));
        }

        [Fact]
        public async Task UserService_Should_Update_Password()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(
                new TestUser()
                {
                    Password = "AbagfgA122@2"
                });
            Assert.NotNull(
                await userService.UpdatePasswordAsync(result, result.Password, "HelloWorld123@")
            );
        }

        [Fact]
        public async Task UserService_Should_Get_Assignees()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_fixture.Mapper, unitOfWork);
            List<UserDTO> assigned = new List<UserDTO>()
            {
                new TestUser() { Email = "Bohdan@fff.d", Role = new RoleDTO() {
                    Name = "WORKER" , TransName = "Працівник"}
                },
                new TestUser() { Email = "DanyloDudokLoh@fff.d", Role = new RoleDTO() {
                    Name = "WORKER" , TransName = "Працівник"}
                }
            };
            List<UserDTO> users = new List<UserDTO> {
                new TestUser() { Email = "aaaaa@aa.c" },
                new TestUser() { Email = "pbbbc@gmail.com" },
            };
            users.AddRange(assigned);

            foreach (UserDTO user in users)
            {
                await userService.CreateAsync(user);
            }

            var list = await userService.GetAssignees(0, 100);

            Assert.True(list.OrderBy(u => u.Email).SequenceEqual(assigned, new UserComparer()));
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
