using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Infrastructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<User> userManager)
        {
            var users = await userManager.Users.CountAsync();
            if (users <= 0)
            {
                var defaultUser = new User()
                {
                    UserName = "admin",
                    Email = "admin@onlineshopping.com",
                    FullName = "onlineshopping",
                    Country = "Egypt",
                    PhoneNumber = "123456789",
                    Address = "Egypt",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                };
            }
        }
    }
}
