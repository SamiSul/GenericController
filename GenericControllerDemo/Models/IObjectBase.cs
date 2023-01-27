using System.ComponentModel.DataAnnotations;

namespace GenericControllerDemo.Models;

public interface IObjectBase
{
    Guid Id { get; set; }
}