using AutoMapper;
using GenericController;
using GenericController.Data;
using IObjectBase = GenericController.Data.IObjectBase;

namespace GenericControllerDemo.Controllers;

public class TestController<TEntity, TEntityId> : GenericControllerBase<TEntity, TEntityId>
    where TEntity : class, IObjectBase
{
    public TestController(IRepository<TEntity, TEntityId> repository, IMapper mapper) : base(repository, mapper)
    {
    }
}