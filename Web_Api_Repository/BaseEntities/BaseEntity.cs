using System.ComponentModel.DataAnnotations;

namespace Web_Api_Repository.BaseEntities;

public abstract class BaseEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    public bool IsDeleted { get; set; } = false;

    [Required]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? ModifiedAt { get; set; }
}
