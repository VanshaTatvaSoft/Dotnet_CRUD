using System.ComponentModel.DataAnnotations;

namespace Web_Api_Service.DTO;

public record CategoryInfoDTO
(
    int Id,
    [Required(ErrorMessage = "Enter category name")]
    [StringLength(50, ErrorMessage = "Category name should not be greater the 50")]
    string CategoryName,
    [Required(ErrorMessage = "Enter category description")]
    [StringLength(100, ErrorMessage = "Category description should not be greater the 100")]
    string CategoryDescription
);
