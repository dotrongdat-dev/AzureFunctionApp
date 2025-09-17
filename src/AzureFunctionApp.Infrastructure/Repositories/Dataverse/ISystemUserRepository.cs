using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk.Query;

namespace AzureFunctionApp.Infrastructure.Repositories.Dataverse
{
    public interface ISystemUserRepository : IDataverseGenericRepository<SystemUser>
    {
        SystemUser? GetSystemUserByEntraObjectId(Guid entraObjectId);
    }

    public class SystemUserRepository : DataverseGenericRepository<SystemUser>, ISystemUserRepository
    {
        public SystemUserRepository(ServiceClientProvier serviceClientProvier, ILogger<DataverseGenericRepository<SystemUser>> logger) : base(serviceClientProvier, logger)
        {
        }

        public SystemUser? GetSystemUserByEntraObjectId(Guid entraObjectId)
        {
            var query = new QueryExpression(_tableName)
            {
                ColumnSet = new ColumnSet(true)
            };

            query.Criteria.AddCondition("azureactivedirectoryobjectid", ConditionOperator.Equal, entraObjectId);

            return DataverseUtils.ConvertTo<SystemUser>(_serviceClient.RetrieveMultiple(query).Entities.FirstOrDefault());
        }
    }
}
