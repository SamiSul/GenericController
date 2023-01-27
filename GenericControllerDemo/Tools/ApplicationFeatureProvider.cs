using System.Reflection;
using GenericControllerDemo.Controllers;
using GenericControllerDemo.Models;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;

namespace GenericControllerDemo.Tools;

/// <summary>
///    Initiate the Controller generator
/// </summary>
/// <param name="assemblies">Names of assemblies to search for classes</param>
public class GenericTypeControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
{
    private string[] Assemblies { get; }

    public GenericTypeControllerFeatureProvider(string[] assemblies)
    {
        this.Assemblies = assemblies;
    }

    public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
    {
        foreach (var assembly in this.Assemblies)
        {
            var loadedAssembly = Assembly.Load(assembly);
            var customClasses = loadedAssembly.GetExportedTypes().Where(type =>
                type.IsAssignableTo(typeof(IObjectBase)) && type.Name != nameof(IObjectBase));


            foreach (var candidate in customClasses)
            {
                // Ignore BaseController itself
                if (candidate.FullName is not null && candidate.FullName.Contains("BaseController")) continue;
                
                // Generate type info for our runtime controller, assign class as T
                var propertyType = candidate.GetProperty("Id")?.PropertyType;
                if (propertyType is null) continue;

                var typeInfo = typeof(GenericController<,>).MakeGenericType(candidate, propertyType).GetTypeInfo();
                
                // Finally, add the new controller via FeatureProvider
                feature.Controllers.Add(typeInfo);
            }
        }
    }
}