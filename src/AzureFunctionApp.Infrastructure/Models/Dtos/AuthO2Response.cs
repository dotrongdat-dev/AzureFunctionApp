using System.Text.Json.Serialization;

namespace AzureFunctionApp.Infrastructure.Models.Dtos;

public class AuthO2Response
{
    [JsonPropertyName("token_type")]
    public string tokenType { get; set; }
    [JsonPropertyName("scope")]
    public string scope { get; set; }
    [JsonPropertyName("expires_in")]
    public long expiresIn { get; set; }
    [JsonPropertyName("ext_expires_in")]
    public long extExpiresIn { get; set; }
    [JsonPropertyName("access_token")]
    public string accessToken { get; set; }
    [JsonPropertyName("refresh_token")]
    public string refreshToken { get; set; }
    [JsonPropertyName("id_token")]
    public string idToken { get; set; }
}