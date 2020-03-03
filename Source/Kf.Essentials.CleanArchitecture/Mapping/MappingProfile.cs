using AutoMapper;
using System;
using System.Linq;
using System.Reflection;

namespace Kf.Essentials.CleanArchitecture.Mapping
{
    public sealed class MappingProfile : Profile
    {
        public MappingProfile(params Assembly[] assemblies)
            => ApplyMappingsFromAssembly(assemblies.IfNullThen(new Assembly[] { Assembly.GetExecutingAssembly() }));        

        private void ApplyMappingsFromAssembly(Assembly[] assemblies)
        {
            var types = assemblies.SelectMany(assembly => assembly.GetExportedTypes())
                .Where(t => t.GetInterfaces().Any(i =>
                    i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IMapFrom<>)))
                .ToList();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);

                var methodInfo = type.GetMethod("Mapping")
                    ?? type.GetInterface("IMapFrom`1").GetMethod("Mapping");

                methodInfo?.Invoke(instance, new object[] { this });
            }
        }
    }
}
