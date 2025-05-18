using Microsoft.AspNetCore.Identity;

namespace GymManager.Models.Identity;

public class IdentityDataInitializer
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManger = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
        var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

        foreach (var roleName in RoleConstants.AllRoles)
        {
            if (!await roleManger.RoleExistsAsync(roleName))
            {
                await roleManger.CreateAsync(new IdentityRole(roleName));
            }
        }
        
        var adminEmail = "admin@gmail.com";
        var adminUser = await userManager.FindByEmailAsync(adminEmail);

        if (adminUser == null)
        {
            var admin = new ApplicationUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                EmailConfirmed = true
            };

            var result = await userManager.CreateAsync(adminUser, "Admin123!");
            if (result.Succeeded)
            {
                await userManager.AddToRoleAsync(adminUser, "Admin");
            }
        }
    }
}