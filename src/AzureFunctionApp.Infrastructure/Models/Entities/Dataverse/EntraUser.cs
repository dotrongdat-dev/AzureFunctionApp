using AzureFunctionApp.Infrastructure.Constants;
using AzureFunctionApp.Infrastructure.CustomAttributes;
using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
[DataverseTable(Name = "aaduser")]
public class EntraUser : AbstractEntity
{
    [DataverseColumn(Name = "businessphones")]
    public string? BusinessPhones { get; set; }
    [DataverseColumn(Name = "city")]

    public string? City { get; set; }
    [DataverseColumn(Name = "companyname")]

    public string? CompanyName { get; set; }
    [DataverseColumn(Name = "createddatetime")]

    public DateTime CreatedDateTime { get; set; }
    [DataverseColumn(Name = "displayname")]

    public string? DisplayName { get; set; }
    [DataverseColumn(Name = "givenname")]

    public string? GivenName { get; set; }
    [DataverseColumn(Name = "imaddresses")]

    public string? ImAddresses { get; set; }
    [DataverseColumn(Name = "jobtitle")]

    public string? JobTitle { get; set; }
    [DataverseColumn(Name = "mail")]

    public string? Mail { get; set; }
    [DataverseColumn(Name = "accountenabled")]

    public bool AccountEnabled { get; set; }
    [DataverseColumn(Name = "mobilephone")]

    public string? MobilePhone { get; set; }
    [DataverseColumn(Name = "officelocation")]

    public string? OfficeLocation { get; set; }
    [DataverseColumn(Name = "postalcode")]

    public string? PostalCode { get; set; }
    [DataverseColumn(Name = "preferredlanguage")]

    public string? PreferredLanguage { get; set; }
    [DataverseColumn(Name = "streetaddress")]

    public string? StreetAddress { get; set; }
    [DataverseColumn(Name = "surname")]

    public string? Surname { get; set; }
    [DataverseColumn(Name = "userprincipalname")]

    public string? UserPrincipalName { get; set; }
    [DataverseColumn(Name = "usertype")]

    public string? UserType { get; set; }

}