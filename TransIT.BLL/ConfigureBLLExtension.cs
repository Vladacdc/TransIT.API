using System;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TransIT.BLL.DTOs;
using TransIT.BLL.Factories;
using TransIT.BLL.Mappings;
using TransIT.BLL.Services;
using TransIT.BLL.Services.ServicesOptions;
using TransIT.BLL.Services.FilterServices;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.Interfaces;
using TransIT.DAL;
using TransIT.DAL.Exceptions;

namespace TransIT.BLL
{
    public static class ConfigureBLLExtension
    {
        public static void ConfigureBLL(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            services.ConfigureAutoMapper();
            services.ConfigureServices();
            services.ConfigureFilterServices();

            services.AddSingleton<IServiceFactory, ServiceFactory>();
            services.AddSingleton<IFilterServiceFactory, FilterServiceFactory>();

            services.ConfigureDocumentService(configuration);

            services.ConfigureDAL(configuration, hostingEnvironment);
        }

        private static void ConfigureServices(this IServiceCollection services)
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
            services.AddScoped<IPartService, PartService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IPartsInService, PartsInService>();
            services.AddScoped<ITransitionService, TransitionService>();
            services.AddScoped<ILocationService, LocationService>();
            services.AddScoped<IStatisticsService, StatisticsService>();
            services.AddScoped<IUnitService, UnitService>();
            services.AddScoped<IManufacturerService, ManufacturerService>();
            services.AddScoped<IWorkTypeService, WorkTypeService>();
        }

        private static void ConfigureFilterServices(this IServiceCollection services)
        {
            services.AddScoped<IFilterService<ActionTypeDTO>, ActionTypeFilterService>();
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
            services.AddScoped<IFilterService<EmployeeDTO>, EmployeeFilterService>();
            services.AddScoped<IFilterService<PartDTO>, PartFilterService>();
            services.AddScoped<IFilterService<PostDTO>, PostFilterService>();
            services.AddScoped<IFilterService<TransitionDTO>, TransitionFilterService>();
            services.AddScoped<IFilterService<LocationDTO>, LocationFilterService>();
            services.AddScoped<IFilterService<UserDTO>, UserFilterService>();
            services.AddScoped<IFilterService<PartInDTO>, PartsInFilterService>();
            services.AddScoped<IFilterService<UnitDTO>, UnitFilterService>();
            services.AddScoped<IFilterService<ManufacturerDTO>, ManufacturerFilterService>();
            services.AddScoped<IFilterService<WorkTypeDTO>,WorkTypeFilterService>();
        }

        private static void ConfigureAutoMapper(this IServiceCollection services)
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
                c.AddProfile(new PartProfile());
                c.AddProfile(new PostProfile());
                c.AddProfile(new EmployeeProfile());
                c.AddProfile(new TransitionProfile());
                c.AddProfile(new LocationProfile());
                c.AddProfile(new PartInProfile());
                c.AddProfile(new UnitProfile());
                c.AddProfile(new ManufacturerProfile());
                c.AddProfile(new WorkTypeProfile());
            }).CreateMapper());
        }

        private static void ConfigureDocumentService(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            services.Configure<DocumentServiceOptions>((options) =>
            {
                long maximumSize;
                var documentServiceOptions = configuration.GetSection(nameof(DocumentServiceOptions));

                if (documentServiceOptions.Exists())
                {
                    var maximumSizeConfiguration = documentServiceOptions.GetSection(nameof(options.MaximumSize));

                    if (maximumSizeConfiguration.Exists())
                    {
                        long bytesInMb = 1024 * 1024;

                        //Here Convert class is used instead of parse method,
                        //because when devops specifies incorrect configuration value,
                        //he will see an exception right away.
                        maximumSize = Convert.ToInt64(maximumSizeConfiguration.Value) * bytesInMb;
                    }
                    else
                    {
                        maximumSize = Int64.MaxValue;
                    }
                }
                else
                {
                    maximumSize = Int64.MaxValue;
                }

                if (maximumSize <= 0)
                {
                    throw new InvalidValueException("Size cannot be zero or negative");
                }

                options.MaximumSize = maximumSize;
            });
        }
    }
}