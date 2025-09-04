using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureKeyVaultClient
{
    public static void Configure(IServiceCollection services)
    {
        Uri kvUri = new(Environment.GetEnvironmentVariable("KEY_VAULT_URI") ?? string.Empty);
        SecretClient client =  new(kvUri, new DefaultAzureCredential());
        services.AddSingleton(client);
    }
}