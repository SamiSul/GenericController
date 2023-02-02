using GenericController.Extensions;
using GenericController.Utils;
using Microsoft.EntityFrameworkCore;

namespace GenericController.Data;

public class Repository<TEntity, TEntityId> : IRepository<TEntity, TEntityId> where TEntity : class, IObjectBase
{
    private readonly DbContext _context;

    public Repository(DbContext context)
    {
        _context  = context;
    }

    public async Task<IReadOnlyList<TEntity>> GetAsync(CancellationToken cancellationToken,
        string entitiesToInclude = null)
    {
        var queryableSet = _context.Set<TEntity>().AsQueryable();
        if (entitiesToInclude is not null)
        {
            queryableSet = EntityFrameworkCore<TEntity>.GenerateQueryWithInclude(queryableSet, entitiesToInclude);
        }

        return await queryableSet.ToListAsync(cancellationToken);
    }

    public async Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken,
        string entitiesToInclude = null)
    {
        var queryableSet = _context.Set<TEntity>().AsQueryable();
        if (entitiesToInclude is not null)
        {
            queryableSet = EntityFrameworkCore<TEntity>.GenerateQueryWithInclude(queryableSet, entitiesToInclude);
        }

        return await queryableSet.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString(), cancellationToken);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void AddRange(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public async Task Remove(TEntityId entityId, CancellationToken cancellationToken)
    {
        var entity = await GetAsync(entityId, cancellationToken);
        _context.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}