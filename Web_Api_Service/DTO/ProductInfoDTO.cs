using System.ComponentModel.DataAnnotations;

namespace Web_Api_Service.DTO;

public class ProductInfoDTO
{
    public int Id { get; set; }
    [Required(ErrorMessage = "Enter product name")]
    [StringLength(50, ErrorMessage = "Product name should not be greater the 50")]
    public string ProductName { get; set; }
    [Required(ErrorMessage = "Enter product description")]
    [StringLength(100, ErrorMessage = "Product description should not be greater the 100")]
    public string ProductDesc { get; set; }
    [Required(ErrorMessage = "Enter product price")]
    public decimal ProductPrice { get; set; }
    [Required(ErrorMessage = "Enter category id")]
    public int CategoryId { get; set; }
    public string CategoryName { get; set; }
}
