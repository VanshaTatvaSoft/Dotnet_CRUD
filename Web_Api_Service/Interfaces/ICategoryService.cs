using Web_Api_Repository.Models;
using Web_Api_Service.DTO;

namespace Web_Api_Service.Interfaces;

public interface ICategoryService
{
    public Task<ApiResponse<List<CategoryInfoDTO>>> GetAllCategoryAsync();
    public Task<ApiResponse<string>> AddCategoryAsync(CategoryInfoDTO categoryInfo);
    public Task<ApiResponse<string>> EditCategoryAsync(CategoryInfoDTO categoryInfo);
    public Task<ApiResponse<string>> SoftDeleteCategoryAsync(int id);
}
