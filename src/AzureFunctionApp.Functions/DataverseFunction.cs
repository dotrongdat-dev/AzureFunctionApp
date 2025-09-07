using System.Net;
using AzureFunctionApp.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;

namespace AzureFunctionApp.Functions;

public class DataverseFunction(IDataverseService _dataverseService)
{
	// Departments
	//[Authorize]
	[OpenApiOperation(operationId: "GetAllDepartments", tags: new[] { "Department" })]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "List all departments.")]
	[Function("GetAllDepartments")]
	public IActionResult GetAllDepartments([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "departments")] HttpRequestData req)
		=> new OkObjectResult(_dataverseService.FindAllDepartments());

	//[Authorize]
	[OpenApiOperation(operationId: "GetDepartmentById", tags: new[] { "Department" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Get department by id.")]
	[Function("GetDepartmentById")]
	public IActionResult GetDepartmentById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "department/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.FindDepartmentById(id));

	//[Authorize]
	[OpenApiOperation(operationId: "CreateDepartment", tags: new[] { "Department" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Department create payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Created department.")]
	[Function("CreateDepartment")]
	public async Task<IActionResult> CreateDepartment([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "department")] HttpRequestData req)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, string>>();
		var result = _dataverseService.CreateDepartment(data["id"], data["code"], data["name"]);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "UpdateDepartment", tags: new[] { "Department" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Department update payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Update result.")]
	[Function("UpdateDepartment")]
	public async Task<IActionResult> UpdateDepartment([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "department/{id}")] HttpRequestData req, string id)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, string>>();
		var result = _dataverseService.UpdateDepartment(id, data["name"]);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "DeleteDepartment", tags: new[] { "Department" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Delete result.")]
	[Function("DeleteDepartment")]
	public IActionResult DeleteDepartment([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "department/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.DeleteDepartment(id));

	// Employees
	//[Authorize]
	[OpenApiOperation(operationId: "GetAllEmployees", tags: new[] { "Employee" })]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "List all employees.")]
	[Function("GetAllEmployees")]
	public IActionResult GetAllEmployees([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employees")] HttpRequestData req)
		=> new OkObjectResult(_dataverseService.FindAllEmployees());

	//[Authorize]
	[OpenApiOperation(operationId: "GetEmployeeById", tags: new[] { "Employee" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Get employee by id.")]
	[Function("GetEmployeeById")]
	public IActionResult GetEmployeeById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employee/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.FindEmployeeById(id));

	//[Authorize]
	[OpenApiOperation(operationId: "GetEmployeesByDepartmentId", tags: new[] { "Employee" })]
	[OpenApiParameter(name: "departmentId", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Get employees by department id.")]
	[Function("GetEmployeesByDepartmentId")]
	public IActionResult GetEmployeesByDepartmentId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "department/{departmentId}/employees")] HttpRequestData req, string departmentId)
		=> new OkObjectResult(_dataverseService.FindEmployeesByDepartmentId(departmentId));

	//[Authorize]
	[OpenApiOperation(operationId: "CreateEmployee", tags: new[] { "Employee" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Employee create payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Created employee.")]
	[Function("CreateEmployee")]
	public async Task<IActionResult> CreateEmployee([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "employee")] HttpRequestData req)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, object>>();
		var result = _dataverseService.CreateEmployee(
			data["id"].ToString(),
			data["code"].ToString(),
			data["name"].ToString(),
			Convert.ToInt32(data["point"]),
			data["departmentId"].ToString()
		);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "UpdateEmployee", tags: new[] { "Employee" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Employee update payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Update result.")]
	[Function("UpdateEmployee")]
	public async Task<IActionResult> UpdateEmployee([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "employee/{id}")] HttpRequestData req, string id)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, object>>();
		var result = _dataverseService.UpdateEmployee(
			id,
			data["name"].ToString(),
			Convert.ToInt32(data["point"]),
			data["departmentId"].ToString()
		);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "DeleteEmployee", tags: new[] { "Employee" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Delete result.")]
	[Function("DeleteEmployee")]
	public IActionResult DeleteEmployee([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "employee/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.DeleteEmployee(id));

	// Tasks
	//[Authorize]
	[OpenApiOperation(operationId: "GetAllTasks", tags: new[] { "Task" })]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "List all tasks.")]
	[Function("GetAllTasks")]
	public IActionResult GetAllTasks([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "tasks")] HttpRequestData req)
		=> new OkObjectResult(_dataverseService.FindAllTasks());

	//[Authorize]
	[OpenApiOperation(operationId: "GetTaskById", tags: new[] { "Task" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Get task by id.")]
	[Function("GetTaskById")]
	public IActionResult GetTaskById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "task/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.FindTaskById(id));

	//[Authorize]
	[OpenApiOperation(operationId: "GetTasksByEmployeeId", tags: new[] { "Task" })]
	[OpenApiParameter(name: "employeeId", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Get tasks by employee id.")]
	[Function("GetTasksByEmployeeId")]
	public IActionResult GetTasksByEmployeeId([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "employee/{employeeId}/tasks")] HttpRequestData req, string employeeId)
		=> new OkObjectResult(_dataverseService.FindTasksByEmployeeId(employeeId));

	//[Authorize]
	[OpenApiOperation(operationId: "CreateTask", tags: new[] { "Task" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Task create payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(object), Description = "Created task.")]
	[Function("CreateTask")]
	public async Task<IActionResult> CreateTask([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "task")] HttpRequestData req)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, object>>();
		var result = _dataverseService.CreateTask(
			data["id"].ToString(),
			data["name"].ToString(),
			data["description"].ToString(),
			DateTime.Parse(data["deadline"].ToString()),
			data["employeeId"].ToString()
		);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "UpdateTask", tags: new[] { "Task" })]
	[OpenApiRequestBody("application/json", typeof(object), Description = "Task update payload")]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Update result.")]
	[Function("UpdateTask")]
	public async Task<IActionResult> UpdateTask([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "task/{id}")] HttpRequestData req, string id)
	{
		var data = await req.ReadFromJsonAsync<Dictionary<string, object>>();
		var result = _dataverseService.UpdateTask(
			id,
			data["name"].ToString(),
			data["description"].ToString(),
			DateTime.Parse(data["deadline"].ToString()),
			data["employeeId"].ToString()
		);
		return new OkObjectResult(result);
	}

	//[Authorize]
	[OpenApiOperation(operationId: "DeleteTask", tags: new[] { "Task" })]
	[OpenApiParameter(name: "id", In = ParameterLocation.Path, Required = true, Type = typeof(string))]
	[OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(bool), Description = "Delete result.")]
	[Function("DeleteTask")]
	public IActionResult DeleteTask([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "task/{id}")] HttpRequestData req, string id)
		=> new OkObjectResult(_dataverseService.DeleteTask(id));
}
