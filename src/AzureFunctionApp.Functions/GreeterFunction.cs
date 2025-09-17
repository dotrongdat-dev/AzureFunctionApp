using System.Collections;
using System.Net;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Functions.Helpers;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
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
    [OpenApiOperation(operationId: "GreeterForEmployee")]
    [OpenApiParameter(name: "name", Type = typeof(string), In = ParameterLocation.Query)]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
    [Function("GreeterForEmployee")]
    public IActionResult GreeterEmployee([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "greeter/employee")] HttpRequestData req)
    {
        string? name = req.Query["name"];
        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please provide a name in the query string.");
        }
        else return new OkObjectResult($"GreeterForEmployee: {_greeterService.Greet(name)}");
    }

    [Authorize(Roles = "Customer")]
    [OpenApiOperation(operationId: "GreeterForCustomer")]
    [OpenApiParameter(name: "name", Type = typeof(string), In = ParameterLocation.Query)]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
    [Function("GreeterForCustomer")]
    public IActionResult GreeterCustomer([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post", Route = "greeter/customer")] HttpRequestData req)
    {
        string? name = req.Query["name"];
        if (string.IsNullOrEmpty(name))
        {
            return new BadRequestObjectResult("Please provide a name in the query string.");
        }
        else return new OkObjectResult($"GreeterForCustomer: {_greeterService.Greet(name)}");
    }

    [Authorize]
    [OpenApiOperation(operationId: "Settings")]
    [OpenApiParameter(name: "key", Type = typeof(string), In = Microsoft.OpenApi.Models.ParameterLocation.Query)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(string),
            Description = "The OK response message containing a JSON result.")]
    [Function("Settings")]
    public IActionResult GetSettings([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "settings")] HttpRequestData req)
    {
        string? key = req.Query["key"];
        if (string.IsNullOrEmpty(key))
        {
            return new BadRequestObjectResult("Please provide a key in the query string.");
        }
        return new OkObjectResult(_settingHelper.GetValue<string>(key));
    }

    [OpenApiOperation(operationId: "Login")]
    [OpenApiRequestBody(contentType: "application/x-www-form-urlencoded", bodyType: typeof(LoginRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AuthO2Response),
            Description = "The OK response message containing a JSON result.")]
    [Function("Login")]
    public async Task<IActionResult> Login([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "login")] HttpRequestData req)
    {
        string body;
        using (var reader = new StreamReader(req.Body))
        {
            body = await reader.ReadToEndAsync();
        }

        // parse form-urlencoded body
        var form = QueryHelpers.ParseQuery(body);
        var username = form.TryGetValue("username", out var u) ? u.ToString() : string.Empty;
        var password = form.TryGetValue("password", out var p) ? p.ToString() : string.Empty;

        return new OkObjectResult(await _greeterService.Login(username, password));
    }

    [OpenApiOperation(operationId: "RefreshToken")]
    [OpenApiRequestBody(contentType: "application/x-www-form-urlencoded", bodyType: typeof(RefreshTokenRequest))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(AuthO2Response),
            Description = "The OK response message containing a JSON result.")]
    [Function("RefreshToken")]
    public async Task<IActionResult> RefreshToken([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "refresh-token")] HttpRequestData req)
    {
        string body;
        using (var reader = new StreamReader(req.Body))
        {
            body = await reader.ReadToEndAsync();
        }

        // parse form-urlencoded body
        var form = QueryHelpers.ParseQuery(body);
        var refreshToken = form.TryGetValue("refreshToken", out var u) ? u.ToString() : string.Empty;

        return new OkObjectResult(await _greeterService.RefreshToken(refreshToken));
    }
}