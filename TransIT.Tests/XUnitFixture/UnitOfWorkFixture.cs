using System;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.EntityFrameworkCore;
using TransIT.BLL.Mappings;
using TransIT.DAL.Models;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.Tests
{
    /// <summary>
    /// A fixture containing all dependencies for services.
    /// </summary>
    public class UnitOfWorkFixture : IDisposable
    {
        private class UserStore : UserStore<User, Role, TransITDBContext, string, IdentityUserClaim<string>,
            UserRole, IdentityUserLogin<string>, IdentityUserToken<string>, IdentityRoleClaim<string>>
        {
            public UserStore(TransITDBContext context, IdentityErrorDescriber describer = null) : base(context, describer)
            {
            }
        }

        private class RoleStore : RoleStore<Role, TransITDBContext, string, UserRole, IdentityRoleClaim<string>>
        {
            public RoleStore(TransITDBContext context, IdentityErrorDescriber describer = null) : base(context, describer)
            {
            }
        }

        /// <summary>
        /// Automapper configuration
        /// </summary>
        public IMapper Mapper { get; } = new MapperConfiguration(c =>
        {
            c.AddProfile(new RoleProfile());
            c.AddProfile(new UserProfile());
            c.AddProfile(new VehicleTypeProfile());
            c.AddProfile(new VehicleProfile());
            c.AddProfile(new RoleProfile());
            c.AddProfile(new MalfunctionGroupProfile());
            c.AddProfile(new MalfunctionSubgroupProfile());
            c.AddProfile(new MalfunctionProfile());
            c.AddProfile(new StateProfile());
            c.AddProfile(new ActionTypeProfile());
            c.AddProfile(new IssueProfile());
            c.AddProfile(new IssueLogProfile());
            c.AddProfile(new DocumentProfile());
            c.AddProfile(new SupplierProfile());
            c.AddProfile(new CurrencyProfile());
            c.AddProfile(new CountryProfile());
            c.AddProfile(new PostProfile());
            c.AddProfile(new EmployeeProfile());
            c.AddProfile(new TransitionProfile());
            c.AddProfile(new LocationProfile());
        }).CreateMapper();

        /// <summary>
        /// Creates a <see cref="UnitOfWork"/> instance.
        /// </summary>
        /// <returns>A new object each time.</returns>
        public IUnitOfWork CreateMockUnitOfWork()
        {
            TransITDBContext transITDBContext = new TransITDBContext(
                new DbContextOptionsBuilder<TransITDBContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .EnableSensitiveDataLogging()
                    .Options
            );

            IUserStore<User> userStore = new UserStore(transITDBContext);
            IRoleStore<Role> roleStore = new RoleStore(transITDBContext);

            IUnitOfWork unitOfWork = new UnitOfWork(
                transITDBContext,
                new ActionTypeRepository(transITDBContext),
                new CountryRepository(transITDBContext),
                new CurrencyRepository(transITDBContext),
                new BillRepository(transITDBContext),
                new DocumentRepository(transITDBContext),
                new IssueRepository(transITDBContext),
                new IssueLogRepository(transITDBContext),
                new MalfunctionRepository(transITDBContext),
                new MalfunctionGroupRepository(transITDBContext),
                new MalfunctionSubgroupRepository(transITDBContext),
                new StateRepository(transITDBContext),
                new SupplierRepository(transITDBContext),
                new VehicleRepository(transITDBContext),
                new VehicleTypeRepository(transITDBContext),
                new EmployeeRepository(transITDBContext),
                new PostRepository(transITDBContext),
                new TransitionRepository(transITDBContext),
                new LocationRepository(transITDBContext),
                new UserRepository(transITDBContext),
                MockHelpers.TestRoleManager(roleStore),
                MockHelpers.TestUserManager(userStore)
            );

            unitOfWork.RoleManager.CreateAsync(new Role { Name = "ADMIN", TransName = "Адмін" }).Wait();
            unitOfWork.RoleManager.CreateAsync(new Role { Name = "WORKER", TransName = "Працівник" }).Wait();
            unitOfWork.RoleManager.CreateAsync(new Role { Name = "ENGINEER", TransName = "Інженер" }).Wait();
            unitOfWork.RoleManager.CreateAsync(new Role { Name = "REGISTER", TransName = "Реєстратор" }).Wait();
            unitOfWork.RoleManager.CreateAsync(new Role { Name = "ANALYST", TransName = "Аналітик" }).Wait();

            return unitOfWork;
        }

        public void Dispose()
        {
        }
    }
}