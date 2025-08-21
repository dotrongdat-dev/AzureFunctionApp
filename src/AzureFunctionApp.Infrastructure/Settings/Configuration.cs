namespace AzureFunctionApp.Infrastructure.Settings;

public record Configuration
{
    public required AzureCosmosDB AzureCosmosDB { get; init; }
}

public record AzureCosmosDB
{
    public required string Endpoint { get; init; }

    public required string DatabaseName { get; init; }

    public required string ContainerName { get; init; }
}