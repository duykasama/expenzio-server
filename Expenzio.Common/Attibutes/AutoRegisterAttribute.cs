using Microsoft.Extensions.DependencyInjection;

namespace Expenzio.Common.Attributes;

public class AutoRegisterAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public AutoRegisterAttribute(ServiceLifetime lifetime)
    {
        Lifetime = lifetime;
    }
}
