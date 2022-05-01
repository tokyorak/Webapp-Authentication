using Microsoft.AspNetCore.Identity;

namespace WebApp1.Data;

public static class SeedData
{
    public static void Seed(IServiceProvider serviceProvider)
    {
        UserManager<IdentityUser> userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();
        RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        SeedRole(roleManager);
        SeedUsers(userManager);
    }

    private static void SeedUsers(UserManager<IdentityUser> userManager)
    {
        if(userManager.FindByNameAsync("toky@localhost").Result == null)
        {
            var user = new IdentityUser
            {
                UserName = "toky@localhost",
                Email = "toky@localhost"
            };
            var result = userManager.CreateAsync(user, "123456789").Result;

            if(result.Succeeded)
            {
                userManager.AddToRoleAsync(user, "Administrator").Wait();
            }
        }
    }

    private static void SeedRole(RoleManager<IdentityRole> roleManager)
    {
        if(!roleManager.RoleExistsAsync("Administrator").Result)
        {
            var role = new IdentityRole
            {
                Name = "Administrator"
            };
            var result = roleManager.CreateAsync(role).Result;
        }

        if(!roleManager.RoleExistsAsync("Normal_User").Result)
        {
            var role = new IdentityRole
            {
                Name = "Normal_User"
            };
            var result = roleManager.CreateAsync(role).Result;
        }
    }
}