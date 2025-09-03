using Microsoft.AspNetCore.Mvc;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Controller.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1.0")]
public class CategoryController(ICategoryService categoryService): ControllerBase
{
    private readonly ICategoryService _categoryService = categoryService;

    #region GetAllCategories
    [HttpGet]
    public async Task<IActionResult> GetAllCategories(CancellationToken cancellationToken)
    {
        ApiResponse<List<CategoryInfoDTO>> response = await _categoryService.GetAllCategoryAsync(cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region AddCategory
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryInfoDTO categoryInfo, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _categoryService.AddCategoryAsync(categoryInfo, cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region EditCategory
    [HttpPut]
    public async Task<IActionResult> EditCategory([FromBody] CategoryInfoDTO categoryInfo, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _categoryService.EditCategoryAsync(categoryInfo, cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region SoftDeleteCategory
    [HttpPut("soft-delete/{id:int}")]
    public async Task<IActionResult> SoftDeleteCategory(int id, CancellationToken cancellationToken)
    {
        ApiResponse<string> response = await _categoryService.SoftDeleteCategoryAsync(id, cancellationToken);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion
}
