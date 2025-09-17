using AzureFunctionApp.Infrastructure.Repositories.Dataverse;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionApp.Functions.Configurations
{
    public static class ConfigureRepository
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddScoped<IEntraUserRepository, EntraUserRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<ISystemUserRepository, SystemUserRepository>();
        }
    }
}
