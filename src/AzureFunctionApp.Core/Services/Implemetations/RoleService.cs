using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using AzureFunctionApp.Infrastructure.Repositories.Dataverse;

namespace AzureFunctionApp.Core.Services.Implementations;

public class RoleService 
    (
        IRoleRepository _roleRepository
    ) : IRoleService
{

    public List<Role> GetAllUserRoles()
    {
        return _roleRepository.FindAll().Result;
    }

    public List<Role> GetUserRolesByUserId(Guid userId)
    {
        return _roleRepository.GetRolesByUserId(userId);
    }
}