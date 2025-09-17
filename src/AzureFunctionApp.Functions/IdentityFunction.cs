using AzureFunctionApp.Core.Services;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest;
using AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Providers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Net;

namespace AzureFunctionApp.Functions;

public class IdentityFunction 
    (
        ILogger<IdentityFunction> _logger,
        IEntraUserService _entraUserService,
        IRoleService _roleService,
        ISystemUserService _systemUserService,
        ClaimProvider _claimProvider
    )
{
    [OpenApiOperation(operationId: "GetAllEntraUsers", tags: new[] { "EntraUser" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<EntraUser>), Description = "Entra user list.")]
    [Function("GetAllEntraUsers")]
    public IActionResult GetAllEntraUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "entra-users")] HttpRequestData req)
    {
        return new OkObjectResult(_entraUserService.GetAllEntraUsers());
    }

    [OpenApiOperation(operationId: "GetEntraUserById", tags: new[] { "EntraUser" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(EntraUser), Description = "Entra user.")]
    [Function("GetEntraUserById")]
    public IActionResult GetEntraUserById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "entra-user/{userId}")] HttpRequestData req, Guid userId)
    {
        return new OkObjectResult(_entraUserService.GetEntraUserById(userId));
    }

    [OpenApiOperation(operationId: "GetAllRoles", tags: new[] { "Role" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Role>), Description = "Role list.")]
    [Function("GetAllRoles")]
    public IActionResult GetAllRoles([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "roles")] HttpRequestData req)
    {
        return new OkObjectResult(_roleService.GetAllUserRoles());
    }

    [OpenApiOperation(operationId: "GetRolesByUserId", tags: new[] { "Role" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Role), Description = "Role")]
    [Function("GetRolesByUserId")]
    public IActionResult GetRolesByUserId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "roles/user/{userId}")] HttpRequestData req, Guid userId)
    {
        return new OkObjectResult(_roleService.GetUserRolesByUserId(userId));
    }

    [OpenApiOperation(operationId: "GetAllSystemUsers", tags: new[] { "SystemUser" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<SystemUser>), Description = "System user list.")]
    [Function("GetAllSystemUsers")]
    public IActionResult GetAllSystemUsers([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "system-users")] HttpRequestData req)
    {
        return new OkObjectResult(_systemUserService.GetAllSystemUsers());
    }

    [OpenApiOperation(operationId: "GetSystemUserById", tags: new[] { "SystemUser" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiParameter(name: "userId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemUser), Description = "System user.")]
    [Function("GetSystemUserById")]
    public IActionResult GetSystemUserById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "system-user/{userId}")] HttpRequestData req, Guid userId)
    {
        return new OkObjectResult(_systemUserService.GetSystemUserById(userId));
    }

    [OpenApiOperation(operationId: "GetSystemUserByEntraObjectId", tags: new[] { "SystemUser" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiParameter(name: "entraObjectId", In = ParameterLocation.Path, Required = true, Type = typeof(Guid))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(SystemUser), Description = "System user.")]
    [Function("GetSystemUserByEntraObjectId")]
    public IActionResult GetSystemUserByEntraObjectId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "system-user/entra-object-id/{entraObjectId}")] HttpRequestData req, Guid entraObjectId)
    {
        return new OkObjectResult(_systemUserService.GetSystemUserByEntraObjectId(entraObjectId));
    }

    [OpenApiOperation(operationId: "ProvideCustomClaims", tags: new[] { "ExtensionAuthentication" })]
    [OpenApiSecurity(schemeType: SecuritySchemeType.Http, schemeName: "Bearer",
        Scheme = Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums.OpenApiSecuritySchemeType.Bearer, BearerFormat = "JWT")]
    [OpenApiRequestBody("application/json", typeof(ExtensionRequest), Description = "Extension request payload", Required = true)]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(ExtensionResponse), Description = "System user.")]
    [Function("ProvideCustomClaims")]
    public async Task<IActionResult> ProvideCustomClaims([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "extension-authentication/custom-claims")] HttpRequestData req)
    {
        var body = await new StreamReader(req.Body).ReadToEndAsync();
        var extensionRequest = JsonConvert.DeserializeObject<ExtensionRequest>(body);
        return new OkObjectResult(_claimProvider.GetCustomClaims(extensionRequest ?? throw new BadHttpRequestException("Body must be not null")));
    }
}