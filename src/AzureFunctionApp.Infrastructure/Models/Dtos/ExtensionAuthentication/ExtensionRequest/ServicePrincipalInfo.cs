using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest
{
    public class ServicePrincipalInfo
    {
        [JsonProperty("id")]
        public string? Id { get; set; }

        [JsonProperty("appId")]
        public string? AppId { get; set; }

        [JsonProperty("appDisplayName")]
        public string? AppDisplayName { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }
    }
}
