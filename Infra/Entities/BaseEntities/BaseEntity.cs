using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Infra.Entities.BaseEntities;

public class BaseEntity : IBaseEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string CreatedBy { get; set; } = string.Empty;
    public string? ModifiedBy { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? ModifiedOn { get; set; }

    [Required][DefaultValue(false)]
    public bool IsDeleted { get; set; } 
}

public interface IBaseEntity
{
    int Id { get; set; }
    string CreatedBy { get; set; }
    string? ModifiedBy { get; set; }
    DateTime CreatedOn { get; set; }
    DateTime? ModifiedOn { get; set; }
    bool IsDeleted { get; set; }
}