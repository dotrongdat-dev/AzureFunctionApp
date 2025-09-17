using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class Claims
{
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? CorrelationId { get; set; }
    [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    public string? DateOfBirth { get; set; }
    public string? ApiVersion { get; set; }
    public List<string> CustomRoles { get; set; } = [];
}