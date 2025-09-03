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
    public async Task<IActionResult> GetAllCategories()
    {
        ApiResponse<List<CategoryInfoDTO>> response = await _categoryService.GetAllCategoryAsync();
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region AddCategory
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromBody] CategoryInfoDTO categoryInfo)
    {
        ApiResponse<string> response = await _categoryService.AddCategoryAsync(categoryInfo);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region EditCategory
    [HttpPut]
    public async Task<IActionResult> EditCategory([FromBody] CategoryInfoDTO categoryInfo)
    {
        ApiResponse<string> response = await _categoryService.EditCategoryAsync(categoryInfo);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion

    #region EditCategory
    [HttpPut("soft-delete/{id}")]
    public async Task<IActionResult> SoftDeleteCategory(int id)
    {
        ApiResponse<string> response = await _categoryService.SoftDeleteCategoryAsync(id);
        return StatusCode((int)response.StatusCode, response);
    }
    #endregion
}
