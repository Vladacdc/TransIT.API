using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Comparers;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.ImplementedServices;
using Xunit;

namespace TransIT.Tests.ServiceTests
{
    public class UserServiceTests : IClassFixture<UnitOfWorkFixture>, IClassFixture<MapperFixture>
    {
        private readonly UnitOfWorkFixture _fixture;
        private readonly MapperFixture _mapperFixture;

        public UserServiceTests(UnitOfWorkFixture unitOfWorkFixture, MapperFixture mapperFixture)
        {
            _fixture = unitOfWorkFixture;
            _mapperFixture = mapperFixture;
        }

        [Fact]
        public async Task UserService_Should_Get_Single_User()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            // Act
            var result = await userService.CreateAsync(new TestUser());
            // Assert
            Assert.Equal(new TestUser(), result, new UserComparer());
        }

        [Fact]
        public async Task UserService_Should_Get_Range_Users()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            var users = new TestUser[] {
                new TestUser { Email = "aaaaa@aa.c" },
                new TestUser
                { Email = "Bohdan@fff.d", Role = new RoleDTO
                    {
                    Name = "ENGINEER" , TransName = "Інженер"}
                },
                new TestUser { Email = "pbbbc@gmail.com" }
            };
            // Act
            foreach (UserDTO user in users)
            {
                await userService.CreateAsync(user);
            }

            var list = await userService.GetRangeAsync(0, 100);
            // Assert
            Assert.True(list.OrderBy(u => u.Email).SequenceEqual(users, new UserComparer()));
        }

        [Fact]
        public async Task UserService_Should_Update_Password()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            var value = new TestUser
            {
                Email = "shewchenkoandriy@gmail.com",
                Password = "AbagfgA122@2"
            };
            // Act
            var actual = await userService.UpdatePasswordAsync(
                await userService.CreateAsync(value), "HelloWorld123@"
            );
            // Assert
            Assert.NotNull(actual);
        }

        [Fact]
        public async Task UserService_Should_Get_Assignees()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            var assigned = new List<UserDTO>
            {
                new TestUser
                { Email = "Bohdan@fff.d", Role = new RoleDTO
                    {
                    Name = "WORKER" , TransName = "Працівник"}
                },
                new TestUser
                { Email = "DanyloDudokLoh@fff.d", Role = new RoleDTO
                    {
                    Name = "WORKER" , TransName = "Працівник"}
                }
            };
            var users = new List<UserDTO> {
                new TestUser { Email = "aaaaa@aa.c" },
                new TestUser { Email = "pbbbc@gmail.com" }
            };
            users.AddRange(assigned);

            // Act
            foreach (UserDTO user in users)
            {
                await userService.CreateAsync(user);
            }

            var list = await userService.GetAssignees(0, 100);
            // Assert
            Assert.True(list.OrderBy(u => u.Email).SequenceEqual(assigned, new UserComparer()));
        }

        [Fact]
        public async Task UserService_Should_Update_User()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            // Act
            var result = await userService.CreateAsync(new TestUser());
            result.Email = "arsendomanich228@gmail.com";
            result.FirstName = "Arsen";
            result.LastName = "Domanich";
            result.UserName = "arsenchik";
            var updated = await userService.UpdateAsync(result);
            // Assert
            Assert.Equal(result, updated, new UserComparer());
        }

        [Fact]
        public async Task UserService_Should_Create_User()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            // Act
            var result = await userService.CreateAsync(new TestUser());
            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Role);
            Assert.NotEqual(result.Id, default(Guid).ToString());
        }

        [Fact]
        public async Task UserService_Should_Delete_User()
        {
            // Arrange
            var unitOfWork = _fixture.CreateMockUnitOfWork();
            var userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            // Act
            var result = await userService.CreateAsync(new TestUser());
            await userService.DeleteAsync(result.Id);
            // Assert
            Assert.Null(await userService.GetAsync(result.Id));
        }
    }
}
