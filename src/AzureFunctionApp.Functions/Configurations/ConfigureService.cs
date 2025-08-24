using Azure.Security.KeyVault.Secrets;
using AzureFunctionApp.Core.Services.Implementations;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Functions.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureService
{
    public static void Configure(IServiceCollection services)
    {

        services.AddSingleton<IGreeterService, GreeterService>();
        services.AddSingleton<IProductService, ProductService>();
        
        services.AddSingleton((serviceProvider) =>
        {
            var configuration = serviceProvider.GetRequiredService<IConfiguration>();
            var secretClient = serviceProvider.GetRequiredService<SecretClient>();
            return new SettingHelper(configuration, secretClient);
        });
    }
}