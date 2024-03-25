using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Microsoft.AspNetCore.Mvc;
using WebApi.Attributes;
using WebApi.RequestModels;

namespace WebApi.Controllers;

/// <summary>
/// Users API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly IMapper _mapper;

    public UsersController(IUserService userService, IMapper mapper)
    {
        _userService = userService;
        _mapper = mapper;
    }

    /// <summary>
    /// Gets user by id.
    /// </summary>
    /// <param name="id">UserId.</param>
    /// <returns>Returns user by id.</returns>
    [HttpGet("{id}")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(string id)
    {
        var user = await _userService.GetUserById(id);
        return Ok(_mapper.Map<GetUserRequest>(user));
    }

    /// <summary>
    /// Gets all users.
    /// </summary>
    /// <returns>Returns list of users.</returns>
    [HttpGet]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _userService.GetAllUsers();
        return Ok(_mapper.Map<IEnumerable<GetUserRequest>>(users));
    }

    /// <summary>
    /// Creates user.
    /// </summary>
    /// <returns>NoContent.</returns>
    [HttpPost]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Create(CreateUserRequest model)
    {
        await _userService.CreateUser(_mapper.Map<UserModel>(model));
        return Ok();
    }

    /// <summary>
    /// Updates user.
    /// </summary>
    /// <returns>NoContent.</returns>
    [HttpPut]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update(UpdateUserRequest model)
    {
        await _userService.UpdateUser(_mapper.Map<UserModel>(model));
        return Ok();
    }

    /// <summary>
    /// Deletes user.
    /// </summary>
    /// <returns>NoContent.</returns>
    [HttpDelete("{id}")]
    [Logging]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(string id)
    {
        if (ModelState.IsValid)
        {
            await _userService.DeleteUser(id);
        }

        return Ok();
    }
}
