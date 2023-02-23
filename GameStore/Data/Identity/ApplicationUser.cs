using Microsoft.AspNetCore.Identity;

// ReSharper disable ClassWithVirtualMembersNeverInherited.Global

#pragma warning disable CS8618


namespace GameStore.Data.Identity;

// Add profile data for application users by adding properties to the GameStoreUser class
public class ApplicationUser : IdentityUser<string>
{
    public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }
    public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }
    public virtual ICollection<IdentityUserToken<string>> Tokens { get; set; }
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}

public class ApplicationRole : IdentityRole
{
    public virtual ICollection<ApplicationUserRole> UserRoles { get; set; }
}

public class ApplicationUserRole : IdentityUserRole<string>
{
    public virtual ApplicationUser User { get; set; }
    public virtual ApplicationRole Role { get; set; }
}