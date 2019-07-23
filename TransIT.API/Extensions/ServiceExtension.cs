using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransIT.BLL.Services;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.BLL.Mappings;
using TransIT.DAL.Repositories;
using TransIT.DAL.Repositories.ImplementedRepositories;
using TransIT.DAL.Repositories.InterfacesRepositories;
using TransIT.DAL.UnitOfWork;

namespace TransIT.API.Extensions
{
    public static class ServiceExtension
    {
        public static void ConfigureAutoMapper(this IServiceCollection services)
        {
            services.AddSingleton(new MapperConfiguration(c =>
            {
                c.AddProfile(new RoleProfile());
                c.AddProfile(new UserProfile());
                c.AddProfile(new TokenProfile());
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
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IStateService, StateService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<ICountryService, CountryService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<ITransitionService, TransitionService>();
            services.AddScoped<ILocationService, LocationService>();


            services.AddScoped<ICrudService<string, User>, UserService>();
            services.AddScoped<ICrudService<int, ActionType>, ActionTypeService>();
            services.AddScoped<ICrudService<int, Vehicle>, VehicleService>();
            services.AddScoped<ICrudService<int, VehicleType>, VehicleTypeService>();
            services.AddScoped<ICrudService<int, Malfunction>, MalfunctionService>();
            services.AddScoped<ICrudService<int, MalfunctionGroup>, MalfunctionGroupService>();
            services.AddScoped<ICrudService<int, MalfunctionSubgroup>, MalfunctionSubgroupService>();
            services.AddScoped<ICrudService<int, Bill>, BillService>();
            services.AddScoped<ICrudService<int, Document>, DocumentService>();
            services.AddScoped<ICrudService<int, Issue>, IssueService>();
            services.AddScoped<ICrudService<int, IssueLog>, IssueLogService>();
            services.AddScoped<ICrudService<int, Supplier>, SupplierService>();
            services.AddScoped<ICrudService<string, Role>, RoleService>();
            services.AddScoped<ICrudService<int, State>, StateService>();
            services.AddScoped<ICrudService<int, Currency>, CurrencyService>();
            services.AddScoped<ICrudService<int, Country>, CountryService>();
            services.AddScoped<ICrudService<int, Employee>, EmployeeService>();
            services.AddScoped<ICrudService<int, Post>, PostService>();
            services.AddScoped<ICrudService<int, Transition>, TransitionService>();
            services.AddScoped<ICrudService<int, Location>, LocationService>();


            services.AddScoped<IFilterService<string, User>, FilterService<string, User>>();
            services.AddScoped<IFilterService<int, ActionType>, FilterService<int, ActionType>>();
            services.AddScoped<IFilterService<int, Vehicle>, FilterService<int, Vehicle>>();
            services.AddScoped<IFilterService<int, VehicleType>, FilterService<int, VehicleType>>();
            services.AddScoped<IFilterService<int, Malfunction>, FilterService<int, Malfunction>>();
            services.AddScoped<IFilterService<int, MalfunctionGroup>, FilterService<int, MalfunctionGroup>>();
            services.AddScoped<IFilterService<int, MalfunctionSubgroup>, FilterService<int, MalfunctionSubgroup>>();
            services.AddScoped<IFilterService<int, Bill>, FilterService<int, Bill>>();
            services.AddScoped<IFilterService<int, Document>, FilterService<int, Document>>();
            services.AddScoped<IFilterService<int, Issue>, FilterService<int, Issue>>();
            services.AddScoped<IFilterService<int, IssueLog>, FilterService<int, IssueLog>>();
            services.AddScoped<IFilterService<int, Supplier>, FilterService<int, Supplier>>();
            services.AddScoped<IFilterService<string, Role>, FilterService<string, Role>>();
            services.AddScoped<IFilterService<int, State>, FilterService<int, State>>();
            services.AddScoped<IFilterService<int, Currency>, FilterService<int, Currency>>();
            services.AddScoped<IFilterService<int, Country>, FilterService<int, Country>>();
            services.AddScoped<IFilterService<int, Employee>, FilterService<int, Employee>>();
            services.AddScoped<IFilterService<int, Post>, FilterService<int, Post>>();
            services.AddScoped<IFilterService<int, Transition>, FilterService<int, Transition>>();
            services.AddScoped<IFilterService<int, Location>, FilterService<int, Location>>();

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
            services.AddScoped<IQueryRepository<Role>, RoleRepository>();
            services.AddScoped<IQueryRepository<State>, StateRepository>();
            services.AddScoped<IQueryRepository<Supplier>, SupplierRepository>();
            services.AddScoped<IQueryRepository<Token>, TokenRepository>();
            services.AddScoped<IQueryRepository<User>, UserRepository>();
            services.AddScoped<IQueryRepository<Vehicle>, VehicleRepository>();
            services.AddScoped<IQueryRepository<VehicleType>, VehicleTypeRepository>();
            services.AddScoped<IQueryRepository<Currency>, CurrencyRepository>();
            services.AddScoped<IQueryRepository<Country>, CountryRepository>();
            services.AddScoped<IQueryRepository<Employee>, EmployeeRepository>();
            services.AddScoped<IQueryRepository<Post>, PostRepository>();
            services.AddScoped<IQueryRepository<Transition>, TransitionRepository>();
            services.AddScoped<IQueryRepository<Location>, LocationRepository>();


            services.AddScoped<IActionTypeRepository, ActionTypeRepository>();
            services.AddScoped<IBillRepository, BillRepository>();
            services.AddScoped<IDocumentRepository, DocumentRepository>();
            services.AddScoped<IIssueRepository, IssueRepository>();
            services.AddScoped<IIssueLogRepository, IssueLogRepository>();
            services.AddScoped<IMalfunctionRepository, MalfunctionRepository>();
            services.AddScoped<IMalfunctionGroupRepository, MalfunctionGroupRepository>();
            services.AddScoped<IMalfunctionSubgroupRepository, MalfunctionSubgroupRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<ISupplierRepository, SupplierRepository>();
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IVehicleRepository, VehicleRepository>();
            services.AddScoped<IVehicleTypeRepository, VehicleTypeRepository>();
            services.AddScoped<ICurrencyRepository, CurrencyRepository>();
            services.AddScoped<ICountryRepository, CountryRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<ITransitionRepository, TransitionRepository>();
            services.AddScoped<ILocationRepository, LocationRepository>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
