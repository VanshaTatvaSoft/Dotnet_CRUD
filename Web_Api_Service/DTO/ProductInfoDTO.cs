using System.ComponentModel.DataAnnotations;

namespace Web_Api_Service.DTO;

public record ProductInfoDTO
(
    int Id,
    [Required(ErrorMessage = "Enter product name")]
    [StringLength(50, ErrorMessage = "Product name should not be greater the 50")]
    string ProductName,
    [Required(ErrorMessage = "Enter product description")]
    [StringLength(100, ErrorMessage = "Product description should not be greater the 100")]
    string ProductDesc,
    [Required(ErrorMessage = "Enter product price")]
    decimal ProductPrice,
    [Required(ErrorMessage = "Enter category id")]
    [Range(1, int.MaxValue, ErrorMessage = "Category Id must be greater than 0")]
    int CategoryId,
    string CategoryName
);
