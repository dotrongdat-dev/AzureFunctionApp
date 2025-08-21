using AzureFunctionApp.Core.Services.Interfaces;
using AzureFunctionApp.Infrastructure.Models.Entities;
using Microsoft.Azure.Cosmos;
using Microsoft.Azure.Cosmos.Linq;

namespace AzureFunctionApp.Core.Services.Implementations;

public class ProductService(
    CosmosClient cosmosClient
) : IProductService
{
    private readonly Database _database = cosmosClient.GetDatabase("cosmicworks");
    private readonly Container _container = cosmosClient.GetContainer("cosmicworks", "products");

    public Task AddProductAsync(Product product)
    {
        return _container.CreateItemAsync(product, new PartitionKey(product.category));
    }

    public Task DeleteProductAsync(string id, string category)
    {
        return _container.DeleteItemAsync<Product>(id, new PartitionKey(category));
    }

    public Task<Product> GetProductByIdAsync(string id, string category)
    {
        return _container.ReadItemAsync<Product>(id, new PartitionKey(category)).ContinueWith(task =>
        {
            if (task.IsCompletedSuccessfully)
            {
                return task.Result.Resource;
            }
            else
            {
                throw new Exception($"Error retrieving product with id {id}: {task.Exception?.Message}");
            }
        });
    }

    public Task<IEnumerable<Product>> GetProductsAsync()
    {
        return _container.GetItemLinqQueryable<Product>(true)
            .ToFeedIterator()
            .ReadNextAsync()
            .ContinueWith(task =>
            {
                if (task.IsCompletedSuccessfully)
                {
                    return task.Result.Select(item => item).AsEnumerable();
                }
                else
                {
                    throw new Exception($"Error retrieving products: {task.Exception?.Message}");
                }
            });
    }

    public async Task<Product> UpdateProductAsync(string id, string category, Product product)
    {
        Product _product = GetProductByIdAsync(id, category).GetAwaiter().GetResult() ?? throw new Exception($"Product with id {id} not found.");
        _product.id = product.id;
        await _container.UpsertItemAsync(_product, new PartitionKey(product.category));
        return product;
    }
}