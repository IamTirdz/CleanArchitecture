using Clean.Architecture.Common.Enums;
using System.Security.Claims;

namespace Clean.Architecture.Common.Helpers;

public static class ClaimsHelper
{
    public static Claim GetUserClaim(ClaimsPrincipal principal)
    {
        principal = principal ?? throw new ArgumentNullException(nameof(principal));

        var userClaim = principal.FindFirst("id");
        if (userClaim == null)
            throw new KeyNotFoundException("Invalid");

        return userClaim;
    }

    public static Guid? GetUserId(ClaimsPrincipal principal)
    {
        principal = principal ?? throw new ArgumentNullException(nameof(principal));

        var claim = principal.FindFirst("id");

        return claim != null && Guid.TryParse(claim.Value, out var userId) ? userId : null;
    }

    public static RoleType? GetUserRole(ClaimsPrincipal principal)
    {
        principal = principal ?? throw new ArgumentNullException(nameof(principal));

        var userClaims = principal.Claims.Where(c => c.Type == ClaimTypes.Role);
        if (Enum.TryParse(userClaims.FirstOrDefault()!.Value, true, out RoleType userRole))
            return userRole;

        return null;
    }
}
