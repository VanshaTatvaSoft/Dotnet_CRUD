using System.ComponentModel.DataAnnotations;

namespace Web_Api_Service.DTO;

public class CategoryInfoDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter category name")]
    [StringLength(50, ErrorMessage = "Category name should not be greater the 50")]
    public string CategoryName { get; set; }
    [Required(ErrorMessage = "Enter category description")]
    [StringLength(100, ErrorMessage = "Category description should not be greater the 100")]
    public string CategoryDescription { get; set; }
}
