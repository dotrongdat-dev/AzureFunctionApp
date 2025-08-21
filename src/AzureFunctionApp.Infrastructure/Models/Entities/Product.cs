namespace AzureFunctionApp.Infrastructure.Models.Entities;

public record Product
{
    public required string id { get; set; }
    public required string category { get; set; }
}