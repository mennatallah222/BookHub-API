using API.Infrastructure.Infrastructures;
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
            services.AddTransient<IProductRepo, ProductRepo>();
            services.AddTransient<IOrderRepo, OrderRepo>();
            //config for generic repo
            services.AddTransient(typeof(IGenericRepo<>), typeof(GenericRepo<>));


            return services;
        }
    }
}
