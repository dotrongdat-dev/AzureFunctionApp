using System.Security.Claims;
using AzureFunctionApp.Functions.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Newtonsoft.Json;

namespace AzureFunctionApp.Functions.Middlewares;

public class AuthorizeMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var httpContext = context.GetHttpContext();

        if (httpContext != null)
        {
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