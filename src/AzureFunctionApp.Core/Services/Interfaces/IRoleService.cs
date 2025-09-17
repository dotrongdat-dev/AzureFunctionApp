using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

namespace AzureFunctionApp.Core.Services.Interfaces;

public interface IRoleService
{
    List<Role> GetAllUserRoles();
    List<Role> GetUserRolesByUserId(Guid userId);
}