using AzureFunctionApp.Infrastructure.Models.Dtos;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.DependencyInjection;
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
                var serviceProvider = services.BuildServiceProvider();
                var azureTernantConfig = serviceProvider.GetRequiredService<AzureTernantConfig>();
                options.Authority = azureTernantConfig.GetAuthorityURL();
                options.Audience = azureTernantConfig.clientId;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = azureTernantConfig.GetAuthorityURL(),
                    ValidAudience = azureTernantConfig.clientId
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