using AzureFunctionApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApp.Functions;

public class GreeterFunction
{
    private readonly ILogger<GreeterFunction> _logger;
    private readonly IGreeterService _greeterService;

    public GreeterFunction(
        ILogger<GreeterFunction> logger,
        IGreeterService greeterService)
    {
        _logger = logger;
        _greeterService = greeterService;
    }

    [Function("Greeter")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req)
    {
        string? name = req.Query["name"];
        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please provide a name in the query string.");
        }else return new OkObjectResult(_greeterService.Greet(name));
    }
}