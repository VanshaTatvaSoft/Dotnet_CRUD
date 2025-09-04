using Microsoft.Extensions.DependencyInjection;

namespace Web_Api_Core.CustomAttributes;

public class InjectableServiceAttribute: Attribute
{
    public ServiceLifetime Lifetime { get; }

    public InjectableServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}
