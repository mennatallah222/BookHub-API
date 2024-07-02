using API.Core.Behaviors;
using FluentValidation;
using MediatR;
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

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidatorBehavior<,>));

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
