using API.Infrastructure.Data;
using ClassLibrary1.Data_ClassLibrary1.Core.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddServiceRegistration(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>(opt =>
            {
                opt.SignIn.RequireConfirmedEmail = true;
                opt.User.RequireUniqueEmail = true;
                opt.Password.RequiredLength = 8;
                opt.SignIn.RequireConfirmedEmail = false;///////////for now

                opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                opt.Lockout.MaxFailedAccessAttempts = 3;
                opt.Lockout.AllowedForNewUsers = true;

                opt.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
            })
            .AddEntityFrameworkStores<ApplicationDBContext>()
            .AddDefaultTokenProviders();

            // Other service registrations
            return services;
        }
    }
}
