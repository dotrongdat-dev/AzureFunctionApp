namespace AzureFunctionApp.Infrastructure.Exceptions
{
    public class DataverseException : Exception
    {
        public DataverseException(string? message) : base(message) { }
    }
}
