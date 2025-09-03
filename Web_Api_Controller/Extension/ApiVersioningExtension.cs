namespace Web_Api_Controller.Extension;

public static class ApiVersioningExtension
{
    public static IServiceCollection ApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(options => 
        {
            // Report API versions in response headers
            options.ReportApiVersions = true;

            // Default API version when client doesn’t specify
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);

            // Support for versioning via query string, URL, header, etc.
            options.ApiVersionReader = new Microsoft.AspNetCore.Mvc.Versioning.UrlSegmentApiVersionReader();
        });

        services.AddVersionedApiExplorer(options =>
        {
            options.GroupNameFormat = "'v'VVV"; // v1, v2
            options.SubstituteApiVersionInUrl = true;
        });
        
        return services;
    }
}
