using AzureFunctionApp.Functions.Helpers;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureDataverseServiceClient
{
    public static void Configure(IServiceCollection services)
    {
        services.AddSingleton(serviceProvider =>
        {
            var settingHelper = serviceProvider.GetRequiredService<SettingHelper>();
            var dataverseUrl = settingHelper.GetValue<string>("DataverseURL") ?? string.Empty;
            var azureTernantConfig = serviceProvider.GetRequiredService<AzureTernantConfig>();
            string connectionString = $@"
            AuthType=ClientSecret;
            Url={dataverseUrl};
            ClientId={azureTernantConfig.clientId};
            ClientSecret={azureTernantConfig.clientSecret};
            TenantId={azureTernantConfig.ternantId}";
            return new ServiceClient(connectionString);
        });
    }
}