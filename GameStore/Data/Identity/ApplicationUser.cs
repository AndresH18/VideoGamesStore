using Microsoft.AspNetCore.Identity;

#pragma warning disable CS8618


namespace GameStore.Data.Identity;

// Add profile data for application users by adding properties to the GameStoreUser class
// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class ApplicationUser : IdentityUser
{
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<IdentityUserRole<string>> UserRoles { get; set; }
}