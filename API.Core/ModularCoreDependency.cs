using API.Infrastructure.Interfaces;
using API.Infrastructure.Repos;
using API.Service.Implementations;
using API.Service.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace API.Core
{
    public static class ModularCoreDependency
    {
        public static IServiceCollection AddCoreDependencies(this IServiceCollection services)
        {

            //mediatr configurations
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

            //automapper configuarations
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}
