using Microsoft.AspNetCore.Identity;
#pragma warning disable CS8618


namespace GameStore.Data.Identity;
// ReSharper disable once ClassNeverInstantiated.Global

public class ApplicationRoleClaim : IdentityRoleClaim<string>
{
    public virtual ApplicationRole Role { get; set; }
}