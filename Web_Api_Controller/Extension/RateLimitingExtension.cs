using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Web_Api_Controller.Extension;

public static class RateLimitingExtension
{
    public static IServiceCollection RateLimiting(this IServiceCollection services, IConfiguration configuration)
    {
        var permitLimit = configuration.GetValue<int>("RateLimiting:PermitLimit");
        var windowSeconds = configuration.GetValue<int>("RateLimiting:WindowSeconds");
        var queueLimit = configuration.GetValue<int>("RateLimiting:QueueLimit");

        services.AddRateLimiter(options => 
        {
            options.AddFixedWindowLimiter("fixed", opt =>
            {
                opt.PermitLimit = permitLimit;
                opt.Window = TimeSpan.FromSeconds(windowSeconds);
                opt.QueueLimit = queueLimit;
                opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });

            options.OnRejected = async (context, token) =>
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                await context.HttpContext.Response.WriteAsync("Too many requests. Please try again later.");
            };
        });

        return services;
    }
}
