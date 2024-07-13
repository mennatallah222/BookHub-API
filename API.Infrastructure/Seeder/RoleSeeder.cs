using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Seeder
{
    public static class RoleSeeder
    {
        public static async Task SeedAsync(RoleManager<Role> roleManager)
        {
            var roles = await roleManager.Roles.CountAsync();
            if (roles <= 0)
            {
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Admin"
                });
                await roleManager.CreateAsync(new Role()
                {
                    Name = "User"
                });
                await roleManager.CreateAsync(new Role()
                {
                    Name = "Author"
                });

            }
        }
    }
}
