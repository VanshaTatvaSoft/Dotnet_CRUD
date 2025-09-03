using System.Net;
using Microsoft.EntityFrameworkCore;
using Web_Api_Repository.Models;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Service.Services;

public class ProviderService(IUnitOfWork unitOfWork): IProviderService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region GetAllProductAsync
    public async Task<ApiResponse<List<ProductInfoDTO>>> GetAllProductAsync()
    {
        List<Product> products = await _unitOfWork.Products.GetAllAsync(
            p => !p.IsDeleted,
            q => q.Include(p => p.Category),
            q => q.OrderBy(p => p.Id)
        );
        if(products == null || products.Count == 0){
            return ApiResponse<List<ProductInfoDTO>>.FailResponse("No Products Found", HttpStatusCode.NotFound);
        }  
        List<ProductInfoDTO> productList = products.Select(p => new ProductInfoDTO{
            Id = p.Id,
            ProductName = p.ProductName,
            ProductDesc = p.ProductDesc,
            ProductPrice = p.ProductPrice,
            CategoryId = p.Category.Id,
            CategoryName = p.Category.CategoryName
        }).ToList();
        return ApiResponse<List<ProductInfoDTO>>.SuccessResponse(productList, HttpStatusCode.OK, "Products retrieved successfully");
    }
    #endregion

    #region AddProductAsync
    public async Task<ApiResponse<string>> AddProductAsync(ProductInfoDTO productInfo)
    {
        try
        {
            List<Product> products = await _unitOfWork.Products
                .GetAllAsync(p => p.ProductName.ToLower() == productInfo.ProductName.ToLower() && !p.IsDeleted);
            if(products.Count > 0) return ApiResponse<string>.FailResponse("Product with this name already exist", HttpStatusCode.BadRequest);

            Product product = new(){
                ProductName = productInfo.ProductName,
                ProductDesc = productInfo.ProductDesc,
                ProductPrice = productInfo.ProductPrice,
                CategoryId = productInfo.CategoryId
            };
            await _unitOfWork.Products.AddAsync(product);

            return ApiResponse<string>.SuccessResponse("Product added successfuly", HttpStatusCode.OK);
        }
        catch (Exception)
        {
           return ApiResponse<string>.FailResponse("Error adding product", HttpStatusCode.BadRequest);
        }
    }
    #endregion
}
