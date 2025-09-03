using Web_Api_Controller.Middlewares;

namespace Web_Api_Controller.Extension;

public static class ExceptionMiddlewareExtension
{
    public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GobalExceptionMiddleware>();
    }
}
