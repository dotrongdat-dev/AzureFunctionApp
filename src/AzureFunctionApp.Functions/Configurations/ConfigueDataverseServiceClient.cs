using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Functions.Helpers;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Identity.Client;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace AzureFunctionApp.Functions.Configurations;

public class ConfigureDataverseServiceClient
{
    public static void Configure(IServiceCollection services)
    {
        //services.AddScoped(serviceProvider =>
        //{
        //    var settingHelper = serviceProvider.GetRequiredService<SettingHelper>();
        //    var dataverseUrl = settingHelper.GetValue<string>("DataverseURL") ?? string.Empty;
        //    AzureTernantConfig azureTernantConfig = serviceProvider.GetRequiredService<AzureTernantConfig>();
        //    //string defaultConnectionString = $@"
        //    //AuthType=ClientSecret;
        //    //Url={dataverseUrl};
        //    //ClientId={azureTernantConfig.clientId};
        //    //ClientSecret={azureTernantConfig.clientSecret};
        //    //TenantId={azureTernantConfig.ternantId}";
        //    IHttpContextAccessor accessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
        //    FunctionContext? functionContext = serviceProvider.GetService<FunctionContext>();
        //    Func<string, Task<string>> tokenProvider = async (authority) =>
        //    {
        //        //IAuthenticationService authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();
        //        HttpContext? httpContext = accessor?.HttpContext;
        //        string? authHeader = httpContext?.Request?.Headers.Authorization.FirstOrDefault();

        //        if (!string.IsNullOrEmpty(authHeader) && authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase))
        //        {
        //            var callerToken = authHeader["Bearer ".Length..];
        //            return callerToken;
        //        }

        //        // 2. Fall back to client secret
        //        var app = ConfidentialClientApplicationBuilder.Create(azureTernantConfig.clientId)
        //            .WithClientSecret(azureTernantConfig.clientSecret)
        //            .WithAuthority($"https://login.microsoftonline.com/{azureTernantConfig.ternantId}/v2.0")
        //            .Build();

        //        var result = await app.AcquireTokenForClient(new[] { $"{dataverseUrl}/.default" }).ExecuteAsync();
        //        return result.AccessToken;
        //    };

        //    return new ServiceClient(new Uri(dataverseUrl), tokenProvider);
        //});

        services.AddScoped(serviceProvider =>
        {
            var settingHelper = serviceProvider.GetRequiredService<SettingHelper>();
            var dataverseUrl = settingHelper.GetValue<string>("DataverseURL") ?? string.Empty;
            AzureTernantConfig azureTernantConfig = serviceProvider.GetRequiredService<AzureTernantConfig>();
            ServiceClientProvier serviceClientProvier = new ServiceClientProvier();
            serviceClientProvier.setDataverseUrl(dataverseUrl);
            // serviceClientProvier.setDefaultAccessToken(async () =>
            // {
            //     var app = ConfidentialClientApplicationBuilder.Create(azureTernantConfig.clientId)
            //         .WithClientSecret(azureTernantConfig.clientSecret)
            //         .WithAuthority($"https://login.microsoftonline.com/{azureTernantConfig.ternantId}/v2.0")
            //         .Build();

            //     var result = await app.AcquireTokenForClient(new[] { $"{dataverseUrl}/.default" }).ExecuteAsync();
            //     return result.AccessToken;
            // });


            string defaultConnectionString = $@"
                AuthType=ClientSecret;
                Url={dataverseUrl};
                ClientId={azureTernantConfig.clientId};
                ClientSecret={azureTernantConfig.clientSecret};
                TenantId={azureTernantConfig.ternantId}";

            serviceClientProvier.setDefaultServiceClient(new ServiceClient(defaultConnectionString));

            return serviceClientProvier;
        });
    }
}