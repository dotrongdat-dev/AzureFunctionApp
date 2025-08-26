using System.Collections;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Functions.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApp.Functions;

public class GreeterFunction(
    ILogger<GreeterFunction> _logger,
    IGreeterService _greeterService,
    SettingHelper _settingHelper
)
{

    [Function("Greeter")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequestData req)
    {
        string? name = req.Query["name"];
        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please provide a name in the query string.");
        }
        else return new OkObjectResult(_greeterService.Greet(name));
    }

    [Function("Settings")]
    public IActionResult getSettings([HttpTrigger(AuthorizationLevel.Function, "get", Route = "/settings")] HttpRequestData req)
    {
        string? key = req.Query["key"];
        if (string.IsNullOrEmpty(key))
        {
            return new BadRequestObjectResult("Please provide a key in the query string.");
        }
        return new OkObjectResult(_settingHelper.GetValue<string>(key));
    }
}