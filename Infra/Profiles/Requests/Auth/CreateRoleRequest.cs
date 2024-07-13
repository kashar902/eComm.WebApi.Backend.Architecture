using System.Reflection.Metadata.Ecma335;
using Infra.Entities.AuthEntities;

namespace Infra.Profiles.Requests.Auth;

public class CreateRoleRequest
{
    public required string Name { get; init; }
    public string? Description { get; init; }

    public Role ToRole()
    {
        return new Role
        {
            Name = Name,
            Description = Description,
            CreatedBy = "System",
            CreatedOn = DateTime.Now,
            IsDeleted = false,
            ModifiedBy = null,
            ModifiedOn = null
        };
    }
}

public class UpdateRoleRequest
{
    public int Id { get; init; }
    public required string Name { get; init; }
    public string? Description { get; init; }

    public Role ToRole(Role entity)
    {
        entity.Name = Name;
        entity.Description = Description;
        entity.IsDeleted = false;
        entity.ModifiedBy = "System";
        entity.ModifiedOn = DateTime.Now;

        return entity;
    }
}