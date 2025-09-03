using System.Text.Json;
using System.Threading.Tasks;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Dtos;

namespace AzureFunctionApp.Core.Services.Implementations;

public class GreeterService : IGreeterService
{
    private readonly HttpClient _httpClient;
    private readonly AzureTernantConfig _azureTernantConfig;

    public GreeterService(HttpClient httpClient, AzureTernantConfig azureTernantConfig)
    {
        _httpClient = httpClient;
        _azureTernantConfig = azureTernantConfig;
    }

    public string Greet(string name)
    {
        return $"Hello, {name}!";
    }

    public async Task<AuthO2Response?> Login(string username, string password)=>
        await RequestTokenAsync(new Dictionary<string, string>
        {
            ["grant_type"] = "password",
            ["client_id"] = _azureTernantConfig.clientId,
            ["client_secret"] = _azureTernantConfig.clientSecret,
            ["username"] = username,
            ["password"] = password,
            ["scope"] = _azureTernantConfig.scope
        });

    public async Task<AuthO2Response?> RefreshToken(string refreshToken)=>
        await RequestTokenAsync(new Dictionary<string, string>
        {
            ["grant_type"] = "refresh_token",
            ["client_id"] = _azureTernantConfig.clientId,
            ["client_secret"] = _azureTernantConfig.clientSecret,
            ["refresh_token"] = refreshToken,
            ["scope"] = _azureTernantConfig.scope
        });

    private async Task<AuthO2Response?> RequestTokenAsync(Dictionary<string, string> body)
    {
        var content = new FormUrlEncodedContent(body);
        var response = await _httpClient.PostAsync(
            _azureTernantConfig.GetAuthROPCURL(),
            content
        );

        var json = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
            throw new Exception($"Token request failed: {json}");

        Console.Out.Write(json);

        return JsonSerializer.Deserialize<AuthO2Response>(json,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}