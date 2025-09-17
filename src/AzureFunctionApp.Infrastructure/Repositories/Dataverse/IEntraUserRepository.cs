using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Providers;
using Microsoft.Extensions.Logging;

namespace AzureFunctionApp.Infrastructure.Repositories.Dataverse
{
    public interface IEntraUserRepository : IDataverseGenericRepository<EntraUser>
    {
    }

    public class EntraUserRepository : DataverseGenericRepository<EntraUser>, IEntraUserRepository
    {
        public EntraUserRepository(ServiceClientProvier serviceClientProvier, ILogger<DataverseGenericRepository<EntraUser>> logger) : base(serviceClientProvier, logger)
        {
        }
    }
}
