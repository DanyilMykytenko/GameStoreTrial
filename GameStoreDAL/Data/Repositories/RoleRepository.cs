using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        protected readonly GameStoreDbContext _ctx;

        public RoleRepository(GameStoreDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task AddAsync(Role entity)
        {
            await _ctx.Set<Role>().AddAsync(entity);
        }

        public async Task DeleteByIdAsync(int id)
        {
            var entity = await _ctx.Set<Role>().FirstOrDefaultAsync(x => x.Id == id.ToString());
            _ctx.Set<Role>().Remove(entity);
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _ctx.Set<Role>().Select(x => x).ToListAsync();
        }

        public async Task<ICollection<Role>> GetAllAsync(Expression<Func<Role, bool>> expression)
        {
            return await _ctx.Set<Role>().Where(expression).ToListAsync();
        }

        public async Task<Role> GetByIdAsync(int id)
        {
            return await _ctx.Set<Role>().FirstOrDefaultAsync(x => x.Id == id.ToString());
        }

        public void Update(Role entity)
        {
            _ctx.Set<Role>().Update(entity);
        }
    }
}
