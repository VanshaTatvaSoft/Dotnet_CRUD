using System.Net;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Repository.BaseRepository;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.DTO;

namespace Web_Api_Controller.Extension;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection RegisterDependencies(this IServiceCollection services)
    {
        var serviceAssenbly = Assembly.Load("Web_Api_Service");
        foreach (var type in serviceAssenbly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.Name.EndsWith("Service")))
        {
            var iface = type.GetInterfaces().FirstOrDefault();
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
