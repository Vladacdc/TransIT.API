using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.Test;
using Microsoft.EntityFrameworkCore;
using Moq;
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

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider.Setup(x => x.GetService(typeof(ActionTypeRepository))).Returns(new ActionTypeRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(CountryRepository))).Returns(new CountryRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(CurrencyRepository))).Returns(new CurrencyRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(BillRepository))).Returns(new BillRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(DocumentRepository))).Returns(new DocumentRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(IssueRepository))).Returns(new IssueRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(IssueLogRepository))).Returns(new IssueLogRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(MalfunctionRepository))).Returns(new MalfunctionRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(MalfunctionGroupRepository))).Returns(new MalfunctionGroupRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(MalfunctionSubgroupRepository))).Returns(new MalfunctionSubgroupRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(StateRepository))).Returns(new StateRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(SupplierRepository))).Returns(new SupplierRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(VehicleRepository))).Returns(new VehicleRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(VehicleTypeRepository))).Returns(new VehicleTypeRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(EmployeeRepository))).Returns(new EmployeeRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(PartRepository))).Returns(new PartRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(PostRepository))).Returns(new PostRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(TransitionRepository))).Returns(new TransitionRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(PartsInRepository))).Returns(new UserRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(PartsInRepository))).Returns(new PartsInRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(RoleManager<Role>))).Returns(MockHelpers.TestRoleManager(roleStore));
            serviceProvider.Setup(x => x.GetService(typeof(UserManager<User>))).Returns(MockHelpers.TestUserManager(userStore));
            serviceProvider.Setup(x => x.GetService(typeof(UnitRepository))).Returns(new UnitRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(WorkTypeRepository))).Returns(new WorkTypeRepository(transITDBContext));
            serviceProvider.Setup(x => x.GetService(typeof(ManufacturerRepository))).Returns(new ManufacturerRepository(transITDBContext));

            var unitOfWork = new UnitOfWork(transITDBContext, serviceProvider.Object);

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