using Microsoft.PowerPlatform.Dataverse.Client;

namespace AzureFunctionApp.Infrastructure.Providers;

public class ServiceClientProvier
{    
    public string? accessToken { get; set; }
    private static string _defaultAccessToken;
    private static string _dataverseUrl;
    private ServiceClient _serviceClient;
    private static ServiceClient _defaultServiceClient;

    public void setDefaultAccessToken(Func<Task<string>> getTokenFunc)
    {
        _defaultAccessToken ??= getTokenFunc().Result;
    }

    public void setDataverseUrl(string dataverseUrl)
    {
        _dataverseUrl ??= dataverseUrl;
    }
        
    public void setDefaultServiceClient(ServiceClient serviceClient)
    {
        _defaultServiceClient ??= serviceClient;
    }

    public ServiceClient GetServiceClient()
    {
        if (_serviceClient != null) return _serviceClient;
        if (accessToken == null) return _defaultServiceClient;
        return new ServiceClient(new Uri(_dataverseUrl), (authority) => Task.FromResult(accessToken));
        //return new ServiceClient(new Uri(_dataverseUrl), (authority) => Task.FromResult(accessToken ?? _defaultAccessToken));
    }
}