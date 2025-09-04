using System.Net;
using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;

namespace AzureFunctionApp.Functions;

public class ProductFunction(
    IProductService productService
)
{
    [Function("GetAllProducts")]
    [OpenApiOperation(operationId: "GetAllProducts")]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(List<Product>),
            Description = "The OK response message containing a JSON result.")]
    public IActionResult GetAllProducts([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "products")] HttpRequestData req)
    {
        return new OkObjectResult(productService.GetProductsAsync().GetAwaiter().GetResult());
    }

    [Function("GetProductById")]
    [OpenApiOperation(operationId: "GetProductById")]
    [OpenApiParameter(name: "id", Type = typeof(string))]
    [OpenApiParameter(name: "category", Type = typeof(string))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Product),
            Description = "The OK response message containing a JSON result.")]
    public IActionResult GetProductById([HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "product/{id}/category/{category}")]
            HttpRequestData req,
            string id,
            string category
        )
    {
        return new OkObjectResult(productService.GetProductByIdAsync(id, category).GetAwaiter().GetResult());
    }

    [Authorize(Roles = "Employee")]
    [OpenApiOperation(operationId: "AddProduct", Description = "Api for create new product")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Product))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Product),
            Description = "The OK response message containing a JSON result.")]
    [Function("AddProduct")]
    public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "product")]
            HttpRequestData req
        )
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        Product? product = JsonConvert.DeserializeObject<Product>(body);
        if (product == null)
        {
            return new BadRequestObjectResult("Invalid product data.");
        }
        productService.AddProductAsync(product).GetAwaiter().GetResult();
        return new CreatedResult($"/product/{product.id}", product);
    }

    [Authorize(Roles = "Employee")]
    [OpenApiParameter(name: "id", Type = typeof(string))]
    [OpenApiParameter(name: "category", Type = typeof(string))]
    [OpenApiOperation(operationId: "UpdateProduct", Description = "Api for update product")]
    [OpenApiRequestBody(contentType: "application/json", bodyType: typeof(Product))]
    [OpenApiResponseWithBody(statusCode: HttpStatusCode.OK, contentType: "application/json", bodyType: typeof(Product),
            Description = "The OK response message containing a JSON result.")]
    [Function("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "product/{id}/category/{category}")]
            HttpRequestData req,
            string id,
            string category
        )
    {
        string body = await new StreamReader(req.Body).ReadToEndAsync();
        string? _id = JsonConvert.DeserializeObject<string>(body);
        if (_id == null)
        {
            return new BadRequestObjectResult("Invalid update product data.");
        }
        return new CreatedResult($"/product/{id}", await productService.UpdateProductAsync(id, category, new Product
        {
            id = _id,
            category = category
        }));
    }

    [Authorize(Roles = "Employee")]
    [OpenApiParameter(name: "id", Type = typeof(string))]
    [OpenApiParameter(name: "category", Type = typeof(string))]
    [OpenApiOperation(operationId: "DeleteProduct", Description = "Api for delete new product by id")]
    [OpenApiResponseWithoutBody(statusCode: HttpStatusCode.OK,Description = "The OK response message containing a JSON result.")]
    [Function("DeleteProduct")]
    public IActionResult DeleteProduct([HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "product/{id}/category/{category}")]
            HttpRequestData req,
            string id,
            string category
        )
    {
        productService.DeleteProductAsync(id, category).GetAwaiter().GetResult();
        return new NoContentResult();
    }
}
