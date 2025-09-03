using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Web_Api_Repository.BaseEntities;

namespace Web_Api_Repository.Models;

[Table("Categories")]
public partial class Category: BaseEntity
{
    [Required]
    [StringLength(100)]
    public string CategoryName { get; set; }

    [Required]
    [StringLength(200)]
    public string CategoryDesc { get; set; }

    public virtual ICollection<Product> Products { get; set; } = [];
}
