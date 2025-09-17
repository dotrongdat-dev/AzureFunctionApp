using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Repositories.Dataverse;

namespace AzureFunctionApp.Core.Services;

public class EntraUserService
    (
        IEntraUserRepository _entraUserRepository
    ) : IEntraUserService
{
    public List<EntraUser> GetAllEntraUsers()
    {
        return _entraUserRepository.FindAll().Result;
    }

    public EntraUser? GetEntraUserById(Guid id)
    {
        return _entraUserRepository.FindById(id).Result;
    }
}