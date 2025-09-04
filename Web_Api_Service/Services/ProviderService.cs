using System.Net;
using Microsoft.Extensions.DependencyInjection;
using Web_Api_Repository.DTO;
using Web_Api_Repository.Models;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.CustomAttributes;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Service.Services;

[InjectableService(ServiceLifetime.Scoped)]
public class ProviderService(IUnitOfWork unitOfWork) : IProviderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region GetAllProductAsync
    public async Task<ApiResponse<List<ProductInfoDTO>>> GetAllProductAsync(CancellationToken cancellationToken)
    {
        QueryOptions<Product, ProductInfoDTO> options = new()
        {
            Predicate = p => !p.IsDeleted,
            Includes = { p => p.Category },
            OrderBy = p => p.Id,
            Selector = p => new ProductInfoDTO
            (
                p.Id,
                p.ProductName,
                p.ProductDesc,
                p.ProductPrice,
                p.Category.Id,
                p.Category.CategoryName
            ),
            CancellationToken = cancellationToken
        };

        List<ProductInfoDTO> productList = await _unitOfWork.Products.GetAllAsync(options);

        if (productList == null || productList.Count == 0)
        {
            return new ApiResponse<List<ProductInfoDTO>>(HttpStatusCode.NotFound, "No Products Found", false);
        }

        return new ApiResponse<List<ProductInfoDTO>>(HttpStatusCode.OK, "Products retrieved successfully", true, productList);
    }
    #endregion

    #region AddProductAsync
    public async Task<ApiResponse<string>> AddProductAsync(ProductInfoDTO productInfo, CancellationToken cancellationToken)
    {
        int count = await _unitOfWork.Products.CountAllAsync
            (
                p => p.ProductName.ToLower() == productInfo.ProductName.ToLower() && !p.IsDeleted,
                cancellationToken
            );
        if (count > 0)
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Product with this name already exist", false);

        count = await _unitOfWork.Categories.CountAllAsync(c => c.Id == productInfo.CategoryId && !c.IsDeleted);
        if(count == 0)
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Category with this id does'nt exist.", false);

        Product product = new()
        {
            ProductName = productInfo.ProductName,
            ProductDesc = productInfo.ProductDesc,
            ProductPrice = productInfo.ProductPrice,
            CategoryId = productInfo.CategoryId
        };
        await _unitOfWork.Products.AddAsync(product, cancellationToken);

        return new ApiResponse<string>(HttpStatusCode.OK, "Product added successfuly", false);
    }
    #endregion

    #region EditProductAsync
    public async Task<ApiResponse<string>> EditProductAsync(ProductInfoDTO productInfoDTO, CancellationToken cancellationToken)
    {
        int count = await _unitOfWork.Products.CountAllAsync
            (p => p.ProductName.ToLower() == productInfoDTO.ProductName.ToLower() && !p.IsDeleted && p.Id != productInfoDTO.Id, cancellationToken);

        if(count > 0) return new ApiResponse<string>(HttpStatusCode.BadRequest, "Product with this name already exist.", false);

        Product product = await _unitOfWork.Products.GetByIdAsync(productInfoDTO.Id, cancellationToken);
        product.ProductName = productInfoDTO.ProductName;
        product.ProductDesc = productInfoDTO.ProductDesc;
        product.ProductPrice = productInfoDTO.ProductPrice;
        product.CategoryId = productInfoDTO.CategoryId;
        product.ModifiedAt = DateTime.UtcNow;

        return new ApiResponse<string>(HttpStatusCode.OK, "Product updated successfully.");
    }
    #endregion

    #region SoftDeleteProductAsync
    public async Task<ApiResponse<string>> SoftDeleteProductAsync(int id, CancellationToken cancellationToken)
    {
        Product product = await _unitOfWork.Products.GetByIdAsync(id);
        if(product == null)
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Product with this id does'nt exist.", false);

        await _unitOfWork.Products.SoftDeleteAsync(product, cancellationToken);
        return new ApiResponse<string>(HttpStatusCode.OK, "Product deleted successfully.");
    }
    #endregion

}
