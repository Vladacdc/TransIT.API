using System;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using TransIT.API.EndpointFilters.OnActionExecuting;
using TransIT.API.EndpointFilters.OnException;
using TransIT.API.Extensions;
using TransIT.API.Hubs;
using TransIT.BLL.Factory;

namespace TransIT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureDbContext(Configuration, Environment);
            services.ConfigureIdentity();

            services.AddSignalR();
            services.ConfigureAutoMapper();
            services.ConfigureAuthentication(Configuration);
            services.ConfigureCors();
            services.ConfigureModelRepositories();
            services.ConfigureDataAccessServices();

            services.AddScoped<IServiceFactory, ServiceFactory>();

            services.ConfigureSwagger();

            services.AddMvc(options =>
                {
                    options.Filters.Add(typeof(ValidateModelStateAttribute));
                    options.Filters.Add(typeof(ApiExceptionFilterAttribute));
                })
                .AddJsonOptions(x => x.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Startup>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IServiceProvider serviceProvider)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseCors("CorsPolicy");
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "TransIT API");
            });

            app.UseSignalR(routes =>
            {
                routes.MapHub<IssueHub>("/issuehub");
            });

            app.Seed(serviceProvider, Configuration, Environment);
        }
    }
}
