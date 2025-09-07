namespace AzureFunctionApp.Core.Services.Interfaces;

public interface IDataverseService
{
    List<dynamic> FindAllEmployees();
    List<dynamic> FindEmployeesByDepartmentId(string departmentId);
    dynamic? FindEmployeeById(string id);
    dynamic CreateEmployee(string id, string code, string name, int point, string departmentId);
    bool UpdateEmployee(string id, string name, int point, string departmentId);
    bool DeleteEmployee(string id);

    List<dynamic> FindAllDepartments();
    dynamic? FindDepartmentById(string id);
    dynamic CreateDepartment(string id, string code, string name);
    bool UpdateDepartment(string id, string name);
    bool DeleteDepartment(string id);

    List<dynamic> FindAllTasks();
    List<dynamic> FindTasksByEmployeeId(string employeeId);
    dynamic? FindTaskById(string id);
    dynamic CreateTask(string id, string name, string description, DateTime deadline, string employeeId);
    bool UpdateTask(string id, string name, string description, DateTime deadline, string employeeId);
    bool DeleteTask(string id);
}