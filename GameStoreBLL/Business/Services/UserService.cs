using AutoMapper;
using Business.Interfaces;
using Business.Models;
using Business.Validation;
using Data.Data;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Business.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly GameStoreDbContext _context;
        private readonly IMapper _mapper;
        
        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, GameStoreDbContext context, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
            _mapper = mapper;
        }

        public async Task CreateUser(UserModel model)
        {
            var user = new User
            {
                Id = model.Id,
                UserName = model.UserName,
            };
            await _userManager.CreateAsync(user, model.Password);

            var roles = new List<Role>();
            foreach (var id in model.Roles)
            {
                var role = await _roleManager.FindByIdAsync(id);
                roles.Add(role);
            }

            foreach (var role in roles)
            {
                await _userManager.AddToRoleAsync(user, role.Name);
            }
        }

        public async Task DeleteUser(string id)
        {
                if (id == null)
                {
                    throw new GameStoreException();
                }

                var user = await _userManager.FindByIdAsync(id);
                var rolesForUser = await _userManager.GetRolesAsync(user);

                using var transaction = _context.Database.BeginTransaction();
                if (rolesForUser.Count > 0)
                {
                    foreach (var item in rolesForUser.ToList())
                    {
                        // item should be the name of the role
                        await _userManager.RemoveFromRoleAsync(user, item);
                    }
                }

                await _userManager.DeleteAsync(user);
                transaction.Commit();
        }

        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await _userManager.Users.ToListAsync();
            return _mapper.Map<IEnumerable<UserModel>>(users);
            //var newUsers = new List<UserModel>();
            //
            //foreach (var user in users)
            //{
            //    var newUser = new Y
            //    {
            //        Id = user.Id,
            //        Name = user.UserName,
            //    };
            //    newUsers.Add(newUser);
            //}
        }

        public async Task<UserModel> GetUserById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            var userRoles = await _userManager.GetRolesAsync(user);

            var model = _mapper.Map<UserModel>(user);
            model.Roles = userRoles;

            return model;
        }

        public async Task UpdateUser(UserModel model)
        {
            var user = _mapper.Map<User>(model);
            await _userManager.UpdateAsync(user);
        }
    }
}
