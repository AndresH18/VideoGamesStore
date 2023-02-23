using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618


namespace GameStore.Data.Identity;
// ReSharper disable once ClassNeverInstantiated.Global

public class ApplicationUserClaim : IdentityUserClaim<string>
{
    public virtual ApplicationUser User { get; set; }
}