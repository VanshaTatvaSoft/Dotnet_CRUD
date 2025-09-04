using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Api_Repository.BaseEntities;

namespace Web_Api_Repository.Models;

[Table("Products")]
public partial class Product: BaseEntity
{
    [Required]
    [StringLength(100)]
    public string ProductName { get; set; }

    [Required]
    [StringLength(200)]
    public string ProductDesc { get; set; }

    [Required]
    public decimal ProductPrice { get; set; }

    [Required]
    public int CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
}
