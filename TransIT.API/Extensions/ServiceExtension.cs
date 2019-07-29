

using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using TransIT.API.DependencyInjection;
using TransIT.BLL.DTOs;
using TransIT.BLL.Mappings;
using TransIT.BLL.Services;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models;
using TransIT.DAL.Models.DependencyInjection;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureDbContext(
            this IServiceCollection services,
            IConfiguration Configuration,
            IHostingEnvironment Environment)
        {
            void configureConnection(DbContextOptionsBuilder options)
            {
                if (Environment.IsDevelopment())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("TransIT.API"));
                }
                if (Environment.IsProduction())
                {
                    options.UseSqlServer(Configuration.GetConnectionString("AzureConnection"), b => b.MigrationsAssembly("TransIT.API"));
                }
            }

            services.AddScoped<IUser, CurrentUser>();
            services.AddDbContext<TransITDBContext>(configureConnection);
        }

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, Role>(options => options.Stores.MaxLengthForKeys = 128)
                .AddEntityFrameworkStores<TransITDBContext>()
                .AddRoleManager<RoleManager<Role>>()
                .AddUserManager<UserManager<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;
            });
        }

        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
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
            }).CreateMapper());
        }

        public static void ConfigureDataAccessServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IActionTypeService, ActionTypeService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IVehicleTypeService, VehicleTypeService>();
            services.AddScoped<IMalfunctionService, MalfunctionService>();
            services.AddScoped<IMalfunctionGroupService, MalfunctionGroupService>();
            services.AddScoped<IMalfunctionSubgroupService, MalfunctionSubgroupService>();
            services.AddScoped<IBillService, BillService>();
            services.AddScoped<IDocumentService, DocumentService>();
            services.AddScoped<IIssueService, IssueService>();
            services.AddScoped<IIssueLogService, IssueLogService>();
            services.AddScoped<ISupplierService, SupplierService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITransitionService, TransitionService>();
            services.AddScoped<ILocationService, LocationService>();


            services.AddScoped<ICrudService<ActionTypeDTO>, ActionTypeService>();
            services.AddScoped<ICrudService<VehicleDTO>, VehicleService>();
            services.AddScoped<ICrudService<VehicleTypeDTO>, VehicleTypeService>();
            services.AddScoped<ICrudService<MalfunctionDTO>, MalfunctionService>();
            services.AddScoped<ICrudService<MalfunctionGroupDTO>, MalfunctionGroupService>();
            services.AddScoped<ICrudService<MalfunctionSubgroupDTO>, MalfunctionSubgroupService>();
            services.AddScoped<ICrudService<BillDTO>, BillService>();
            services.AddScoped<ICrudService<DocumentDTO>, DocumentService>();
            services.AddScoped<ICrudService<IssueDTO>, IssueService>();
            services.AddScoped<ICrudService<IssueLogDTO>, IssueLogService>();
            services.AddScoped<ICrudService<SupplierDTO>, SupplierService>();
            services.AddScoped<ICrudService<StateDTO>, StateService>();
            services.AddScoped<ICrudService<CurrencyDTO>, CurrencyService>();
            services.AddScoped<ICrudService<CountryDTO>, CountryService>();
            services.AddScoped<ICrudService<EmployeeDTO>, EmployeeService>();
            services.AddScoped<ICrudService<PostDTO>, PostService>();
            services.AddScoped<ICrudService<TransitionDTO>, TransitionService>();
            services.AddScoped<ICrudService<LocationDTO>, LocationService>();

            services.AddScoped<IFilterService<ActionTypeDTO>, FilterService<ActionTypeDTO>>();
            services.AddScoped<IFilterService<VehicleDTO>, VehicleFilterService>();
            services.AddScoped<IFilterService<VehicleTypeDTO>, VehicleTypeFilterService>();
            services.AddScoped<IFilterService<MalfunctionDTO>, MalfunctionFilterService>();
            services.AddScoped<IFilterService<MalfunctionGroupDTO>, MalfunctionGroupFilterService>();
            services.AddScoped<IFilterService<MalfunctionSubgroupDTO>, MalfunctionSubgroupFilterService>();
            services.AddScoped<IFilterService<BillDTO>, BillFilterService>();
            services.AddScoped<IFilterService<DocumentDTO>, DocumentFilterService>();
            services.AddScoped<IFilterService<IssueDTO>, IssueFilterService>();
            services.AddScoped<IFilterService<IssueLogDTO>, IssueLogFilterService>();
            services.AddScoped<IFilterService<SupplierDTO>, SupplierFilterService>();
            services.AddScoped<IFilterService<StateDTO>, StateFilterService>();
            services.AddScoped<IFilterService<CurrencyDTO>, CurrencyFilterService>();
            services.AddScoped<IFilterService<CountryDTO>, CountryFilterService>();
            services.AddScoped<IFilterService<EmployeeDTO>, EmployeeFilterServiceEmployee>();
            services.AddScoped<IFilterService<PostDTO>, PostFilterService>();
            services.AddScoped<IFilterService<TransitionDTO>, TransitionFilterService>();
            services.AddScoped<IFilterService<LocationDTO>, LocationFilterService>();
            services.AddScoped<IFilterService<UserDTO>, UserFilterService>();
        }

        public static void ConfigureModelRepositories(this IServiceCollection services)
        {
            services.AddScoped<IQueryRepository<ActionType>, ActionTypeRepository>();
            services.AddScoped<IQueryRepository<Bill>, BillRepository>();
            services.AddScoped<IQueryRepository<Document>, DocumentRepository>();
            services.AddScoped<IQueryRepository<Issue>, IssueRepository>();
            services.AddScoped<IQueryRepository<IssueLog>, IssueLogRepository>();
            services.AddScoped<IQueryRepository<Malfunction>, MalfunctionRepository>();
            services.AddScoped<IQueryRepository<MalfunctionGroup>, MalfunctionGroupRepository>();
            services.AddScoped<IQueryRepository<MalfunctionSubgroup>, MalfunctionSubgroupRepository>();
            services.AddScoped<IQueryRepository<State>, StateRepository>();
            services.AddScoped<IQueryRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IQueryRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<IQueryRepository<VehicleType>, VehicleTypeRepository>();
            services.AddScoped<IQueryRepository<Currency>, CurrencyRepository>();
            services.AddScoped<IQueryRepository<Country>, CountryRepository>();
            services.AddScoped<IQueryRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IQueryRepository<Post>, PostRepository>();
            services.AddScoped<IQueryRepository<Transition>, TransitionRepository>();
            services.AddScoped<IQueryRepository<Location>, LocationRepository>();
            services.AddScoped<IQueryRepository<User>, UserQueryRepository>();

            services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IIssueLogRepository, IssueLogRepository>();
            services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            services.AddScoped<IMalfunctionGroupRepository, MalfunctionGroupRepository>();
            services.AddScoped<IMalfunctionSubgroupRepository, MalfunctionSubgroupRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITransitionRepository, TransitionRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();
            services.AddScoped<UserManager<User>>();
            services.AddScoped<RoleManager<Role>>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
