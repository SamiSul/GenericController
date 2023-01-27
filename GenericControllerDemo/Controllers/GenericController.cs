using GenericControllerDemo.Data;
using GenericControllerDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericControllerDemo.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class GenericController<T, TEntityId> : Controller
    where T : class,
    IObjectBase
{
    private readonly GenericDbContext db;

    public GenericController(GenericDbContext context)
    {
        db = context;
    }

    [HttpGet]
    public IQueryable<T> Get()
    {
        return db.Set<T>();
    }

    [HttpGet("{id}")]
    public T GetById(TEntityId id)
    {
        return Get().SingleOrDefault(e => e.Id.ToString() == id.ToString());
    }


    [HttpPost]
    public async Task<IActionResult> Create([FromBody] T record)
    {
        try
        {
            // Check if payload is valid
            if (!ModelState.IsValid) return BadRequest();

            // Create the new entry
            db.Add(record);
            await db.SaveChangesAsync();

            // respond with the newly created record
            return CreatedAtAction("GetById", new { id = record.Id }, record);
        }
        catch (Exception ex)
        {
            return BadRequest(ex);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(TEntityId id, [FromBody] T record)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();

            db.Set<T>().Attach(record);
            await db.SaveChangesAsync();

            return Ok(record);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(TEntityId id)
    {
        try
        {
            if (!ModelState.IsValid) return BadRequest();
            
            var record = GetById(id);
            db.Remove(record);
            await db.SaveChangesAsync();
            return Ok("true!");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}