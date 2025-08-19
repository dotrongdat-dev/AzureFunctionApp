using AzureFunctionApp.Core.Services.Interfaces;

namespace AzureFunctionApp.Core.Services.Implemetations;

public class GreeterService : IGreeterService
{
    public string Greet(string name)
    {
        return $"Hello, {name}!";
    }
}