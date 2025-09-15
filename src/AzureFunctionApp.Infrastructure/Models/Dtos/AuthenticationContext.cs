namespace AzureFunctionApp.Infrastructure.Models.Dtos;

public class AuthenticationContext
{
    public Guid? CorrelationId { get; set; }
    public Client? Client { get; set; }
    public UserClaim? User { get; set; }
}

public class Client
{
    public string? Ip { get; set; }
    public string? Locale { get; set; }
    public string? Market { get; set; }
}