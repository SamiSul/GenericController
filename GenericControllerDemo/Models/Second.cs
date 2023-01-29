using GenericController;

namespace GenericControllerDemo.Models;

public class Second : IObjectBase
{
    public Guid Id { get; set; }
    public List<Third>? ThirdTestModels { get; set; }
    public First? FirstTestModel { get; set; }
    public Guid? FirstTestModelId { get; set; }
}