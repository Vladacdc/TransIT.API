using AutoMapper;
using Microsoft.AspNetCore.Identity.Test;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TransIT.BLL.DTOs;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;
using TransIT.Tests.Helper;
using Xunit;

namespace TransIT.Tests.ServiceTests
{
    public class EmployeeServiceTests : IClassFixture<MapperFixture>
    {
        private readonly string _someUserId = Guid.NewGuid().ToString();

        private Employee _sampleEmployee;
        private Employee _someOtherEmployee;
        private User _sampleUser;
        private Mock<IUnitOfWork> _unitOfWork;
        private readonly IMapper _mapper;

        public EmployeeServiceTests(MapperFixture fixture)
        {
            _mapper = fixture.Mapper;
        }


        [Fact]
        public async Task EmployeeService_Should_Attach_A_User()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            await employees.CreateAsync(_mapper.Map<EmployeeDTO>(_sampleEmployee));
            var actual = await employees.AttachUserAsync(_sampleEmployee.Id, _someUserId);
            // Assert
            Assert.NotNull(actual);
            Assert.NotNull(actual.AttachedUser);
        }


        [Fact]
        public async Task EmployeeService_Should_Not_Attach_Non_Existing_Employee()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            var actual = await employees.AttachUserAsync(1, _someUserId);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task EmployeeService_Should_Not_Attach_Already_Attached_User()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            await employees.CreateAsync(_mapper.Map<EmployeeDTO>(_sampleEmployee));
            await employees.CreateAsync(_mapper.Map<EmployeeDTO>(_someOtherEmployee));
            await employees.AttachUserAsync(_sampleEmployee.Id, _someUserId);
            var actual = await employees.AttachUserAsync(_someOtherEmployee.Id, _someUserId);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task EmployeeService_Should_Return_Empty_If_Non_Existing_User()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            await employees.CreateAsync(_mapper.Map<EmployeeDTO>(_sampleEmployee));
            var actual = await employees.AttachUserAsync(_sampleEmployee.Id, Guid.NewGuid().ToString());
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task EmployeeService_Should_Remove_Attached_User()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            var dto = _mapper.Map<EmployeeDTO>(_sampleEmployee);
            // Act
            await employees.CreateAsync(dto);
            await employees.AttachUserAsync(_sampleEmployee.Id, _someUserId);
            var actual = await employees.RemoveUserAsync(_sampleEmployee.Id);
            // Assert
            Assert.NotNull(dto);
            Assert.Null(actual.AttachedUser);
        }

        [Fact]
        public async Task EmployeeService_RemoveUser_Should_Ignore_If_Non_Existing_Employee()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            await employees.AttachUserAsync(1, _someUserId);
            var actual = await employees.RemoveUserAsync(1);
            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task EmployeeService_Should_Get_Employee_Which_User_Was_Attached_To()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateUnitOfWork();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            EmployeeDTO expected = _mapper.Map<EmployeeDTO>(_sampleEmployee);
            await employees.CreateAsync(expected);
            await employees.AttachUserAsync(_sampleEmployee.Id, _someUserId);
            var actual = await employees.GetEmployeeForUserAsync(_someUserId);
            // Assert
            Assert.Equal(expected, actual, new EmployeeComparer());
        }

        [Fact]
        public async Task EmployeeService_Should_Get_Not_Attached_Users()
        {
            // Arrange
            InitializeConstants();
            var fake = CreateWithMockedUsers();
            var employees = new EmployeeService(fake.Object, _mapper);
            // Act
            await employees.CreateAsync(_mapper.Map<EmployeeDTO>(_sampleEmployee));
            await employees.AttachUserAsync(_sampleEmployee.Id, _someUserId);
            var actual = await employees.GetNotAttachedUsersAsync();
            // Assert
            Assert.Equal(2, actual.Count);
        }


        private void InitializeConstants()
        {
            _sampleEmployee = new Employee()
            {
                Post = new Post() { Name = "Big Boss", Id = 5 },
                FirstName = "Vitalii",
                LastName = "Maksymiv",
                ShortName = "lv420",
                BoardNumber = 228,
                Id = 20
            };
            _someOtherEmployee = new Employee()
            {
                Post = new Post() { Name = "Soft serve Director", Id = 4 },
                FirstName = "Sviatoslav",
                LastName = "Bachynskyi",
                ShortName = "lv420",
                BoardNumber = 128,
                Id = 4
            };
            _sampleUser = new User()
            {
                UserName = "bbbbc",
                IsActive = true,
                Id = _someUserId
            };
        }

        private Mock<IUnitOfWork> CreateWithMockedUsers()
        {
            var context = TestSetUpHelper.CreateDbContext();

            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.UserManager)
               .Returns(() =>
               {
                   var mock = MockHelpers.MockUserManager<User>();
                   mock.Setup(m => m.Users)
                       .Returns(() => context.Users.AsQueryable());
                   mock.Setup(m => m.FindByIdAsync(It.IsNotNull<string>()))
                       .Returns((string id) => context.Users.FindAsync(id));
                   return mock.Object;
               });

            context.Users.AddRange(
                new User[]
                {
                    _sampleUser,
                    new User()
                    {
                        UserName = "Sviatoslav",
                        IsActive = true,
                        Id = Guid.NewGuid().ToString()
                    },
                    new User()
                    {
                        UserName = "VladAcDC",
                        IsActive = true,
                        Id = Guid.NewGuid().ToString()
                    }
                });
            context.SaveChanges();

            SetupEmployeeRepository(context);

            return _unitOfWork;
        }

        private Mock<IUnitOfWork> CreateUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.UserManager)
               .Returns(() =>
               {
                   var mock = MockHelpers.MockUserManager<User>();
                   mock.Setup(m => m.FindByIdAsync(It.IsNotNull<string>()))
                       .Returns((string id) =>
                       {
                           return id == _someUserId
                               ? Task.FromResult(_sampleUser)
                               : Task.FromResult<User>(null);
                       });
                   return mock.Object;
               });

            SetupEmployeeRepository(TestSetUpHelper.CreateDbContext());

            return _unitOfWork;
        }

        private void SetupEmployeeRepository(TransITDBContext context)
        {
            var employeeRepository = new EmployeeRepository(context);

            _unitOfWork.Setup(u => u.SaveAsync()).Returns(() => context.SaveChangesAsync());
            _unitOfWork.Setup(u => u.EmployeeRepository).Returns(() => employeeRepository);
        }

        private class EmployeeComparer : IEqualityComparer<EmployeeDTO>
        {
            public bool Equals(EmployeeDTO x, EmployeeDTO y)
            {
                return x.Id == y.Id &&
                    x.FirstName == y.FirstName &&
                    x.LastName == y.LastName &&
                    x.ShortName == y.ShortName &&
                    x.BoardNumber == y.BoardNumber;
            }

            public int GetHashCode(EmployeeDTO obj)
            {
                throw new NotImplementedException();
            }
        }
    }
}
