using API.Service.AuthService.Implementations;
using API.Service.AuthService.Interfaces;
using API.Service.Implementations;
using API.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API.Service
{
    public static class ModularServiceDependency
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<IProductsService, ProductService>();
            services.AddTransient<IOrederService, OrderService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IApplicationUserService, ApplicationUserService>();
            services.AddTransient<IReaderService, ReaderService>();
            services.AddTransient<IFriendsService, FriendsService>();
            services.AddTransient<ICurrentUserService, CurrentUserService>();
            services.AddTransient<IFileService, FileService>();


            return services;
        }
    }
}
