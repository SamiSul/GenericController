using System.ComponentModel.DataAnnotations;

namespace GenericController;

public interface IObjectBase
{
    Guid Id { get; set; }
}