using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        protected readonly GameStoreDbContext _ctx;

        public UserRepository(GameStoreDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(User entity)
        {
            await _ctx.Set<User>().AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _ctx.Set<User>().FirstOrDefaultAsync(x => x.Id == id.ToString());
            _ctx.Set<User>().Remove(entity);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _ctx.Set<User>().Select(x => x).ToListAsync();
        }

        public async Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>> expression)
        {
            return await _ctx.Set<User>().Where(expression).ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _ctx.Set<User>().FirstOrDefaultAsync(x => x.Id == id.ToString());
        }

        public void Update(User entity)
        {
            _ctx.Set<User>().Update(entity);
        }
    }
}
