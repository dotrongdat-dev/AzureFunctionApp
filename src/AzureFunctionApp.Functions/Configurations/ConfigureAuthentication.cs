using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureAuthentication
{
    public static void Configure(IServiceCollection services)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = "https://login.microsoftonline.com/6086a83a-cdae-48c5-985c-0bf5ba575ef3/v2.0";
                options.Audience = "34c37852-64ec-4b95-80fa-714a50e41b2e";
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "https://login.microsoftonline.com/6086a83a-cdae-48c5-985c-0bf5ba575ef3/v2.0",
                    ValidAudience = "34c37852-64ec-4b95-80fa-714a50e41b2e"
                };
            });

        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions()
            {
                Info = new OpenApiInfo()
                {
                    Version = "1.0.0",
                    Title = "Your API",
                    Description = "Your API Description"
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = OpenApiVersionType.V3,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false,
            };

            // // Add security definition for Bearer token
            // options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            // {
            //     Type = SecuritySchemeType.Http,
            //     Scheme = "bearer",
            //     BearerFormat = "JWT",
            //     Description = "Enter 'Bearer' followed by a space and your token"
            // });

            return options;
        });
    }
}