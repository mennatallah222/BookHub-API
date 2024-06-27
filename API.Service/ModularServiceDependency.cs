using API.Infrastructure.Interfaces;
using API.Infrastructure.Repos;
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
            return services;
        }
    }
}
