using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.Comparers;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.Tests
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
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());

            Assert.Equal(new TestUser(), result, new UserComparer());
        }

        [Fact]
        public async Task UserService_Should_Get_Range_Users()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            TestUser[] users = {
                new TestUser { Email = "aaaaa@aa.c" },
                new TestUser
                { Email = "Bohdan@fff.d", Role = new RoleDTO
                    {
                    Name = "ENGINEER" , TransName = "Інженер"}
                },
                new TestUser { Email = "pbbbc@gmail.com" }
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
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            TestUser value = new TestUser
            {
                Email = "shewchenkoandriy@gmail.com",
                Password = "AbagfgA122@2"
            };
            UserDTO result = await userService.CreateAsync(value);
            Assert.NotNull(
                await userService.UpdatePasswordAsync(result, "HelloWorld123@")
            );
        }

        [Fact]
        public async Task UserService_Should_Get_Assignees()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            List<UserDTO> assigned = new List<UserDTO>
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
            List<UserDTO> users = new List<UserDTO> {
                new TestUser { Email = "aaaaa@aa.c" },
                new TestUser { Email = "pbbbc@gmail.com" }
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
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
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
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());
            Assert.NotNull(result);
            Assert.NotNull(result.Role);
            Assert.NotEqual(result.Id, default(Guid).ToString());
        }

        [Fact]
        public async Task UserService_Should_Delete_User()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
            UserService userService = new UserService(_mapperFixture.Mapper, unitOfWork);
            UserDTO result = await userService.CreateAsync(new TestUser());
            await userService.DeleteAsync(result.Id);
            Assert.Null(await userService.GetAsync(result.Id));
        }
    }
}
