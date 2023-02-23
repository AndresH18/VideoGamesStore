using GameStore.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Data;
#pragma warning disable CS8604

/// <summary>
/// Use this class to generate initial data for Identity on <see cref="UsersContext"/>
/// </summary>
public static class IdentityInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // manages Identity Roles
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        // manages identity Users
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        // verify if there are any roles, if the database is new (empty) it should not contain any roles and would want to add data
        if (!roleManager.Roles.ToList().Any())
        {
            // admin role
            var adminRole = new ApplicationRole("admin");
            ApplicationUser? adminUser;
            // create admin role
            var result = await roleManager.CreateAsync(adminRole);
            if (result.Succeeded)
            {
                // get configuration to use AdminCredentials
                var config = serviceProvider.GetRequiredService<IConfiguration>();
                adminUser = await userManager.FindByEmailAsync(config["AdminCredentials:Email"]);
                if (adminUser == null)
                {
                    adminUser = new ApplicationUser
                    {
                        UserName = config["AdminCredentials:UserName"],
                        Email = config["AdminCredentials:Email"], EmailConfirmed = true,
                    };
                    result = await userManager.CreateAsync(adminUser, config["AdminCredentials:Password"]);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                }
            }
        }
    }
#pragma warning restore CS8604
}