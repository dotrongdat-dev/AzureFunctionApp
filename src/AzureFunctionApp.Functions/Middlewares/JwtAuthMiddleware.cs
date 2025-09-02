using AzureFunctionApp.Functions.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Middlewares;

public class JwtAuthMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var httpContext = context.GetHttpContext();
        if (true == httpContext?.Request.Path.Value?.Contains("/api/swagger"))
        {
            await next(context);
            return;
        }

        var hasAuthorize = AuthUtils.GetAttribute<AuthorizeAttribute>(context) != null;

        if (httpContext != null && hasAuthorize)
        {
            var authService = httpContext.RequestServices.GetRequiredService<IAuthenticationService>();
            var result = await authService.AuthenticateAsync(httpContext, JwtBearerDefaults.AuthenticationScheme);

            if (result.Succeeded && result.Principal != null)
            {
                httpContext.User = result.Principal; // attach user identity
            }
            else
            {
                // optional: short-circuit unauthorized requests
                httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
                return;
            }
        }
        await next(context);
    }
}