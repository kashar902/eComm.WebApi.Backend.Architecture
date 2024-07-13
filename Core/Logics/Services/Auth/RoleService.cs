using Core.Logics.IServices.Auth;
using Core.ServiceResponse;
using Infra.Constants;
using Infra.Profiles.Requests.Auth;
using Infra.Repositories.AuthRepository;

namespace Core.Logics.Services.Auth;

public class RoleService : IRoleService
{
    private readonly IRoleRepository _roleRepository;

    public RoleService(IRoleRepository roleRepository)
    {
        _roleRepository = roleRepository;
    }

    public async Task<Result> Create(CreateRoleRequest request)
    {
        var role = request.ToRole();
        _ = await _roleRepository.CreateAsync(role);
        return Result.Added();
    }

    public async Task<Result> Update(UpdateRoleRequest request)
    {
        var role = await _roleRepository.FindAsync(request.Id);
        if (role == null) 
            return Result.Failure(Consts.NotFound);
        
        role = request.ToRole(role);
        _ = _roleRepository.UpdateAsync(role);
        
        return Result.Updated();
    }
}