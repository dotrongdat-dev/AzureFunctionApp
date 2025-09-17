namespace AzureFunctionApp.Infrastructure.CustomAttributes;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public sealed class DataverseTableAttribute : Attribute
{
    public string? Prefix { get; set; }
    public string Name { get; set; }
    public bool ApplyPrefixToColumn { get; set; } = false;

    public string GetTableName()
    {
        return $"{Prefix ?? ""}{Name ?? ""}";
    }
}