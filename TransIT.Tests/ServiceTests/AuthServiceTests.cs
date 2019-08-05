using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Test;
using Moq;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.UnitOfWork;
using Xunit;

namespace TransIT.Tests
{
    public class AuthServiceTests
    {
        private readonly TokenDTO _testToken = new BLL.DTOs.TokenDTO()
        {
            AccessToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dpbiI6InRlc3RBZG1pbiIsInJvbGUiOiJBRE1JTiIsInN1YiI6Ijk3NmY4NTQyLWNlZjItNDRhOS1iY2NkLWRlNzE0NDhhNzkwNyIsImp0aSI6ImIyZjBmNmRhLTY0ZDMtNDZhMi1hYTZmLTk4NGNlZTZjMTc4ZSIsImlhdCI6MTU2NDU4MzYyMSwibmJmIjoxNTY0NTgzNjIwLCJleHAiOjE1NjQ1ODcyMjAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.paX4-N6OqFpM_gCeRlOoQGUTVoJT00mOttC6ubDlMOQ",
            RefreshToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJsb2dpbiI6InRlc3RBZG1pbiIsInJvbGUiOiJBRE1JTiIsInN1YiI6Ijk3NmY4NTQyLWNlZjItNDRhOS1iY2NkLWRlNzE0NDhhNzkwNyIsImp0aSI6ImE4ODBjYzA1LTJjYjYtNGMwZS04OTEyLTgzNjIxZjZjYmM1OSIsImlhdCI6MTU2NDU4MzYyMSwibmJmIjoxNTY0NTgzNjIwLCJleHAiOjE1NjUxODg0MjAsImlzcyI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCIsImF1ZCI6Imh0dHA6Ly9sb2NhbGhvc3Q6NTAwMCJ9.SKASB4jrHS0_whmhmHKPFnYxgVJEyCHtQx2NaR-9lGU"
        };

        private Mock<IUnitOfWork> _unitOfWork;
        private Mock<IJwtFactory> _factory;

        public AuthServiceTests()
        {
            InitializeJwtFactory();
            InitializeUnitOfWork();
        }

        [Fact]
        public async Task AuthService_Should_Authorize_With_Valid_Userdata()
        {
            // Arrange
            var service = new AuthenticationService(
                MockHelpers.MockILogger<AuthenticationService>().Object,
                _factory.Object,
                _unitOfWork.Object
            );

            // Act
            var result = await service.SignInAsync(
                new BLL.DTOs.LoginDTO()
                {
                    Login = "bbbbc",
                    Password = "HelloWord123@"
                });

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task AuthService_Should_Not_Authorize_With_Invalid_Userdata()
        {
            // Arrange
            var service = new AuthenticationService(
                MockHelpers.MockILogger<AuthenticationService>().Object,
                _factory.Object,
                _unitOfWork.Object
            );

            // Act
            var result = await service.SignInAsync(
                new BLL.DTOs.LoginDTO()
                {
                    Login = "bbbbc",
                    Password = "HellsssssoWord123@"
                });

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task AuthService_Should_Refresh_Token()
        {
            // Arrange
            var service = new AuthenticationService(
                MockHelpers.MockILogger<AuthenticationService>().Object,
                _factory.Object,
                _unitOfWork.Object
            );

            // Act
            var result = await service.SignInAsync(
                new BLL.DTOs.LoginDTO()
                {
                    Login = "bbbbc",
                    Password = "HelloWord123@"
                });

            var refreshResult = await service.TokenAsync(result);

            // Assert
            Assert.NotNull(refreshResult);
        }

        private void InitializeJwtFactory()
        {
            _factory = new Mock<IJwtFactory>();
            _factory.Setup(f => f.GenerateToken(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                .Returns((string id, string name, string role) =>
                {
                    return name == "bbbbc" && role == "ADMIN"
                        ? _testToken
                        : null;
                });
            _factory.Setup(f => f.GetPrincipalFromExpiredToken(It.IsNotNull<string>()))
                .Returns(() =>
                {
                    return ValueTuple.Create(
                        new ClaimsPrincipal(new List<ClaimsIdentity>() { }),
                        new JwtSecurityToken(_testToken.RefreshToken)
                    );
                });
        }

        private void InitializeUnitOfWork()
        {
            _unitOfWork = new Mock<IUnitOfWork>();
            _unitOfWork.Setup(u => u.UserManager)
                .Returns(() =>
                {
                    var mock = MockHelpers.MockUserManager<User>();
                    mock.Setup(m => m.FindByNameAsync(It.IsNotNull<string>()))
                        .Returns((string name) =>
                        {
                            if (name == "bbbbc")
                            {
                                return Task.FromResult(new User()
                                {
                                    UserName = "bbbbc",
                                    IsActive = true,
                                    Id = Guid.NewGuid().ToString()
                                });
                            }
                            else
                            {
                                return Task.FromResult<User>(null);
                            }
                        });
                    mock.Setup(m => m.FindByIdAsync(It.IsNotNull<string>()))
                        .Returns((string id) =>
                        {
                            return Guid.TryParse(id, out Guid result)
                                ? Task.FromResult(new User()
                                {
                                    UserName = "bbbbc",
                                    IsActive = true,
                                    Id = id
                                })
                                : null;
                        });
                    mock.Setup(m => m.CheckPasswordAsync(It.IsNotNull<User>(), It.IsNotNull<string>()))
                        .Returns((User user, string password) =>
                        {
                            return Task.FromResult(
                                user.UserName == "bbbbc" && password == "HelloWord123@"
                            );
                        });
                    mock.Setup(m => m.GetRolesAsync(It.IsNotNull<User>()))
                        .Returns(() => Task.FromResult(
                            (IList<string>)new List<string>()
                            {
                                "ADMIN"
                            }));
                    return mock.Object;
                });
        }
    }
}
