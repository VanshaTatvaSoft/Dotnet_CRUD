using System.Net;
using Microsoft.AspNetCore.Mvc;
using Web_Api_Service.DTO;

namespace Web_Api_Controller.Extension;

public static class ModelStateValidatorExtension
{
    public static IServiceCollection ModelStateValidator(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(options => {
            options.InvalidModelStateResponseFactory = context => 
            {
                List<string> errors = context.ModelState.Values.SelectMany(e => e.Errors).Select(e => e.ErrorMessage).ToList();
                ApiResponse<string> errorResponse = new(HttpStatusCode.BadRequest, string.Join(", ", errors), false);
                return new BadRequestObjectResult(errorResponse);
            };
        });

        return services;
    }
}
