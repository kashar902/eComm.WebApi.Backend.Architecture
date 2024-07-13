using Core.Logics.IServices.Auth;
using ECommStore.Controller.BaseControllers;
using Infra.Profiles.Requests.Auth;
using Microsoft.AspNetCore.Mvc;
using ApiUrl = Infra.Constants.Consts;

namespace ECommStore.Controller;

[Route(ApiUrl.Route)]
public class RoleController : BaseController
{
    private readonly IRoleService _roleService;

    public RoleController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    [HttpPost(ApiUrl.Create)]
    public async Task<IActionResult> Add(CreateRoleRequest request)
        => Ok(await _roleService.Create(request));

    [HttpPut(ApiUrl.Update)]
    public async Task<IActionResult> Update(UpdateRoleRequest request)
        => Ok(await _roleService.Update(request));
}