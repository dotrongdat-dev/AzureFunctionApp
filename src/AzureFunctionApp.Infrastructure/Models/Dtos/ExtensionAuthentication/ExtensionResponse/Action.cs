using System.Text.Json.Serialization;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class Action
{
    [JsonPropertyName("@odata.type")]
    public string ODataType { get; } = "microsoft.graph.tokenIssuanceStart.provideClaimsForToken";
    [JsonPropertyName("claims")]
    public Claims Claims { get; set; } = new();
}
