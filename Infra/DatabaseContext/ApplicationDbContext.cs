using Infra.Entities.AuthEntities;
using Microsoft.EntityFrameworkCore;

namespace Infra.DatabaseContext;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
    : DbContext(options)
{
    public DbSet<Role> Roles { get; set; }
}