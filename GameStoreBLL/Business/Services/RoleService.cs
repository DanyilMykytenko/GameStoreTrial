using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Security.Claims;

namespace Business.Services
{
    public class RoleService : IRoleService
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        public RoleService(RoleManager<Role> roleManager, UserManager<User> userManager, IMapper mapper)
        {
            _roleManager = roleManager;
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task CreateRole(CreateRoleModel model)
        {
            var roleToCreate = new Role
            {
                Name = model.Name,
            };
            await _roleManager.CreateAsync(roleToCreate);
            var role = await _roleManager.FindByNameAsync(model.Name);
            foreach(var claim in model.Claims) 
            {
                var claimToAdd = new Claim("permission", claim);
                await _roleManager.AddClaimAsync(role, claimToAdd);
            }
        }

        public async Task DeleteRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            var usersInRole = await _userManager.GetUsersInRoleAsync(role.Name);

            foreach (var user in usersInRole)
            {
                await _userManager.RemoveFromRoleAsync(user, role.Name);
            }

            await _roleManager.DeleteAsync(role);
        }

        public async Task<IEnumerable<RoleModel>> GetAllRoles()
        {
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map<IEnumerable<RoleModel>>(roles);
        }

        public async Task<IEnumerable<string>> GetPermissions()
        {
            var roles = await _roleManager.Roles.ToListAsync();

            var claims = new List<Claim>();

            foreach (var role in roles)
            {
                var roleClaims = await _roleManager.GetClaimsAsync(role);
                claims.AddRange(roleClaims);
            }

            var claimsValue = claims.Select(x => x.Value).ToList();
            
            return claimsValue;
        }

        public async Task<IEnumerable<string>> GetPermissionsFromRole(string userId)
        {
            var role = await _roleManager.FindByIdAsync(userId);
            var claims = await _roleManager.GetClaimsAsync(role);

            var claimsValue = claims.Select(x => x.Value).ToList();

            return claimsValue;
        }

        public async Task<RoleModel> GetRoleById(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            return _mapper.Map<RoleModel>(role);
        }

        public async Task<IEnumerable<string>> GetRolesFromUser(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var roles = await _userManager.GetRolesAsync(user);
            return roles;
        }

        public async Task UpdateRole(UpdateRoleModel model)
        {
            var roleToUpdate = _mapper.Map<Role>(model);
            await _roleManager.UpdateAsync(roleToUpdate);
            
            var role = await _roleManager.FindByNameAsync(model.Name);

            var permissions = await GetPermissions();
            var permissionsToAdd = permissions.Where(x => model.Claims.Contains(x));
            var permissionsToRemove = permissions.Where(x => !model.Claims.Contains(x));
            
            foreach(var permission in permissionsToRemove)
            {
                var claim = new Claim("permission", permission);
                await _roleManager.RemoveClaimAsync(role, claim);
            }

            foreach(var permission in permissionsToAdd) 
            {
                var claim = new Claim("permission", permission);
                await _roleManager.AddClaimAsync(role, claim);
            }
        }
    }
}
