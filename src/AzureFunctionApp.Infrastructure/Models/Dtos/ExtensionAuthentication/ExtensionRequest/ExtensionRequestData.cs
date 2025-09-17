using AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest;
using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest
{
    public class ExtensionRequestData
    {
        [JsonProperty("@odata.type")]
        public string? ODataType { get; set; }

        [JsonProperty("tenantId")]
        public string? TenantId { get; set; }

        [JsonProperty("authenticationEventListenerId")]
        public string? AuthenticationEventListenerId { get; set; }

        [JsonProperty("customAuthenticationExtensionId")]
        public string? CustomAuthenticationExtensionId { get; set; }

        [JsonProperty("authenticationContext")]
        public AuthenticationContext? AuthenticationContext { get; set; }
    }
}
