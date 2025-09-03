using System.Reflection;
using Web_Api_Repository.BaseRepository;
using Web_Api_Repository.UnitOfWork;

namespace Web_Api_Controller.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        Assembly serviceAssenbly = Assembly.Load("Web_Api_Service");
        foreach (Type type in serviceAssenbly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")))
        {
            Type iface = type.GetInterfaces().FirstOrDefault();
            if (iface != null)
            {
                services.AddScoped(iface, type);
            }
        }
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
