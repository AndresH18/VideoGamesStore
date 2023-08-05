using GameStore.Data.Identity;
using Microsoft.AspNetCore.Identity;

namespace GameStore.Data;
#pragma warning disable CS8604

/// <summary>
/// Use this class to generate initial data for Identity on <see cref="ApplicationContext"/>
/// </summary>
public static class IdentityInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // manages Identity Roles
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        // manages identity Users
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        // get configuration to use AdminCredentials
        var config = serviceProvider.GetRequiredService<IConfiguration>();

        // Verify main root admin
        var user = await userManager.FindByEmailAsync(config["AdminCredentials:Email"]);
        if (user == null)
        {
            user = new ApplicationUser
            {
                UserName = config["AdminCredentials:UserName"],
                Email = config["AdminCredentials:Email"],
                EmailConfirmed = true,
            };
            await userManager.CreateAsync(user, config["AdminCredentials:Password"]);
            user = await userManager.FindByEmailAsync(user.Email);
        }

        var rootRole = await roleManager.FindByNameAsync("root");
        var adminRole = await roleManager.FindByNameAsync("admin");
        var userRole = await roleManager.FindByNameAsync("user");

        if (rootRole == null)
        {
            rootRole = new ApplicationRole("root");
            await roleManager.CreateAsync(rootRole);
        }

        if (adminRole == null)
        {
            adminRole = new ApplicationRole("admin");
            await roleManager.CreateAsync(adminRole);
        }

        if (userRole == null)
        {
            // create user role
            userRole = new ApplicationRole("user");
            await roleManager.CreateAsync(userRole);
        }

        await userManager.AddToRolesAsync(user, new[] {"root", "admin", "user"});
    }
#pragma warning restore CS8604
}