using AzureFunctionApp.Infrastructure.Exceptions;

namespace AzureFunctionApp.Infrastructure.CustomAttributes

{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DataverseColumnAttribute : Attribute
    {
        public string? Prefix { get; set; }
        public string Name { get; set; }

        public bool IgnoreTablePrefix { get; set; } = false;
        
        public string GetColumnName()
        {
            if (string.IsNullOrEmpty(Name)) throw new DataverseColumnDefinitionException(null);
            return $"{Prefix ?? ""}{Name ?? ""}";
        }
        public string? GetColumnName(string? tablePrefix = null)
        {
            string columnName = string.Empty;
            if (IgnoreTablePrefix) columnName = GetColumnName();
            return $"{tablePrefix ?? ""}{GetColumnName()}";
        }
    }
}
