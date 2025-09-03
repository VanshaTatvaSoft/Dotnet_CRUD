using System.Net;
using Web_Api_Repository.DTO;
using Web_Api_Repository.Models;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Service.Services;

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
            {
                Id = p.Id,
                ProductName = p.ProductName,
                ProductDesc = p.ProductDesc,
                ProductPrice = p.ProductPrice,
                CategoryId = p.Category.Id,
                CategoryName = p.Category.CategoryName
            },
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

}
