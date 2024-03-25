using System.Security.Claims;
using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Data;
using Data.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Controllers;

/// <summary>
/// Basket API.
/// </summary>
[ApiController]
[Route("[controller]")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<User> _signInManager;
    private readonly UserManager<User> _userManager;
    private readonly GameStoreDbContext _context;
    private readonly IJWTGeneratorService _tokenGenerator;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public AuthController(SignInManager<User> signInManager, UserManager<User> userManager, GameStoreDbContext context, IJWTGeneratorService tokenGenerator, IMapper mapper, ICurrentUserService currentUserService)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _context = context;
        _tokenGenerator = tokenGenerator;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(AuthModel authDTO)
    {
        if (!ModelState.IsValid)
        {
            throw new GameStoreException(); // implement login exception
        }

        var user = await _userManager.FindByNameAsync(authDTO.Model.Login);
        if (user == null)
        {
            ModelState.AddModelError(string.Empty, "User not found");
            throw new GameStoreException(); // implement login exception
        }

        var userRole = await _context.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
        if (userRole == null)
        {
            ModelState.AddModelError(string.Empty, "RoleRef not found");
            throw new GameStoreException(); // implement login exception
        }

        var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
        if (role == null)
        {
            ModelState.AddModelError(string.Empty, "Role not found");
            throw new GameStoreException(); // implement login exception
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, authDTO.Model.Password, false);
        if (result.Succeeded)
        {
            await _signInManager.SignInWithClaimsAsync(user, false, new List<Claim> { new Claim(JwtClaimTypes.Role, role.Name) });
            var tokenModel = new JWTModel
            {
                UserId = user.Id,
                RoleName = role.Name,
            };
            var token = _tokenGenerator.GenerateJWT(_mapper.Map<JWTBusinessModel>(tokenModel));

            return Ok(new { Token = "Bearer " + token });
        }

        ModelState.AddModelError(string.Empty, "Login error");
        throw new GameStoreException(); // implement login exception
    }

    [HttpPost("check")]
    public IActionResult CheckAccess()
    {
        return Ok(true);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDTO registerDTO)
    {
        if (!ModelState.IsValid)
        {
            throw new GameStoreException(); // implement register exception
        }

        var user = new User
        {
            FirstName = registerDTO.FirstName,
            LastName = registerDTO.LastName,
            UserName = registerDTO.FirstName + registerDTO.LastName,
            Email = registerDTO.Email,
        };

        var result = await _userManager.CreateAsync(user, registerDTO.Password);
        result = await _userManager.AddToRoleAsync(user, "User");
        if (result.Succeeded)
        {
            var userRole = await _context.UserRoles.FirstOrDefaultAsync(u => u.UserId == user.Id);
            if (userRole == null)
            {
                ModelState.AddModelError(string.Empty, "RoleRef not found");
                throw new GameStoreException(); // implement register exception
            }

            var role = await _context.Roles.FirstOrDefaultAsync(r => r.Id == userRole.RoleId);
            if (role == null)
            {
                ModelState.AddModelError(string.Empty, "Role not found");
                throw new GameStoreException(); // implement register exception
            }

            await _signInManager.SignInWithClaimsAsync(user, false, new List<Claim> { new Claim(JwtClaimTypes.Role, role.Name) });

            var tokenModel = new JWTModel
            {
                UserId = _currentUserService.UserId,
                RoleName = role.Name,
            };
            var token = _tokenGenerator.GenerateJWT(_mapper.Map<JWTBusinessModel>(tokenModel));
            return Ok(new { Token = "Bearer " + token });
        }

        ModelState.AddModelError(string.Empty, "Error occurred");
        throw new GameStoreException(); // implement register exception
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await _signInManager.SignOutAsync();
        return Ok();
    }
}
