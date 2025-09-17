using AzureFunctionApp.Core.Providers;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using Microsoft.PowerPlatform.Dataverse.Client;

public class ClaimProvider
{
    private ServiceClient _serviceClient;

    public ClaimProvider(ServiceClientProvier serviceClientProvier)
    {
        _serviceClient = serviceClientProvier.GetServiceClient();
    }

    public ExtensionResponse GetCustomClaims(ExtensionRequest request)
    {
        throw new NotImplementedException();   
    }
}