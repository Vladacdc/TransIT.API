using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using TransIT.BLL.Helpers;
using TransIT.BLL.Helpers.Abstractions;
using TransIT.BLL.Services.ImplementedServices;
using TransIT.BLL.Services.Interfaces;

namespace TransIT.API.Extensions
{
    public static class AuthenticationExtension
    {
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var key = GenerateKey(25);
            var jwtAppSettingOptions = configuration.GetSection(nameof(JwtIssuerOptions));
            var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            services.Configure<JwtIssuerOptions>(options =>
            {
                options.Issuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                options.Audience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)];
                options.AccessExpirationMins = int.Parse(jwtAppSettingOptions["Lifetime"]);
                options.RefreshExpirationMins = 60 * 24 * 7;

                options.SigningCredentials = new SigningCredentials(
                     signingKey,
                     SecurityAlgorithms.HmacSha256);
            });
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)],

                ValidateAudience = true,
                ValidAudience = jwtAppSettingOptions[nameof(JwtIssuerOptions.Audience)],

                ValidateIssuerSigningKey = true,
                IssuerSigningKey = signingKey,

                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
            };

            _ = services.AddAuthentication(options =>
              {
                  options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                  options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
              }).AddJwtBearer(configureOptions =>
              {
                  configureOptions.ClaimsIssuer = jwtAppSettingOptions[nameof(JwtIssuerOptions.Issuer)];
                  configureOptions.TokenValidationParameters = tokenValidationParameters;

                  configureOptions.Events = new JwtBearerEvents
                  {
                      OnMessageReceived = context =>
                      {
                          var accessToken = context.Request.Query["access_token"];

                          if (!string.IsNullOrEmpty(accessToken)
                              && context.HttpContext.Request.Path
                                  .StartsWithSegments("/issuehub"))
                          {
                              context.Token = accessToken;
                          }

                          return Task.CompletedTask;
                      },
                  };
              });

            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
        }

        private static string GenerateKey(int length)
        {
            var characterArray = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxy0123456789".ToCharArray();
            if (length < 0)
            {
                throw new ArgumentException("length must not be negative", "length");
            }

            if (length > int.MaxValue / 8)
            {
                throw new ArgumentException("length is too big", "length");
            }

            var bytes = new byte[length * 8];
            new RNGCryptoServiceProvider().GetBytes(bytes);
            var result = new char[length];
            for (int i = 0; i < length; i++)
            {
                ulong value = BitConverter.ToUInt64(bytes, i * 8);
                result[i] = characterArray[value % (uint)characterArray.Length];
            }

            return new string(result);
        }
    }
}
