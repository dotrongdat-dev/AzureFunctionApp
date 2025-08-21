using AzureFunctionApp.Infrastructure.Models.Entities;

namespace AzureFunctionApp.Core.Services.Interfaces;

public interface IProductService
{
    Task<IEnumerable<Product>> GetProductsAsync();
    Task<Product> GetProductByIdAsync(string id, string category);
    Task AddProductAsync(Product product);
    Task<Product> UpdateProductAsync(string id, string category, Product product);
    Task DeleteProductAsync(string id, string category);
}