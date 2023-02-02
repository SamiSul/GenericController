using System.Linq.Expressions;

namespace GenericController.Data;

public interface IRepository<TEntity, TEntityId> where TEntity : IObjectBase
{
    Task<IReadOnlyList<TEntity>> GetAsync(CancellationToken cancellationToken, string? entitiesToInclude);
    Task<TEntity?> GetAsync(TEntityId id, CancellationToken cancellationToken, string? entitiesToInclude);
    void Add(TEntity entity);
    void AddRange(IEnumerable<TEntity> entities);
    Task Remove(TEntityId entityId, CancellationToken cancellationToken);
    void Update(TEntity entity);
    Task SaveChangesAsync(CancellationToken cancellationToken);
}