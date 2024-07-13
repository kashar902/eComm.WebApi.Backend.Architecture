using Infra.Entities.BaseEntities;

namespace Infra.Entities.AuthEntities;

public class Role : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; } 
}