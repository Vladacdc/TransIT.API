using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace TransIT.API.Extensions
{
    public static class SwaggerExtension
    {
        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info
                    {
                        Version = "v1",
                        Title = "TransIT API",
                    });
                    var security = new Dictionary<string, IEnumerable<string>>
                    {
                    { "Bearer", new string[] { } },
                    };

                    c.AddSecurityDefinition("Bearer", new ApiKeyScheme
                    {
                        Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                        Name = "Authorization",
                        In = "header",
                        Type = "apiKey",
                    });
                    c.AddSecurityRequirement(security);
                    c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                });
        }
    }
}
