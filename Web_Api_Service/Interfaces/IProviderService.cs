using Web_Api_Service.DTO;

namespace Web_Api_Service.Interfaces;

public interface IProviderService
{
    public Task<ApiResponse<List<ProductInfoDTO>>> GetAllProductAsync(CancellationToken cancellationToken);
    public Task<ApiResponse<string>> AddProductAsync(ProductInfoDTO productInfo, CancellationToken cancellationToken);
}
