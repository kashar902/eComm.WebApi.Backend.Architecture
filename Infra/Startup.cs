using Infra.Repositories;
using Infra.Repositories.AuthRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Infra;

public static class Startup
{
    public static void AddInfra(this IServiceCollection services)
    {
        services.AddServices();
    }

    private static void AddServices(this IServiceCollection services)
    {
        _ = services.AddSingleton(typeof(IRepository<>), typeof(Repository<>));
        
        _ = services.AddScoped<IRoleRepository, RoleRepository>();
    }
}