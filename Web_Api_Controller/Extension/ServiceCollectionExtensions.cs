using System.Reflection;
using Web_Api_Repository.BaseRepository;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.CustomAttributes;

namespace Web_Api_Controller.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        Assembly serviceAssenbly = Assembly.Load("Web_Api_Service");
        foreach (Type type in serviceAssenbly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.GetCustomAttribute<InjectableServiceAttribute>() != null))
        {
            InjectableServiceAttribute attr = type.GetCustomAttribute<InjectableServiceAttribute>();
            Type iface = type.GetInterfaces().FirstOrDefault();
            if (iface != null)
            {
                services.Add(new ServiceDescriptor(iface, type, attr.Lifetime));
            }
            else
            {
                services.Add(new ServiceDescriptor(type, type, attr.Lifetime));
            }
        }
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
