using Data.Entities;
using System.Linq.Expressions;

namespace Data.Interfaces
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllAsync();
        Task<ICollection<Role>> GetAllAsync(Expression<Func<Role, bool>> expression);

        Task<Role> GetByIdAsync(int id);

        Task AddAsync(Role entity);

        Task DeleteByIdAsync(int id);

        void Update(Role entity);
    }
}
