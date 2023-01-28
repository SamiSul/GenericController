namespace GenericControllerDemo.Models;

public class Some : IObjectBase
{
    public Guid Id { get; set; }
    public List<SomeOtherThing>? SomeOtherThings { get; set; }
}

public class Something : IObjectBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TheThing = "Something";
    public SomeOtherThing? SomeOtherThing { get; set; }
    public Guid? SomeOtherThingId { get; set; }
}

public class SomeOtherThing : IObjectBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TheThing = "TheOtherThing";
    public List<Something>? Somethings { get; set; }
    public Some? Some { get; set; }
    public Guid? SomeId { get; set; }
}