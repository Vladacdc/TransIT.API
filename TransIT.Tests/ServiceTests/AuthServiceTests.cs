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
using TransIT.BLL.Helpers;

namespace TransIT.Tests
{
    public class AuthServiceTests : IClassFixture<UnitOfWorkFixture>
    {
        private readonly UnitOfWorkFixture _fixture;

        public AuthServiceTests(UnitOfWorkFixture unitOfWorkFixture)
        {
            this._fixture = unitOfWorkFixture;
        }

        [Fact]
        public async Task AuthenticationService_Should_Sign_In_With_Valid_Data()
        {
            IUnitOfWork unitOfWork = _fixture.CreateMockUnitOfWork();
        }
    }
}