using Core.ServiceResponse;
using Infra.Profiles.Requests.Auth;

namespace Core.Logics.IServices.Auth;

public interface IRoleService
{
    Task<Result> Create(CreateRoleRequest request);
    Task<Result> Update(UpdateRoleRequest request);
}