using System.Security.Claims;
using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Functions.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace AzureFunctionApp.Functions.Middlewares;

public class AuthorizeMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var httpContext = context.GetHttpContext();

        if (httpContext != null)
        {
            if (false == httpContext.Request.Path.Value?.Contains("extension-authentication"))
            {
                ServiceClientProvier serviceClientProvier = httpContext.RequestServices.GetRequiredService<ServiceClientProvier>();
                serviceClientProvier.accessToken = GetToken(httpContext, false);
            }
            AuthorizeAttribute? authorizeAttribute = AuthUtils.GetAttribute<AuthorizeAttribute>(context);
            IEnumerable<Claim>? rolesClaim = httpContext.User.Claims.Where(x => x.Type == ClaimTypes.Role);
            if (!IsAllowed(rolesClaim, authorizeAttribute))
            {
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;
                return;
            }
        }

        await next(context);
    }

    private string? GetToken(HttpContext httpContext, bool includingBearerPrefix = true)
    {
        string? authorization = httpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authorization)) return null;
        if (includingBearerPrefix) return authorization;
        return authorization.StartsWith("Bearer ") ? authorization["Bearer ".Length..] : authorization;
    }

    private bool IsAllowed(IEnumerable<Claim>? rolesClaim, AuthorizeAttribute? authorizeAttribute)
    {
        string[]? roles = GetRoles(rolesClaim);
        string? requiredRole = authorizeAttribute?.Roles;
        return requiredRole == null || (roles != null && roles.Contains(requiredRole));
    }

    private string[]? GetRoles(IEnumerable<Claim>? claims)
    {
        if (claims == null || claims.IsEmpty()) return null;
        return [.. claims.Select(c => c.Value)];
    }
}
