using System.Text.Json.Serialization;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse
{
    public class ExtensionResponseData
    {
        [JsonPropertyName("@odata.type")]
        public string ODataType { get; } = "microsoft.graph.onTokenIssuanceStartResponseData";
        [JsonPropertyName("actions")]
        public List<Action> Actions { get; set; } = new List<Action>();
    }
}
