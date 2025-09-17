using AzureFunctionApp.Infrastructure.Constants;
using AzureFunctionApp.Infrastructure.CustomAttributes;

namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

[DataverseTable(Name = "role", Prefix = DataverseConstant.DATAVERSER_PREFIX, ApplyPrefixToColumn = true)]
public class Role : AbstractEntity
{
    [DataverseColumn(Name = "rolename")]
    public string? RoleName { get; set; }
}