using AzureFunctionApp.Infrastructure.CustomAttributes;

namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse
{
    [DataverseTable(Name = "systemuser")]
    public class SystemUser : AbstractEntity
    {
        [DataverseColumn(Name = "fullname")]
        public string? FullName { get; set; }
        [DataverseColumn(Name = "firstname")]
        public string? FirstName { get; set; }
        [DataverseColumn(Name = "middlename")]
        public string? MiddleName { get; set; }
        [DataverseColumn(Name = "lastname")]
        public string? LastName { get; set; }
        [DataverseColumn(Name = "internalemailaddress")]
        public string? Email { get; set; }
        [DataverseColumn(Name = "homephone")]
        public string? HomePhone { get; set; }
        [DataverseColumn(Name = "importsequencenumber")]
        public long? SequenceNumber { get; set; }
        [DataverseColumn(Name = "applicationid")]
        public Guid ApplicationId { get; set; } = Guid.Empty;
        [DataverseColumn(Name = "azureactivedirectoryobjectid")]
        public Guid EntraObjectId { get; set; } = Guid.Empty;
        [DataverseColumn(Name = "issyncwithdirectory")]
        public bool IsUserSynced { get; set; } = false;
        [DataverseColumn(Name = "islicensed")]
        public bool IsLicensed { get; set; } = false;
    }
}
