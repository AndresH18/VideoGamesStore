using GameStore.Data.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GameStore.Data;

public static class DataInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        // Manages Identity Roles
        var roleManager = serviceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
        // Manages Identity Users
        var userManger = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        // manages configuration
        var configuration = serviceProvider.GetRequiredService<IConfiguration>();
        // verify if there are any users 
        if (!await userManger.Users.AnyAsync())
        {
            // create admin user
            // var adminUser = ser
        }
    }
}