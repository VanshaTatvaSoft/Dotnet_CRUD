using System.Net;
using Web_Api_Repository.Models;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Service.Services;

public class CategoryService(IUnitOfWork unitOfWork): ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region GetAllCategoryAsync 
    public async Task<ApiResponse<List<CategoryInfoDTO>>> GetAllCategoryAsync()
    {
        List<Category> categories = await _unitOfWork.Categories.GetAllAsync(
            c => !c.IsDeleted,
            null,
            q => q.OrderBy(c => c.Id)
        );
        if(categories == null || categories.Count == 0){
            return ApiResponse<List<CategoryInfoDTO>>.FailResponse("No Category Found", HttpStatusCode.NotFound);
        }  
        List<CategoryInfoDTO> categoryList = categories.Select(c => new CategoryInfoDTO{
            Id = c.Id,
            CategoryName = c.CategoryName,
            CategoryDescription = c.CategoryDesc
        }).ToList();
        return ApiResponse<List<CategoryInfoDTO>>.SuccessResponse(categoryList, HttpStatusCode.OK, "Categories retrieved successfully");
    }
    #endregion

    #region AddCategoryAsync
    public async Task<ApiResponse<string>> AddCategoryAsync(CategoryInfoDTO categoryInfo)
    {
        try
        {
            List<Category> categories = await _unitOfWork.Categories
                .GetAllAsync(c => c.CategoryName.ToLower() == categoryInfo.CategoryName.ToLower() && !c.IsDeleted);

            if(categories.Count > 0) return ApiResponse<string>.FailResponse("Category with this name already exist", HttpStatusCode.BadRequest);

            Category category = new(){
                CategoryName = categoryInfo.CategoryName,
                CategoryDesc = categoryInfo.CategoryDescription
            };
            await _unitOfWork.Categories.AddAsync(category);
            await _unitOfWork.SaveAsync();
            return ApiResponse<string>.SuccessResponse("", HttpStatusCode.OK, "Category added successfully");
        }
        catch (Exception)
        {
            return ApiResponse<string>.FailResponse("Error adding category.", HttpStatusCode.BadRequest);
        }

    }
    #endregion

    #region EditCategoryAsync
    public async Task<ApiResponse<string>> EditCategoryAsync(CategoryInfoDTO categoryInfo)
    {
        try
        {
            List<Category> categories = await _unitOfWork.Categories
                .GetAllAsync(c => c.CategoryName.ToLower() == categoryInfo.CategoryName.ToLower() && !c.IsDeleted && c.Id != categoryInfo.Id);

            if(categories.Count > 0) 
                return ApiResponse<string>.FailResponse("Category with this name already exist", HttpStatusCode.BadRequest);

            Category category = await _unitOfWork.Categories.GetByIdAsync(categoryInfo.Id);
            category.CategoryName = categoryInfo.CategoryName;
            category.CategoryDesc = categoryInfo.CategoryDescription;
            category.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.Categories.EditAsync(category);
            return ApiResponse<string>.SuccessResponse("", HttpStatusCode.OK, "Category updated successfully");
        }
        catch (Exception)
        {
            return ApiResponse<string>.FailResponse("Error editing category.", HttpStatusCode.BadRequest);
        }
    }
    #endregion

    #region SoftDeleteCategoryAsync
    public async Task<ApiResponse<string>> SoftDeleteCategoryAsync(int id)
    {
        try
        {
            Category category = await _unitOfWork.Categories.GetByIdAsync(id);
            if(category == null)
            {
                return ApiResponse<string>.FailResponse("Category with this id does'nt exist.", HttpStatusCode.NotFound);
            }
            await _unitOfWork.Categories.SoftDeleteAsync(category);
            return ApiResponse<string>.SuccessResponse("", HttpStatusCode.OK, "Category deleted successfully");
        }
        catch (Exception)
        {
            return ApiResponse<string>.FailResponse("Error deleting category.", HttpStatusCode.BadRequest);
        }
    }
    #endregion
}
