using AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionRequest;
using AzureFunctionApp.Infrastructure.Models.Dtos.ExtensionAuthentication.ExtensionResponse;
using AzureFunctionApp.Infrastructure.Models.Entities.Dataverse;
using AzureFunctionApp.Infrastructure.Repositories.Dataverse;


namespace AzureFunctionApp.Infrastructure.Providers;
public class ClaimProvider 
    (
        ISystemUserRepository _systemUserRepository,
        IRoleRepository _roleRepository
    )
{

    public ExtensionResponse GetCustomClaims(ExtensionRequest request)
    {
        ExtensionResponse response = new ExtensionResponse();
        Guid userId = request?.Data?.AuthenticationContext?.User?.Id ?? Guid.Empty;
        if (userId == Guid.Empty) return response;
        SystemUser? user = _systemUserRepository.GetSystemUserByEntraObjectId(userId);
        if (user != null)
        {
            List<Role> roles = _roleRepository.GetRolesByUserId(user.Id);
            Claims claims = new Claims();
            foreach (Role role in roles)
            {
                claims.CustomRoles.Add(role.RoleName ?? string.Empty);
            }
            Models.Dtos.ExtensionAuthentication.ExtensionResponse.Action action = new();
            action.Claims = claims;
            response.Data.Actions.Add(action);
        }

        return response;
    }
}