using GenericController.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GenericController;

[Route("api/[controller]")]
[Produces("application/json")]
public abstract class GenericControllerBase<T, TEntityId> : Controller where T : class, IObjectBase
{
    private readonly DbContext _db;

    protected GenericControllerBase(DbContext context)
    {
        _db = context;
    }

    [HttpGet]
    public virtual async Task<IEnumerable<T>> Get(CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        var queryableSet = _db.Set<T>().AsQueryable();
        if (entitiesToInclude is not null)
        {
            queryableSet = EntityFrameworkCore<T>.GenerateQueryWithInclude(queryableSet, entitiesToInclude);
        }

        return await queryableSet.ToListAsync(cancellationToken);
    }

    [HttpGet("{id}")]
    public virtual async Task<T?> GetById(TEntityId id, CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        var queryableSet = _db.Set<T>().AsQueryable();
        if (entitiesToInclude is not null)
        {
            queryableSet = EntityFrameworkCore<T>.GenerateQueryWithInclude(queryableSet, entitiesToInclude);
        }

        return await queryableSet.FirstOrDefaultAsync(e => e.Id.ToString() == id.ToString(), cancellationToken);
    }


    [HttpPost]
    public virtual async Task<IActionResult> Create([FromBody] T record, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            _db.Add(record);
            await _db.SaveChangesAsync(cancellationToken);

            return NoContent();
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    public virtual async Task<IActionResult> Update(TEntityId id, [FromBody] T record,
        CancellationToken cancellationToken,
        [FromQuery] string entitiesToInclude = null)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            var entityToUpdate = await GetById(id, cancellationToken, entitiesToInclude);
            _db.Set<T>().Update(entityToUpdate);

            await _db.SaveChangesAsync(cancellationToken);
            return Ok(record);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public virtual async Task<IActionResult> Delete(TEntityId id, CancellationToken cancellationToken)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            var record = GetById(id, cancellationToken);
            _db.Remove(record);
            await _db.SaveChangesAsync(cancellationToken);
            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}