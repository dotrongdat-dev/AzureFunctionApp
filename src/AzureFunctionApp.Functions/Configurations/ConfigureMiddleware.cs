using AzureFunctionApp.Functions.Middlewares;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.Hosting;

namespace AzureFunctionApp.Functions.Configurations;

public static class ConfigureMiddleware
{
    public static void AddMiddleWares(this FunctionsApplicationBuilder builder)
    {
        builder.UseMiddleware<JwtAuthMiddleware>();
        builder.UseMiddleware<AuthorizeMiddleware>();
    }
}