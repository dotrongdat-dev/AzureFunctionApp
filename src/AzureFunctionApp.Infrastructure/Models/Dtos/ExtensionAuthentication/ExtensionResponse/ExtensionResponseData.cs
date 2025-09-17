using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse
{
    public class ExtensionResponseData
    {
        [JsonProperty("@odata.type")]
        public static string ODataType { get; } = "microsoft.graph.onTokenIssuanceStartResponseData";
        [JsonProperty("actions")]
        public List<Action> Actions { get; set; } = new List<Action>();
    }
}
