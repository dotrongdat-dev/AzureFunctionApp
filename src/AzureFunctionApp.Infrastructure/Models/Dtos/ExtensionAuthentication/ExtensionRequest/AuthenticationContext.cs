using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest;

public class AuthenticationContext
{
    [JsonProperty("correlationId")]
    public string? CorrelationId { get; set; }

    [JsonProperty("client")]
    public ClientInfo? Client { get; set; }

    [JsonProperty("protocol")]
    public string? Protocol { get; set; }

    [JsonProperty("clientServicePrincipal")]
    public ServicePrincipalInfo? ClientServicePrincipal { get; set; }

    [JsonProperty("resourceServicePrincipal")]
    public ServicePrincipalInfo? ResourceServicePrincipal { get; set; }

    [JsonProperty("user")]
    public UserInfo? User { get; set; }
}