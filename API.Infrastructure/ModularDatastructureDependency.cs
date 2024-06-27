using API.Infrastructure.Interfaces;
using API.Infrastructure.Repos;
using Microsoft.Extensions.DependencyInjection;

namespace API.Infrastructure
{
    public static class ModularDatastructureDependency
    {
        public static IServiceCollection AddInfrastructureDependencies(this IServiceCollection services)
        {
            services.AddTransient<ICustomer, CustomerRepo>();
            return services;
        }
    }
}
