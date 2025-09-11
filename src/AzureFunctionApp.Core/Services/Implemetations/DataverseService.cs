using AzureFunctionApp.Core.Providers;
using AzureFunctionApp.Core.Services.Interfaces;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;

namespace AzureFunctionApp.Core.Services.Implementations;

public class DataverseService : IDataverseService
{
    private readonly ServiceClient _serviceClient;
    private const string DATAVERSER_PREFIX = "cr778_";

    public DataverseService(ServiceClientProvier serviceClientProvier)
    {
        _serviceClient = serviceClientProvier.GetServiceClient();
    }

    public dynamic CreateDepartment(string id, string code, string name)
    {
        var department = new Entity($"{DATAVERSER_PREFIX}department", Guid.NewGuid());
        department[$"{DATAVERSER_PREFIX}id"] = id;
        department[$"{DATAVERSER_PREFIX}code"] = code;
        department[$"{DATAVERSER_PREFIX}name"] = name;

        Guid guid = _serviceClient.Create(department);
        return department;
    }

    public dynamic CreateEmployee(string id, string code, string name, int point, string departmentId)
    {
        var employee = new Entity($"{DATAVERSER_PREFIX}employee", Guid.NewGuid());
        employee[$"{DATAVERSER_PREFIX}id"] = id;
        employee[$"{DATAVERSER_PREFIX}code"] = code;
        employee[$"{DATAVERSER_PREFIX}name"] = name;
        employee[$"{DATAVERSER_PREFIX}point"] = point;
        employee[$"{DATAVERSER_PREFIX}departmentemployee"] = new EntityReference($"{DATAVERSER_PREFIX}department", new Guid(departmentId));

        Guid guid = _serviceClient.Create(employee);
        return employee;
    }


    public dynamic CreateTask(string id, string name, string description, DateTime deadline, string employeeId)
    {
        var task = new Entity($"{DATAVERSER_PREFIX}task", Guid.NewGuid());
        task[$"{DATAVERSER_PREFIX}id"] = id;
        task[$"{DATAVERSER_PREFIX}name"] = name;
        task[$"{DATAVERSER_PREFIX}description"] = description;
        task[$"{DATAVERSER_PREFIX}deadline"] = deadline;
        task[$"{DATAVERSER_PREFIX}employeetask"] = new EntityReference($"{DATAVERSER_PREFIX}employee", new Guid(employeeId));
        Guid guid = _serviceClient.Create(task);
        return task;
    }


    public bool DeleteDepartment(string id)
    {
        try
        {
            _serviceClient.Delete($"{DATAVERSER_PREFIX}department", new Guid(id));
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool DeleteEmployee(string id)
    {
        try
        {
            _serviceClient.Delete($"{DATAVERSER_PREFIX}employee", new Guid(id));
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool DeleteTask(string id)
    {
        try
        {
            _serviceClient.Delete($"{DATAVERSER_PREFIX}task", new Guid(id));
            return true;
        }
        catch
        {
            return false;
        }
    }


    public List<dynamic> FindAllDepartments()
    {
        var query = new Microsoft.Xrm.Sdk.Query.QueryExpression($"{DATAVERSER_PREFIX}department")
        {
            ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
        };
        var entities = _serviceClient.RetrieveMultiple(query).Entities;
        return entities.Cast<dynamic>().ToList();
    }


    public List<dynamic> FindAllEmployees()
    {
        var query = new Microsoft.Xrm.Sdk.Query.QueryExpression($"{DATAVERSER_PREFIX}employee")
        {
            ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
        };
        var entities = _serviceClient.RetrieveMultiple(query).Entities;
        return entities.Cast<dynamic>().ToList();
    }


    public List<dynamic> FindAllTasks()
    {
        var query = new Microsoft.Xrm.Sdk.Query.QueryExpression($"{DATAVERSER_PREFIX}task")
        {
            ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
        };
        var entities = _serviceClient.RetrieveMultiple(query).Entities;
        return entities.Cast<dynamic>().ToList();
    }


    public dynamic? FindDepartmentById(string id)
    {
        try
        {
            return _serviceClient.Retrieve($"{DATAVERSER_PREFIX}department", new Guid(id), new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        }
        catch
        {
            return null;
        }
    }


    public dynamic? FindEmployeeById(string id)
    {
        try
        {
            return _serviceClient.Retrieve($"{DATAVERSER_PREFIX}employee", new Guid(id), new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        }
        catch
        {
            return null;
        }
    }


    public List<dynamic> FindEmployeesByDepartmentId(string departmentId)
    {
        var query = new Microsoft.Xrm.Sdk.Query.QueryExpression($"{DATAVERSER_PREFIX}employee")
        {
            ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
        };
        query.Criteria.AddCondition($"{DATAVERSER_PREFIX}departmentemployee", Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal, new Guid(departmentId));
        var entities = _serviceClient.RetrieveMultiple(query).Entities;
        return entities.Cast<dynamic>().ToList();
    }


    public dynamic? FindTaskById(string id)
    {
        try
        {
            return _serviceClient.Retrieve($"{DATAVERSER_PREFIX}task", new Guid(id), new Microsoft.Xrm.Sdk.Query.ColumnSet(true));
        }
        catch
        {
            return null;
        }
    }


    public List<dynamic> FindTasksByEmployeeId(string employeeId)
    {
        var query = new Microsoft.Xrm.Sdk.Query.QueryExpression($"{DATAVERSER_PREFIX}task")
        {
            ColumnSet = new Microsoft.Xrm.Sdk.Query.ColumnSet(true)
        };
        query.Criteria.AddCondition($"{DATAVERSER_PREFIX}employeetask", Microsoft.Xrm.Sdk.Query.ConditionOperator.Equal, new Guid(employeeId));
        var entities = _serviceClient.RetrieveMultiple(query).Entities;
        return entities.Cast<dynamic>().ToList();
    }


    public bool UpdateDepartment(string id, string name)
    {
        try
        {
            var entity = new Entity($"{DATAVERSER_PREFIX}department", new Guid(id));
            entity[$"{DATAVERSER_PREFIX}name"] = name;
            _serviceClient.Update(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool UpdateEmployee(string id, string name, int point, string departmentId)
    {
        try
        {
            var entity = new Entity($"{DATAVERSER_PREFIX}employee", new Guid(id));
            entity[$"{DATAVERSER_PREFIX}name"] = name;
            entity[$"{DATAVERSER_PREFIX}point"] = point;
            entity[$"{DATAVERSER_PREFIX}departmentemployee"] = new EntityReference($"{DATAVERSER_PREFIX}department", new Guid(departmentId));
            _serviceClient.Update(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }


    public bool UpdateTask(string id, string name, string description, DateTime deadline, string employeeId)
    {
        try
        {
            var entity = new Entity($"{DATAVERSER_PREFIX}task", new Guid(id));
            entity[$"{DATAVERSER_PREFIX}name"] = name;
            entity[$"{DATAVERSER_PREFIX}description"] = description;
            entity[$"{DATAVERSER_PREFIX}deadline"] = deadline;
            entity[$"{DATAVERSER_PREFIX}employeetask"] = new EntityReference($"{DATAVERSER_PREFIX}employee", new Guid(employeeId));
            _serviceClient.Update(entity);
            return true;
        }
        catch
        {
            return false;
        }
    }
}