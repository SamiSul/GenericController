namespace GenericControllerDemo.Models;

public class Something : IObjectBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TheThing = "Something";
}

public class SomeOtherThing : IObjectBase
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string TheThing = "TheOtherThing";
}

public class AnotherShittyClass : IObjectBase
{
    public Guid Id { get; set; }
}