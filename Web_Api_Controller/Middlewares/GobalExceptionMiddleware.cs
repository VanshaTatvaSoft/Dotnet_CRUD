using System.Net;
using System.Text.Json;
using Web_Api_Service.DTO;

namespace Web_Api_Controller.Middlewares;

public class GobalExceptionMiddleware(RequestDelegate next)
{
    private readonly RequestDelegate _next = next;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            LogExceptionToFile(ex, context);

            await HandleExceptionAsync(context, ex);
        }
    }

    private static void LogExceptionToFile(Exception ex, HttpContext context)
    {
        var logDir = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
        if (!Directory.Exists(logDir))
            Directory.CreateDirectory(logDir);

        String logFile = Path.Combine(logDir, $"{DateTime.UtcNow:yyyy-MM-dd}.log");

        String logText = $@"
==============================
Time: {DateTime.UtcNow:yyyy-MM-dd HH:mm:ss}
UrlPath: {context.Request.Path}
Method: {context.Request.Method}
Message: {ex.Message}
InnerException: {ex.InnerException}
StackTrace: {ex.StackTrace}
==============================";

        File.AppendAllText(logFile, logText);
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        context.Response.ContentType = "application/json";

        ApiResponse<string> response = new ApiResponse<string>(HttpStatusCode.InternalServerError, "An unexpected error occurred. Please try again later.", false);

        await context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}
