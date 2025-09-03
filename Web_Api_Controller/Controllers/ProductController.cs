using Microsoft.AspNetCore.Mvc;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Controller.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductController(IProviderService providerService): ControllerBase
{
    private readonly IProviderService _providerService = providerService;

    #region GetAllProducts
    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        ApiResponse<List<ProductInfoDTO>> response = await _providerService.GetAllProductAsync();
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region AddProduct
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductInfoDTO productInfo)
    {
        ApiResponse<string> response = await _providerService.AddProductAsync(productInfo);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion
}
