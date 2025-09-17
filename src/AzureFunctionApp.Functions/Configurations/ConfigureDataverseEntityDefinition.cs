using AzureFunctionApp.Infrastructure.Models.Dtos;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

namespace AzureFunctionApp.Functions.Configurations
{
    public class ConfigureDataverseEntityDefinition
    {
        public static void Configure()
        {
            DataverseEntityDefinition<EntraUser>.Load();
            DataverseEntityDefinition<Role>.Load();
            DataverseEntityDefinition<SystemUser>.Load();
        }
    }
}
