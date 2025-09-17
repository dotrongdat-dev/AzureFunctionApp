namespace AzureFunctionApp.Infrastructure.Exceptions
{
    public class DataverseColumnDefinitionException : DataverseException
    {
        public DataverseColumnDefinitionException(string? message) : base(message)
        {
        }
    }
}
