using System.Text.Json.Serialization;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class ExtensionResponse
{
    [JsonPropertyName("data")]
    public ExtensionResponseData Data { get; set; } = new();
}
