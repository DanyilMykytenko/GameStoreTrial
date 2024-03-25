using Business.Interfaces;
using Business.Models;
using Data.Data;
using Data.Entities;
using IdentityModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Business.Services
{
    public class JWTGeneratorService : IJWTGeneratorService
    {
        private readonly IConfiguration _configuration;
        private readonly GameStoreDbContext _context;
        private readonly RoleManager<Role> _roleManager;

        public JWTGeneratorService(IConfiguration configuration, GameStoreDbContext context, RoleManager<Role> roleManager)
        {
            _configuration = configuration;
            _context = context;
            _roleManager = roleManager;
        }

        public string GenerateJWT(JWTBusinessModel model)
        {
            var secretKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));
            var credential = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256Signature);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, model.UserId),
                new Claim(JwtClaimTypes.Role, model.RoleName)
            };

            var role = _roleManager.Roles.FirstOrDefaultAsync(x => x.Name == model.RoleName).GetAwaiter().GetResult();
            var roleClaims = _roleManager.GetClaimsAsync(role).GetAwaiter().GetResult();
            var claimsValue = roleClaims.Select(x => x.Value).ToList();
            
            foreach(string claim in claimsValue) 
            {
                claims.Add(new Claim("permission", claim));
            }
            
            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credential);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
