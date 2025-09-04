using Web_Api_Service.DTO;

namespace Web_Api_Service.Interfaces;

public interface ICategoryService
{
    public Task<ApiResponse<List<CategoryInfoDTO>>> GetAllCategoryAsync(CancellationToken cancellationToken);
    public Task<ApiResponse<string>> AddCategoryAsync(CategoryInfoDTO categoryInfo, CancellationToken cancellationToken);
    public Task<ApiResponse<string>> EditCategoryAsync(CategoryInfoDTO categoryInfo, CancellationToken cancellationToken);
    public Task<ApiResponse<string>> SoftDeleteCategoryAsync(int id, CancellationToken cancellationToken);
}
