using GenericControllerDemo.Data;
using GenericControllerDemo.Models;
using Microsoft.AspNetCore.Mvc;

namespace GenericControllerDemo.Controllers;

[Route("api/[controller]")]
[Produces("application/json")]
public class TestController<T, TEntityId> : GenericControllerBase<T, TEntityId> where T : class,
    IObjectBase
{
    private readonly GenericDbContext db;

    public TestController(GenericDbContext context) : base(context)
    {
        db = context;
    }

    [HttpGet("asa")]
    public IActionResult dostuff()
    {
        return Ok("");
    }
}