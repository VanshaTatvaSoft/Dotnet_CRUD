using Microsoft.AspNetCore.Mvc;
using Web_Api_Service.DTO;

namespace Web_Api_Controller.Controllers;

[ApiController]
public class BaseController: ControllerBase
{
    protected IActionResult ResponseHandler<T>(ApiResponse<T> response)
    {
        return StatusCode((int)response.StatusCode, response);
    }
}
