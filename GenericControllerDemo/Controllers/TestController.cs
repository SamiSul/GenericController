using GenericController;
using GenericControllerDemo.Data;

namespace GenericControllerDemo.Controllers;

public class TestController<T, TEntityId> : GenericControllerBase<T, TEntityId> where T : class,
    IObjectBase
{
    private readonly GenericDbContext db;

    public TestController(GenericDbContext context) : base(context)
    {
        db = context;
    }
}