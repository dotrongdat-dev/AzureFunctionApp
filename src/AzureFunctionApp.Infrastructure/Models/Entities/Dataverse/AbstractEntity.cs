using AzureFunctionApp.Infrastructure.CustomAttributes;
using AzureFunctionApp.Infrastructure.Enums;

namespace AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;

public class AbstractEntity
{
    public Guid Id { get; set; }
    public AbstractAudit? OwningBussinessUnit { get; set; }
    public EDataverseStateCode? StateCode { get; set; }
    public AbstractAudit? CreatedBy { get; set; }
    public DateTime? CreatedOn { get; set; }
    public AbstractAudit? ModifiedBy { get; set; }
    public DateTime? ModifiedOn { get; set; }
    public AbstractAudit? OwningUser { get; set; }
    public AbstractAudit? OwnerId { get; set; }
}