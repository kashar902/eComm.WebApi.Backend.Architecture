using Infra.DatabaseContext;
using Infra.Entities.AuthEntities;

namespace Infra.Repositories.AuthRepository;

public class RoleRepository : Repository<Role>, IRoleRepository
{
    public RoleRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}

public interface IRoleRepository : IRepository<Role>
{
}