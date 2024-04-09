using AutoMapper;
using Expenzio.Common.Helpers;
using Expenzio.Common.Interfaces;
using Expenzio.DAL.Data;

namespace Expenzio.Api.Extensions;

public static class ServiceCollectionExtensions {
    public static IServiceCollection ConfigureServices(this IServiceCollection services) {
        EnsureRequiredAssembliesLoaded();
        services.AddScoped<ExpenzioDbContext>();
        var assemblyTypes = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(t => t.GetTypes())
            .ToList();
        var autoRegisterableTypes = assemblyTypes.Where(t => t.IsInterface && typeof(IAutoRegisterable).IsAssignableFrom(t) && t != typeof(IAutoRegisterable));
        var numb = autoRegisterableTypes.Count();
        foreach (var registerableType in autoRegisterableTypes)
        {
            var implementationType = assemblyTypes.FirstOrDefault(t => t.IsClass && !t.IsAbstract && registerableType.IsAssignableFrom(t));
            if (implementationType is null) continue;
            services.AddScoped(registerableType, implementationType);
        }

        var config = new MapperConfiguration(AutoMapperConfigurer.Configure);
        var mapper = config.CreateMapper();
        services.AddSingleton(mapper);

        return services;
    }

    private static void EnsureRequiredAssembliesLoaded() {
        var assemblyNames = new[] {
            "Expenzio.DAL",
            "Expenzio.Service",
        };
        foreach (var assemblyName in assemblyNames) {
            AppDomain.CurrentDomain.Load(assemblyName);
        }
    }
}
