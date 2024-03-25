using Business.Models;

namespace Business.Interfaces
{
    public interface IUserService
    {
        Task<UserModel> GetUserById(string id);
        Task<IEnumerable<UserModel>> GetAllUsers();
        Task CreateUser(UserModel model);
        Task UpdateUser(UserModel model);
        Task DeleteUser(string id);
    }
}
