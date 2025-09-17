using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.Xrm.Sdk.Query;

namespace AzureFunctionApp.Infrastructure.Repositories.Dataverse
{
    public interface IRoleRepository : IDataverseGenericRepository<Role>
    {
        List<Role> GetRolesByUserId(Guid userId);
    }

    public class RoleRepository : DataverseGenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(ServiceClientProvier serviceClientProvier, ILogger<DataverseGenericRepository<Role>> logger) : base(serviceClientProvier, logger)
        {
        }

        public List<Role> GetRolesByUserId(Guid userId)
        {
            var query = new QueryExpression(_tableName)
            {
                ColumnSet = new ColumnSet(true)
            };

            LinkEntity linkUserRoles = query.AddLink("cr778_userandrolejoin", "cr778_roleid", "cr778_roleid");
            linkUserRoles.LinkCriteria.AddCondition("cr778_systemuser", ConditionOperator.Equal, userId);
            return DataverseUtils.ConvertTo<Role>([.. _serviceClient.RetrieveMultiple(query).Entities]);
        }
    }
}
