using System.Collections;
using AzureFunctionApp.Functions.Helpers;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureCosmosClient
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton<CosmosClient>(serviceProvider =>
        {
            var settingHelper = serviceProvider.GetRequiredService<SettingHelper>();
            var connectionStrings = settingHelper.GetValue<string>("ConnectionStrings.CosmosDb");
            Console.WriteLine(connectionStrings);
            return new(
            connectionString: connectionStrings);
        });
    
    }
}