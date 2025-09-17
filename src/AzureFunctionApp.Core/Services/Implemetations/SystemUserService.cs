using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Repositories.Dataverse;

namespace AzureFunctionApp.Core.Services.Implemetations
{
    public class SystemUserService 
        (
            ISystemUserRepository _systemUserRepository
        )
        : ISystemUserService
    {
        public List<SystemUser> GetAllSystemUsers()
        {
            return _systemUserRepository.FindAll().Result;
        }

        public SystemUser? GetSystemUserByEntraObjectId(Guid entraObjectId)
        {
            return _systemUserRepository.GetSystemUserByEntraObjectId(entraObjectId);
        }

        public SystemUser? GetSystemUserById(Guid id)
        {
            return _systemUserRepository.FindById(id).Result;
        }
    }
}
