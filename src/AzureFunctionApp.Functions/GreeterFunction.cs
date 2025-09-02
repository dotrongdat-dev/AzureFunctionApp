using System.Collections;
using System.Net;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Functions.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace AzureFunctionApp.Functions;

public class GreeterFunction(
    ILogger<GreeterFunction> _logger,
    IGreeterService _greeterService,
    SettingHelper _settingHelper
)
{

    [Authorize(Roles = "Employee")]
    [OpenApiOperation(operationId: "Greeter")]
    [OpenApiParameter(name: "name", Type = typeof(string), In = ParameterLocation.Query)]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]    
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
    [Function("Greeter")]
    public IActionResult Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = null)] HttpRequestData req)
    {
        string? name = req.Query["name"];
        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please provide a name in the query string.");
        }
        else return new OkObjectResult(_greeterService.Greet(name));
    }

    [Authorize]
    [OpenApiOperation(operationId: "Settings")]
    [OpenApiParameter(name: "key", Type = typeof(string), In = Microsoft.OpenApi.Models.ParameterLocation.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
    [Function("Settings")]
    public IActionResult getSettings([HttpTrigger(AuthorizationLevel.Function, "get", Route = "settings")] HttpRequestData req)
    {
        string? key = req.Query["key"];
        if (string.IsNullOrEmpty(key))
        {
            return new BadRequestObjectResult("Please provide a key in the query string.");
        }
        return new OkObjectResult(_settingHelper.GetValue<string>(key));
    }
}