namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
public class EntraUser
{
    public Guid Id { get; set; }
    public string? BusinessPhones { get; set; }
    public string? City { get; set; }
    public string? CompanyName { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string? DisplayName { get; set; }
    public string? GivenName { get; set; }
    public string? ImAddresses { get; set; }
    public string? JobTitle { get; set; }
    public string? Mail { get; set; }
    public bool AccountEnabled { get; set; }
    public string? MobilePhone { get; set; }
    public string? OfficeLocation { get; set; }
    public string? PostalCode { get; set; }
    public string? PreferredLanguage { get; set; }
    public string? StreetAddress { get; set; }
    public string? Surname { get; set; }
    public string? UserPrincipalName { get; set; }
    public string? UserType { get; set; }

}