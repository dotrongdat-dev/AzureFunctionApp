using AzureFunctionApp.Functions.Configurations;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

var services = builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddSingleton((serviceProvider) =>
{
    CosmosClient client = new(
        connectionString: builder.Configuration.GetConnectionString("CosmosDb")
    );
    return client;
});

ConfigureService.Configure(services);
ConfigureKeyVaultClient.Configure(services);

builder.Build().Run();
