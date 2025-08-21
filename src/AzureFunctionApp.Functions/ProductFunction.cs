using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace AzureFunctionApp.Functions;

public class ProductFunction(
    IProductService productService
)
{
    [Function("GetAllProducts")]
    public IActionResult GetAllProducts([HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequestData req)
    {
        return new OkObjectResult(productService.GetProductsAsync().GetAwaiter().GetResult());
    }

    [Function("GetProductById")]
    public IActionResult GetProductById([HttpTrigger(AuthorizationLevel.Function, "get", Route = "product/{id}/category/{category}")]
            HttpRequestData req,
            string id,
            string category
        )
    {
        return new OkObjectResult(productService.GetProductByIdAsync(id, category).GetAwaiter().GetResult());
    }

    [Function("AddProduct")]
    public async Task<IActionResult> AddProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = "product")]
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

    [Function("UpdateProduct")]
    public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "product/{id}/category/{category}")]
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

    [Function("DeleteProduct")]
    public IActionResult DeleteProduct([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "product/{id}/category/{category}")]
            HttpRequestData req,
            string id,
            string category
        )
    {
        productService.DeleteProductAsync(id, category).GetAwaiter().GetResult();
        return new NoContentResult();
    }
}