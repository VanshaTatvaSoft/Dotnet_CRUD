using System.Net;
using Web_Api_Repository.DTO;
using Web_Api_Repository.Models;
using Web_Api_Repository.UnitOfWork;
using Web_Api_Service.DTO;
using Web_Api_Service.Interfaces;

namespace Web_Api_Service.Services;

public class CategoryService(IUnitOfWork unitOfWork): ICategoryService
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    #region GetAllCategoryAsync 
    public async Task<ApiResponse<List<CategoryInfoDTO>>> GetAllCategoryAsync(CancellationToken cancellationToken)
    {
        QueryOptions<Category, CategoryInfoDTO> options = new()
        {
            Predicate = c => !c.IsDeleted,
            OrderBy = c => c.Id,
            Selector = c => new CategoryInfoDTO
            {
                Id = c.Id,
                CategoryName = c.CategoryName,
                CategoryDescription = c.CategoryDesc
            },
            CancellationToken = cancellationToken
        };
        List<CategoryInfoDTO> categoryList = await _unitOfWork.Categories.GetAllAsync(options);

        if(categoryList == null || categoryList.Count == 0){
            return new ApiResponse<List<CategoryInfoDTO>>(HttpStatusCode.NotFound, "No Category Found", false);
        }
        return new ApiResponse<List<CategoryInfoDTO>>(HttpStatusCode.OK, "Categories retrieved successfully", true, categoryList);
    }
    #endregion

    #region AddCategoryAsync
    public async Task<ApiResponse<string>> AddCategoryAsync(CategoryInfoDTO categoryInfo, CancellationToken cancellationToken)
    {
        try
        {
            int count = await _unitOfWork.Categories
                .CountAllAsync(c => c.CategoryName.ToLower() == categoryInfo.CategoryName.ToLower() && !c.IsDeleted);

            if(count > 0) 
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Category with this name already exist", false);

            Category category = new(){
                CategoryName = categoryInfo.CategoryName,
                CategoryDesc = categoryInfo.CategoryDescription
            };
            await _unitOfWork.Categories.AddAsync(category, cancellationToken);
            await _unitOfWork.SaveAsync();
            return new ApiResponse<string>(HttpStatusCode.OK, "Category added successfully");
        }
        catch (Exception)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Error adding category.", false);
        }

    }
    #endregion

    #region EditCategoryAsync
    public async Task<ApiResponse<string>> EditCategoryAsync(CategoryInfoDTO categoryInfo, CancellationToken cancellationToken)
    {
        try
        {
            int count = await _unitOfWork.Categories
                .CountAllAsync(c => c.CategoryName.ToLower() == categoryInfo.CategoryName.ToLower() && !c.IsDeleted && c.Id != categoryInfo.Id);

            if(count > 0) 
                return new ApiResponse<string>(HttpStatusCode.BadRequest, "Category with this name already exist", false);

            Category category = await _unitOfWork.Categories.GetByIdAsync(categoryInfo.Id, cancellationToken);
            category.CategoryName = categoryInfo.CategoryName;
            category.CategoryDesc = categoryInfo.CategoryDescription;
            category.ModifiedAt = DateTime.UtcNow;
            await _unitOfWork.Categories.EditAsync(category, cancellationToken);
            return new ApiResponse<string>(HttpStatusCode.OK, "Category updated successfully");
        }
        catch (Exception)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Error editing category.", false);
        }
    }
    #endregion

    #region SoftDeleteCategoryAsync
    public async Task<ApiResponse<string>> SoftDeleteCategoryAsync(int id, CancellationToken cancellationToken)
    {
        try
        {
            Category category = await _unitOfWork.Categories.GetByIdAsync(id, cancellationToken);

            if(category == null)
            {
                return new ApiResponse<string>(HttpStatusCode.NotFound, "Category with this id does'nt exist.", false);
            }
            await _unitOfWork.Categories.SoftDeleteAsync(category, cancellationToken);
            return new ApiResponse<string>(HttpStatusCode.OK, "Category deleted successfully");
        }
        catch (Exception)
        {
            return new ApiResponse<string>(HttpStatusCode.BadRequest, "Error deleting category.", false);
        }
    }
    #endregion


}
