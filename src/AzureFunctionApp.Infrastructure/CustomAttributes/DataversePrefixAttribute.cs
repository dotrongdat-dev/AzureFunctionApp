namespace AzureFunctionApp.Infrastructure.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class DataversePrefixAttribute : Attribute
    {
        public string? Prefix { get; set; }
        public bool? Ignored { get; set; }
    }
}
