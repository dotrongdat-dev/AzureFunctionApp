using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

namespace AzureFunctionApp.Core.Services.Interfaces
{
    public interface ISystemUserService
    {
        SystemUser? GetSystemUserById(Guid id);
        SystemUser? GetSystemUserByEntraObjectId(Guid entraObjectId);
        List<SystemUser> GetAllSystemUsers();
    }
}
