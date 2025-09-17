using AzureFunctionApp.Infrastructure.CustomAttributes;
using AzureFunctionApp.Infrastructure.Exceptions;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using Microsoft.PowerPlatform.Dataverse.Client;
using System.Reflection;

namespace AzureFunctionApp.Infrastructure.Models.Dtos
{
    public class DataverseEntityDefinition<E> where E : AbstractEntity
    {
        public static ServiceClient ServiceClient { get; private set; }
        public static DataverseTableAttribute TableAttribute { get; private set; }
        public static DataverseColumnAttribute?[] ColumnAttributes { get; private set; }

        public static string TableName { get; private set; }
        public static string?[] ColumnNames { get; private set; }

        public static void Load()
        {
            TableAttribute = typeof(E).GetCustomAttribute<DataverseTableAttribute>();

            TableName = TableAttribute?.GetTableName() ?? throw new DataverseTableDefinitionException($"Can not match Dataverse table definition for entity {typeof(E).FullName}");

            List<PropertyInfo> properties = [.. typeof(E).GetProperties()];
            ColumnAttributes = new DataverseColumnAttribute?[properties.Count];
            ColumnNames = new string[properties.Count];
            string? tablePrefix = TableAttribute?.Prefix;
            bool applyPrefixToColumn = TableAttribute?.ApplyPrefixToColumn??false;

            for (int i = 0; i < properties.Count; i++)
            {
                PropertyInfo property = properties[i];
                try
                {
                    List<Attribute> propAttributes = [.. property.GetCustomAttributes()];
                    ColumnAttributes[i] = propAttributes.OfType<DataverseColumnAttribute>().FirstOrDefault();
                    ColumnNames[i] = ColumnAttributes[i]?.GetColumnName(applyPrefixToColumn ? tablePrefix : null);
                }
                catch (DataverseColumnDefinitionException ex)
                {
                    throw new DataverseTableDefinitionException($"Can not match Dataverse column definition for column ${property.Name} of entity {typeof(E).FullName} ");
                }
            }

            Console.WriteLine($"Load DataverseEntityDefinition successfully for Entity {typeof(E).FullName}");
        }
    }
}
