namespace AzureFunctionApp.Infrastructure.Models.Dtos;

public class AzureTernantConfig
{
    public string ternantId { get; set; } = string.Empty;
    public string clientId { get; set; } = string.Empty;
    public string scope { get; set; } = string.Empty;
    public string clientSecret { get; set; } = string.Empty;

    public string GetAuthorityURL()
    {
        return $"https://login.microsoftonline.com/{ternantId}/v2.0";
    }

    public string GetAuthROPCURL()
    {
        return $"https://login.microsoftonline.com/{ternantId}/oauth2/v2.0/token";
    }
}