using Data.Data;
using Data.Entities;
using Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories;

public class Repository<TEntity> : IRepository<TEntity>
    where TEntity : BaseEntity
{
    protected readonly GameStoreDbContext _ctx;

    protected Repository(GameStoreDbContext ctx)
    {
        _ctx = ctx;
    }

    public async Task AddAsync(TEntity entity)
    {
        await _ctx.Set<TEntity>().AddAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _ctx.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        _ctx.Set<TEntity>().Remove(entity);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _ctx.Set<TEntity>().Select(x => x).ToListAsync();
    }

    public async Task<ICollection<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression)
    {
        return await _ctx.Set<TEntity>().Where(expression).ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(int id)
    {
        return await _ctx.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
    }

    public void Update(TEntity entity)
    {
        _ctx.Set<TEntity>().Update(entity);
    }
}
