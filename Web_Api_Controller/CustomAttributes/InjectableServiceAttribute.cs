namespace Web_Api_Controller.CustomAttributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public class InjectableServiceAttribute: Attribute
{
    public ServiceLifetime Lifetime { get; }

    public InjectableServiceAttribute(ServiceLifetime lifetime = ServiceLifetime.Scoped)
    {
        Lifetime = lifetime;
    }
}
