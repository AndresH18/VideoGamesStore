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

        await userManager.AddToRolesAsync(user, new[] { "root", "admin" });


        /*// verify if there are any roles, if the database is new (empty) it should not contain any roles and would want to add data
        if (!roleManager.Roles.ToList().Any())
        {
            // admin role
            var adminRole = new ApplicationRole("admin");
            var rootAdmin = new ApplicationRole("root");
            // create admin role
            var result = await roleManager.CreateAsync(adminRole);
            if (result.Succeeded)
            {
                // view if admin user exists
                var adminUser = await userManager.FindByEmailAsync(config["AdminCredentials:Email"]);
                if (adminUser == null)
                {
                    // create admin user
                    adminUser = new ApplicationUser
                    {
                        UserName = config["AdminCredentials:UserName"],
                        Email = config["AdminCredentials:Email"],
                        EmailConfirmed = true,
                    };
                    result = await userManager.CreateAsync(adminUser, config["AdminCredentials:Password"]);
                    if (result.Succeeded)
                    {
                        result = await userManager.AddToRoleAsync(adminUser, "Admin");
                    }

                    // crate user role
                    var userRole = new ApplicationRole("user");
                    result = await roleManager.CreateAsync(userRole);
                    result = await userManager.AddToRoleAsync(adminUser, "user");
                    result = await roleManager.CreateAsync(rootAdmin);
                    await userManager.AddToRoleAsync(adminUser, "root");
                }
            }
        }*/
    }
#pragma warning restore CS8604
}