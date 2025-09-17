using Azure.Security.KeyVault.Secrets;
using AzureFunctionApp.Core.Services;
using AzureFunctionApp.Core.Services.Implementations;
using AzureFunctionApp.Core.Services.Implemetations;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Functions.Helpers;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using AzureFunctionApp.Infrastructure.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureService
{
    public static void Configure(IServiceCollection services)
    {
        services.AddScoped<IGreeterService, GreeterService>();
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IDataverseService, DataverseService>();
        services.AddScoped<IEntraUserService, EntraUserService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ISystemUserService, SystemUserService>();
        services.AddScoped<ClaimProvider>();
    }

    public static void ConfigureSingleton(IServiceCollection services)
    {
        services.AddSingleton((serviceProvider) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var secretClient = serviceProvider.GetRequiredService<SecretClient>();
            return new SettingHelper(configuration, secretClient);
        });

        services.AddSingleton((serviceProvider) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            SettingHelper settingHelper = serviceProvider.GetRequiredService<SettingHelper>();
            return new AzureTernantConfig
            {
                ternantId = settingHelper.GetValue<string>("Azure.Ternant.Id") ?? string.Empty,
                clientId = settingHelper.GetValue<string>("Azure.Ternant.ClientId") ?? string.Empty,
                scope = $"openid profile offline_access {settingHelper.GetValue<string>("Azure.Ternant.Scope") ?? string.Empty}",
                clientSecret = settingHelper.GetValue<string>("Azure.Ternant.ClientSecret") ?? string.Empty
            };
        });

        services.AddSingleton(new HttpClient());
    }
}