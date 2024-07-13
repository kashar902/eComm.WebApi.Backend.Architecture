using Core.Logics.IServices.Auth;
using Core.Logics.Services.Auth;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class Startup
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        services.AddServices();
        return services;
    }

    private static void AddServices(this IServiceCollection services)
    {
        _ = services.AddScoped<IRoleService, RoleService>();
    }
}