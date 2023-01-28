using System.Reflection;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GenericController.Utils;

/// <summary>
///    Initiate the Controller generator
/// </summary>
/// <param name="assemblies">Names of assemblies to search for classes</param>
public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private Assembly _assembly { get; }

    public GenericTypeControllerFeatureProvider(Assembly assembly)
    {
        _assembly = assembly;
    }

    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        var types = _assembly.GetExportedTypes();

        var customClasses = types.Where(type =>
            type.IsAssignableTo(typeof(IObjectBase)) && type.Name != nameof(IObjectBase));

        var inheritedController = types.SingleOrDefault(type =>
            type is { IsAbstract: false } &&
            Reflection.IsSubclassOfRawGeneric(typeof(GenericControllerBase<,>), type)
        );

        if (inheritedController is null) return;

        foreach (var candidate in customClasses)
        {
            // Ignore BaseController itself
            if (candidate.FullName is not null && candidate.FullName.Contains("BaseController")) continue;

            // Generate type info for our runtime controller, assign class as T
            var propertyType = candidate.GetProperty("Id")?.PropertyType;
            if (propertyType is null) continue;

            var typeInfo = inheritedController.GetTypeInfo().MakeGenericType(candidate, propertyType)
                .GetTypeInfo();

            feature.Controllers.Add(typeInfo);
        }
    }
}