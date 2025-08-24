using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using AzureFunctionApp.Functions.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureKeyVaultClient
{
    public static void Configure(IServiceCollection services)
    {
        Uri kvUri = new("https://dtpracticekeyvault.vault.azure.net/");
        SecretClient client = new(kvUri, new DefaultAzureCredential());
        services.AddSingleton(client);
    }
}