using AzureFunctionApp.Core.Providers;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;

namespace AzureFunctionApp.Core.Services.Implementations;

public class UserRoleService : IUserRoleService
{
    private readonly ServiceClient _serviceClient;

    public UserRoleService(ServiceClientProvier serviceClientProvier)
    {
        _serviceClient = serviceClientProvier.GetServiceClient();
    }

    public List<UserRole> GetAllUserRoles()
    {
        throw new NotImplementedException();
    }

    public List<UserRole> GetUserRolesByUserId(Guid userId)
    {
        throw new NotImplementedException();
    }
}