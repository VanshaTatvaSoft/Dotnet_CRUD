using Microsoft.AspNetCore.Mvc;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Controller.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class ProductController(IProviderService providerService): BaseController
{
    private readonly IProviderService _providerService = providerService;

    #region GetAllProducts
    [HttpGet]
    public async Task<IActionResult> GetAllProducts10(CancellationToken cancellationToken)
    {
        ApiResponse<List<ProductInfoDTO>> response = await _providerService.GetAllProductAsync(cancellationToken);
        return ResponseHandler(response);
    }
    #endregion

    #region AddProduct
    [HttpPost]
    public async Task<IActionResult> AddProduct([FromBody] ProductInfoDTO productInfo, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _providerService.AddProductAsync(productInfo, cancellationToken);
        return ResponseHandler(response);
    }
    #endregion

    #region EditProduct
    [HttpPut]
    public async Task<IActionResult> EditProduct([FromBody] ProductInfoDTO productInfoDTO, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _providerService.EditProductAsync(productInfoDTO, cancellationToken);
        return ResponseHandler(response);
    }
    #endregion

    #region SoftDeleteProduct
    [HttpPut("soft-delete/{id:int:min(1)}")]
    public async Task<IActionResult> SoftDeleteProduct(int id, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _providerService.SoftDeleteProductAsync(id, cancellationToken);
        return ResponseHandler(response);
    }
    #endregion
}
