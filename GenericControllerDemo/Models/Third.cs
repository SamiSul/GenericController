using GenericController;

namespace GenericControllerDemo.Models;

public class Third : IObjectBase
{
    public Guid Id { get; set; }
    public Second? SecondTestModel { get; set; }
    public Guid? SecondTestModelId { get; set; }
}