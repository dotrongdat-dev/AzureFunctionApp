namespace AzureFunctionApp.Infrastructure.Exceptions
{
    public class DataverseTableDefinitionException : DataverseException
    {
        public DataverseTableDefinitionException(string? message) : base(message)
        {
        }
    }
}
