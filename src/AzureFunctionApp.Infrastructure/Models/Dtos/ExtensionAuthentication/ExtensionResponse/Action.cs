using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class Action
{
    [JsonProperty("@odata.type")]
    public string ODataType { get; } = "microsoft.graph.tokenIssuanceStart.provideClaimsForToken";
    [JsonProperty("claims")]
    public Claims Claims { get; set; } = new();
}
