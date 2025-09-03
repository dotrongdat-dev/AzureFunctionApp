using AzureFunctionApp.Infrastructure.Models.Dtos;

namespace AzureFunctionApp.Core.Services.Interfaces;

public interface IGreeterService
{
    string Greet(string name);
    Task<AuthO2Response?> Login(string username, string password);
    Task<AuthO2Response?> RefreshToken(string refreshToken);
}