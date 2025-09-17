using AzureFunctionApp.Infrastructure.Enums;
using AzureFunctionApp.Infrastructure.Models.Dtos;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using Microsoft.Xrm.Sdk;
using System.Reflection;

namespace AzureFunctionApp.Infrastructure.Utils;

public static class DataverseUtils
{
    public static E? ConvertTo<E>(Entity? entity) where E : AbstractEntity
    { 
        string?[] columnNames = DataverseEntityDefinition<E>.ColumnNames;
        PropertyInfo[] properties = typeof(E).GetProperties();
        return ConvertTo<E>(entity, properties, columnNames);
    }

    public static List<E?> ConvertTo<E>(List<Entity> entities) where E : AbstractEntity
    {
        List<E?> objs = new();

        string?[] columnNames = DataverseEntityDefinition<E>.ColumnNames;
        PropertyInfo[] properties = typeof(E).GetProperties();

        foreach (Entity entity in entities)
        { 
            objs.Add(ConvertTo<E>(entity, properties, columnNames));
        }

        return objs;
    }

    private static E? ConvertTo<E>(Entity? entity, PropertyInfo[] properties, string?[] columnNames) where E : AbstractEntity
    {
        if (entity == null) return null;
        E obj = Activator.CreateInstance<E>();
        obj.Id = entity.Id;
        entity.Attributes.TryGetValue("owningbusinessunit", out object obuObject);
        obj.OwningBussinessUnit = AbstractAudit.CastFrom(obuObject);
        entity.Attributes.TryGetValue("statecode", out object stateCodeObject);
        obj.StateCode = stateCodeObject == null ? null : (EDataverseStateCode?)((OptionSetValue)stateCodeObject).Value;
        entity.Attributes.TryGetValue("createdby", out object createdByObject);
        obj.CreatedBy = AbstractAudit.CastFrom(createdByObject);
        entity.Attributes.TryGetValue("createdon", out object createdOnObject);
        obj.CreatedOn = createdOnObject == null ? null : (DateTime)createdOnObject;
        entity.Attributes.TryGetValue("modifiedby", out object modifiedByObject);
        obj.ModifiedBy = AbstractAudit.CastFrom(modifiedByObject);
        entity.Attributes.TryGetValue("modifiedon", out object modifiedOnObject);
        obj.ModifiedOn = modifiedOnObject == null ? null : (DateTime)modifiedOnObject;
        entity.Attributes.TryGetValue("owninguser", out object owningUserObject);
        obj.OwningUser = AbstractAudit.CastFrom(owningUserObject);
        entity.Attributes.TryGetValue("ownerid", out object ownerIdObject);
        obj.OwnerId = AbstractAudit.CastFrom(ownerIdObject);

        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            string? columnName = columnNames[i];
            if (!string.IsNullOrEmpty(columnName))
            {
                entity.Attributes.TryGetValue(columnName, out object value);
                property.SetValue(obj, value);
            }
        }

        return obj;
    }

    private static Entity? ConvertToEntity<E>(E obj, PropertyInfo[] properties, string?[] columnNames) where E : AbstractEntity
    {
        if (obj == null) return null;
        Entity entity = new Entity();
        entity.Id = obj.Id;
        for (int i = 0; i < properties.Length; i++)
        {
            PropertyInfo property = properties[i];
            string? columnName = columnNames[i];
            if (!string.IsNullOrEmpty(columnName))
            {
                entity[columnName] = property.GetValue(obj);
            }
        }

        return entity;
    }

    public static Entity? ConvertToEntity<E>(E obj) where E : AbstractEntity
    { 
        string?[] columnNames = DataverseEntityDefinition<E>.ColumnNames;
        PropertyInfo[] properties = typeof(E).GetProperties();
        return ConvertToEntity<E>(obj, properties, columnNames);  
    }

    public static List<Entity?> ConvertToEntities<E>(List<E> objs) where E : AbstractEntity
    {
        List<Entity?> entities = new();
        string?[] columnNames = DataverseEntityDefinition<E>.ColumnNames;
        PropertyInfo[] properties = typeof(E).GetProperties();

        foreach (E obj in objs)
        {
            entities.Add(ConvertToEntity(obj, properties, columnNames));   
        }

        return entities;
    }
}