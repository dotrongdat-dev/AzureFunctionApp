using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest
{
    public class ClientInfo
    {
        [JsonProperty("ip")]
        public string? Ip { get; set; }

        [JsonProperty("locale")]
        public string? Locale { get; set; }

        [JsonProperty("market")]
        public string? Market { get; set; }
    }
}
