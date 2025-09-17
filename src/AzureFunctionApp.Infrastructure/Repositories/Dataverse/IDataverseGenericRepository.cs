using AzureFunctionApp.Infrastructure.CustomAttributes;
using AzureFunctionApp.Infrastructure.Exceptions;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Providers;
using AzureFunctionApp.Infrastructure.Utils;
using Microsoft.Extensions.Logging;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace AzureFunctionApp.Infrastructure.Repositories.Dataverse
{
    public interface IDataverseGenericRepository<E> where E : AbstractEntity
    {
        Task<E?> FindById(Guid id);
        Task<List<E>> FindAll();
        Task<E> Save(E entity);
        Task<bool> Update(E entity);
        Task<bool>Delete(Guid id);
    }

    public abstract class DataverseGenericRepository<E> : IDataverseGenericRepository<E> where E : AbstractEntity
    {
        private readonly ILogger<DataverseGenericRepository<E>> _logger;

        protected readonly ServiceClient _serviceClient;
        private readonly DataverseTableAttribute? _tableAttribute;
        private readonly DataverseColumnAttribute?[] _columnAttributes;

        protected readonly string? _tableName;
        protected readonly string?[] _columnNames;

        public DataverseGenericRepository(
            ServiceClientProvier serviceClientProvier,
            ILogger<DataverseGenericRepository<E>> logger)
        {
            _logger = logger;
            _serviceClient = serviceClientProvier.GetServiceClient();
            _tableAttribute = DataverseEntityDefinition<E>.TableAttribute;
            _tableName = DataverseEntityDefinition<E>.TableName;
            _columnAttributes = DataverseEntityDefinition<E>.ColumnAttributes;
            _columnNames = DataverseEntityDefinition<E>.ColumnNames;
        }

        public async Task<bool> Delete(Guid id)
        {
            try
            {
                await _serviceClient.DeleteAsync(_tableName, id);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at delete dataverse entity {entity} with id {id} => {ex}", typeof(E).FullName, id, ex);
                throw;
            }
        }

        public async Task<List<E>> FindAll()
        {
            try
            {
                var query = new Microsoft.Xrm.Sdk.Query.QueryExpression(_tableName)
                {
                    ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
                };
                var entities = (await _serviceClient.RetrieveMultipleAsync(query)).Entities;
                return DataverseUtils.ConvertTo<E>(entities.ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at find all dataverse entity {entity} => {ex}", typeof(E).FullName, ex);
                throw;
            }
        }

        public async Task<E?> FindById(Guid id)
        {
            try
            {
                return DataverseUtils.ConvertTo<E>(await _serviceClient.RetrieveAsync(_tableName, id, new Microsoft.Xrm.Sdk.Query.ColumnSet(true)));
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at find by id dataverse entity {entity} with id {id} => {ex}", typeof(E).FullName, id, ex);
                throw;
            }
        }

        public async Task<E> Save(E entity)
        {
            try
            {
                Entity? _entity = DataverseUtils.ConvertToEntity(entity);
                if (_entity == null) throw new DataverseException("Create instance must be not null");
                return DataverseUtils.ConvertTo<E>(await _serviceClient.CreateAndReturnAsync(_entity));
            }
            catch (Exception ex) 
            {
                _logger.LogError("Error at save dataverse entity {entity} => {ex}", typeof(E).FullName, ex);
                throw;
            }
        }

        public async Task<bool> Update(E entity)
        {
            try
            {
                Entity? _entity = DataverseUtils.ConvertToEntity(entity);
                if (_entity == null) throw new DataverseException("Create instance must be not null");
                await _serviceClient.UpdateAsync(_entity);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error at save dataverse entity {entity} => {ex}", typeof(E).FullName, ex);
                throw;
            }
        }
    }
}
