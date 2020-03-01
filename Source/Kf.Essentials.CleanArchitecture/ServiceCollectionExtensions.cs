using AutoMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Kf.Essentials.CleanArchitecture.Mapping
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAndConfigureMediatR(
            this IServiceCollection serviceCollection
        )
            => serviceCollection
                .AddAndConfigureMediatR(Assembly.GetExecutingAssembly());

        public static IServiceCollection AddAndConfigureMediatR(
            this IServiceCollection serviceCollection,
            params Assembly[] assemblies
        )
        {
            serviceCollection
                .AddMediatR(assemblies);

            serviceCollection
                .Scan(scan => scan.FromAssemblies(assemblies)                                    
                .AddClasses(classes => classes.AssignableTo(typeof(IPipelineBehavior<,>)))
                .AsImplementedInterfaces());

            return serviceCollection;
        }
            

        public static IServiceCollection AddAndConfigureAutomapper(
            this IServiceCollection serviceCollection
        )
            => serviceCollection
                .AddAndConfigureAutomapper(Assembly.GetExecutingAssembly());

        public static IServiceCollection AddAndConfigureAutomapper(
            this IServiceCollection serviceCollection,
            params Assembly[] assemblies
        )
            => serviceCollection
                .AddAutoMapper(assemblies);
    }
}
