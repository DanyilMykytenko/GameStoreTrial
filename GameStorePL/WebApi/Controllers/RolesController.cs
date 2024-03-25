using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.RequestModels;

namespace WebApi.Controllers;

/// <summary>
/// Roles API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;
    private readonly IMapper _mapper;

    public RolesController(IRoleService roleService, IMapper mapper)
    {
        _roleService = roleService;
        _mapper = mapper;
    }

    /// <summary>
    /// Get Role by id.
    /// </summary>
    /// <param name="id">RoleId.</param>
    /// <returns>Role by id.</returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var role = await _roleService.GetRoleById(id);
        return Ok(role);
    }

    /// <summary>
    /// Get all roles.
    /// </summary>
    /// <returns>List of roles.</returns>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var roles = await _roleService.GetAllRoles();
        return Ok(roles);
    }

    /// <summary>
    /// Create role.
    /// </summary>
    /// <param name="role">Role model.</param>
    /// <returns>NoContent.</returns>
    [HttpPost]
    public async Task<IActionResult> Create(CreateRoleRequest role)
    {
        await _roleService.CreateRole(_mapper.Map<CreateRoleModel>(role));
        return Ok();
    }

    /// <summary>
    /// Updates role.
    /// </summary>
    /// <param name="role">Role model.</param>
    /// <returns>NoContent.</returns>
    [HttpPut]
    public async Task<IActionResult> Update(UpdateRoleRequest role)
    {
        await _roleService.UpdateRole(_mapper.Map<UpdateRoleModel>(role));
        return Ok();
    }

    /// <summary>
    /// Deletes role.
    /// </summary>
    /// <param name="id">Role id.</param>
    /// <returns>NoContent.</returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(string id)
    {
        await _roleService.DeleteRole(id);
        return Ok();
    }

    /// <summary>
    /// Gets role from user.
    /// </summary>
    /// <param name="id">User Id.</param>
    /// <returns>Return list of users.</returns>
    [HttpGet("user/{id}")]
    public async Task<IActionResult> GetRolesFromUser(string id)
    {
        var roles = await _roleService.GetRolesFromUser(id);
        return Ok(roles);
    }

    /// <summary>
    /// Gets permission from role.
    /// </summary>
    /// <param name="id">Role id.</param>
    /// <returns>Permissions list.</returns>
    [HttpGet("permissions/{id}")]
    public async Task<IActionResult> GetPermissionsFromRole(string id)
    {
        var claimsValue = await _roleService.GetPermissionsFromRole(id);
        return Ok(claimsValue);
    }

    /// <summary>
    /// Gets all permissions.
    /// </summary>
    /// <returns>List of permissions.</returns>
    [HttpGet("permissions")]
    public async Task<IActionResult> GetAllPermissions()
    {
        var claimsValue = await _roleService.GetPermissions();
        return Ok(claimsValue);
    }

    // [HttpGet("user/{id}")]
    // public async Task<IActionResult> GetRoleByUser(string id)
    // {
    //     var user = await _userManager.FindByIdAsync(id);
    //     var userRole = await _userManager.GetRolesAsync(user);
    //     // var roles = new List<string>();
    //     // foreach (var role in userRoles)
    //     // {
    //     //     var roleName = await _roleManager.FindByNameAsync(role);
    //     //     roles.Add(roleName.Id);
    //     // }
    //     return Ok(userRole);
    // }
}
