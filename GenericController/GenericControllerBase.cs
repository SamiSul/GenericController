using GenericController.Data;
using Microsoft.AspNetCore.Mvc;

namespace GenericController;

[Route("api/[controller]")]
[Produces("application/json")]
public abstract class GenericControllerBase<TEntity, TEntityId> : Controller where TEntity : class, Data.IObjectBase
{
    private readonly IRepository<TEntity, TEntityId> _repository;

    protected GenericControllerBase(IRepository<TEntity, TEntityId> repository)
    {
        _repository = repository;
    }
    
    [HttpGet]
    public virtual async Task<IActionResult> Get(CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        var entities = await _repository.GetAsync(cancellationToken, entitiesToInclude);
        return Ok(entities);
    }

    [HttpGet("{id}")]
    public virtual async Task<IActionResult> GetById(TEntityId id, CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        var entity = await _repository.GetAsync(id, cancellationToken, entitiesToInclude);
        return Ok(entity);
    }


    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] TEntity record, CancellationToken cancellationToken)
    {
        _repository.Add(record);
        _repository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update([FromBody] TEntity entity,
        CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        await _repository.Update(entity, cancellationToken, entitiesToInclude);
        await _repository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TEntityId id, CancellationToken cancellationToken)
    {
        await _repository.Remove(id, cancellationToken);
        await _repository.SaveChangesAsync(cancellationToken);
        return NoContent();
    }
}