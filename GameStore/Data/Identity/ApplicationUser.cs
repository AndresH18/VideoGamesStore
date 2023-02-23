using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618


namespace GameStore.Data.Identity;

// Add profile data for application users by adding properties to the GameStoreUser class
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class ApplicationUser : IdentityUser
{
    // public virtual ICollection<ApplicationUserClaim> Claims { get; set; }
    // public virtual ICollection<ApplicationUserLogin> Logins { get; set; }
    // public virtual ICollection<ApplicationUserToken> Tokens { get; set; }
    // public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}