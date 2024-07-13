using API.Service.Implementations;
using API.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace API.Service
{
    public static class ModularServiceDependency
    {
        public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICustomerService, CustomerService>();
            services.AddTransient<IProductsService, ProductService>();
            services.AddTransient<IOrederService, OrderService>();
            services.AddTransient<ICartService, CartService>();
            services.AddTransient<IAuthenticationService, AuthenticationService>();
            services.AddTransient<IAuthorizationService, AuthorizationService>();


            return services;
        }
    }
}
