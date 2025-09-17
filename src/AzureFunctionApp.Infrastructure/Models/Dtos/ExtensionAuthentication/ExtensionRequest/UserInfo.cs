using Newtonsoft.Json;

namespace AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest
{
    public class UserInfo
    {
        [JsonProperty("companyName")]
        public string? CompanyName { get; set; }

        [JsonProperty("createdDateTime")]
        public DateTime CreatedDateTime { get; set; }

        [JsonProperty("displayName")]
        public string? DisplayName { get; set; }

        [JsonProperty("givenName")]
        public string? GivenName { get; set; }

        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("mail")]
        public string? Mail { get; set; }

        [JsonProperty("onPremisesSamAccountName")]
        public string? OnPremisesSamAccountName { get; set; }

        [JsonProperty("onPremisesSecurityIdentifier")]
        public string? OnPremisesSecurityIdentifier { get; set; }

        [JsonProperty("onPremisesUserPrincipalName")]
        public string? OnPremisesUserPrincipalName { get; set; }

        [JsonProperty("preferredLanguage")]
        public string? PreferredLanguage { get; set; }

        [JsonProperty("surname")]
        public string? Surname { get; set; }

        [JsonProperty("userPrincipalName")]
        public string? UserPrincipalName { get; set; }

        [JsonProperty("userType")]
        public string? UserType { get; set; }
    }
}
