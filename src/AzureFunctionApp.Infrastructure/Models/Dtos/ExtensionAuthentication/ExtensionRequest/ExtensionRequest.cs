using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest;

public class ExtensionRequest
{
    [JsonProperty("type")]
    public string? Type { get; set; }

    [JsonProperty("source")]
    public string? Source { get; set; }

    [JsonProperty("data")]
    public ExtensionRequestData? Data { get; set; }
}