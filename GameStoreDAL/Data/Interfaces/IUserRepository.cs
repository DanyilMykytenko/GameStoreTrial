using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> expression);

        Task<User> GetByIdAsync(int id);

        Task AddAsync(User entity);

        Task DeleteByIdAsync(int id);

        void Update(User entity);
    }
}
