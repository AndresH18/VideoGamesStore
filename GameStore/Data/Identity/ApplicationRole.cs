using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618

namespace GameStore.Data.Identity;

// ReSharper disable once ClassNeverInstantiated.Global
public class ApplicationRole : IdentityRole
{
    public virtual List<ApplicationUserRole> UserRoles { get; set; }
    public virtual List<ApplicationRoleClaim> RoleClaims { get; set; }
}