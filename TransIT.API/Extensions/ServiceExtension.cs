using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using TransIT.BLL.Services;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL.Models.Entities;
using TransIT.DAL.Models.Mappings;
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


            services.AddScoped<ICrudService<User>, UserService>();
            services.AddScoped<ICrudService<ActionType>, ActionTypeService>();
            services.AddScoped<ICrudService<Vehicle>, VehicleService>();
            services.AddScoped<ICrudService<VehicleType>, VehicleTypeService>();
            services.AddScoped<ICrudService<Malfunction>, MalfunctionService>();
            services.AddScoped<ICrudService<MalfunctionGroup>, MalfunctionGroupService>();
            services.AddScoped<ICrudService<MalfunctionSubgroup>, MalfunctionSubgroupService>();
            services.AddScoped<ICrudService<Bill>, BillService>();
            services.AddScoped<ICrudService<Document>, DocumentService>();
            services.AddScoped<ICrudService<Issue>, IssueService>();
            services.AddScoped<ICrudService<IssueLog>, IssueLogService>();
            services.AddScoped<ICrudService<Supplier>, SupplierService>();
            services.AddScoped<ICrudService<Role>, RoleService>();
            services.AddScoped<ICrudService<State>, StateService>();
            services.AddScoped<ICrudService<Currency>, CurrencyService>();
            services.AddScoped<ICrudService<Country>, CountryService>();
            services.AddScoped<ICrudService<Employee>, EmployeeService>();
            services.AddScoped<ICrudService<Post>, PostService>();
            services.AddScoped<ICrudService<Transition>, TransitionService>();
            services.AddScoped<ICrudService<Location>, LocationService>();


            services.AddScoped<IFilterService<User>, FilterService<User>>();
            services.AddScoped<IFilterService<ActionType>, FilterService<ActionType>>();
            services.AddScoped<IFilterService<Vehicle>, FilterService<Vehicle>>();
            services.AddScoped<IFilterService<VehicleType>, FilterService<VehicleType>>();
            services.AddScoped<IFilterService<Malfunction>, FilterService<Malfunction>>();
            services.AddScoped<IFilterService<MalfunctionGroup>, FilterService<MalfunctionGroup>>();
            services.AddScoped<IFilterService<MalfunctionSubgroup>, FilterService<MalfunctionSubgroup>>();
            services.AddScoped<IFilterService<Bill>, FilterService<Bill>>();
            services.AddScoped<IFilterService<Document>, FilterService<Document>>();
            services.AddScoped<IFilterService<Issue>, FilterService<Issue>>();
            services.AddScoped<IFilterService<IssueLog>, FilterService<IssueLog>>();
            services.AddScoped<IFilterService<Supplier>, FilterService<Supplier>>();
            services.AddScoped<IFilterService<Role>, FilterService<Role>>();
            services.AddScoped<IFilterService<State>, FilterService<State>>();
            services.AddScoped<IFilterService<Currency>, FilterService<Currency>>();
            services.AddScoped<IFilterService<Country>, FilterService<Country>>();
            services.AddScoped<IFilterService<Employee>, FilterService<Employee>>();
            services.AddScoped<IFilterService<Post>, FilterService<Post>>();
            services.AddScoped<IFilterService<Transition>, FilterService<Transition>>();
            services.AddScoped<IFilterService<Location>, FilterService<Location>>();

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
