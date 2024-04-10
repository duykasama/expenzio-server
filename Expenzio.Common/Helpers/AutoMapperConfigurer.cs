using AutoMapper;
using Expenzio.Common.Interfaces;

namespace Expenzio.Common.Helpers;

public static class AutoMapperConfigurationHelper
{
    public static void Configure(IMapperConfigurationExpression config)
    {
        var configurers = AppDomain
            .CurrentDomain
            .GetAssemblies()
            .SelectMany(a => a.GetTypes())
            .Where(t => t.IsAssignableTo(typeof(IAutoMapperConfigurer)) && t.IsClass && !t.IsAbstract);
        foreach (var configurer in configurers)
        {
            var instance = Activator.CreateInstance(configurer) as IAutoMapperConfigurer;
            instance?.Configure(config);
        }
    }
}
