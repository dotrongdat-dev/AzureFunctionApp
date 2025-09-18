using System.Text.Json.Serialization;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;

public class Claims
{
    [JsonPropertyName("CorrelationId")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? CorrelationId { get; set; }
    //[JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    [JsonPropertyName("DateOfBirth")]
    public string? DateOfBirth { get; set; }
    [JsonPropertyName("ApiVersion")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? ApiVersion { get; set; }
    [JsonPropertyName("CustomRoles")]
    public List<string> CustomRoles { get; set; } = [];
}
