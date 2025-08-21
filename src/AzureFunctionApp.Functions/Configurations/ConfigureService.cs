using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureService
{
    public static void Configure(IServiceCollection services)
    {
        // Register the GreeterService as a singleton
        services.AddSingleton<AzureFunctionApp.Core.Services.Interfaces.IGreeterService, AzureFunctionApp.Core.Services.Implemetations.GreeterService>();
        services.AddSingleton<AzureFunctionApp.Core.Services.Interfaces.IProductService, AzureFunctionApp.Core.Services.Implementations.ProductService>();
        // Add other services as needed
        // services.AddSingleton<ICosmosDbService, CosmosDbService>(); // Example for Cosmos DB service
    }
}