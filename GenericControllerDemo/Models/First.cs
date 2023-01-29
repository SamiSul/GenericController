using GenericController;

namespace GenericControllerDemo.Models;

public class First : IObjectBase
{
    public Guid Id { get; set; }
    public List<Second>? SecondTestModels { get; set; }
    public Unrelated? Unrelated { get; set; }
    public Guid? UnrelatedId { get; set; }
}