using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class ExtensionResponse
{
    [JsonProperty("data")]
    public ExtensionResponseData Data { get; set; } = new();
}