using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

namespace AzureFunctionApp.Core.Services;

public interface IEntraUserService
{
    List<EntraUser> GetAllEntraUsers();
    EntraUser? GetEntraUserById(Guid id);
}