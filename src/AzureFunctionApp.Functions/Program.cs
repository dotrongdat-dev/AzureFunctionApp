using AzureFunctionApp.Functions.Configurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();
builder.AddMiddleWares();

var services = builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

ConfigureKeyVaultClient.Configure(services);
ConfigureService.Configure(services);
ConfigureDataverseServiceClient.Configure(services);
ConfigureCosmosClient.Configure(services);
ConfigureAuthentication.Configure(services);

builder.Build().Run();
