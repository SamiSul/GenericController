using GenericController;

namespace GenericControllerDemo.Models;

public class Unrelated : IObjectBase
{
    public Guid Id { get; set; }
    public List<First>? Firsts { get; set; }
}