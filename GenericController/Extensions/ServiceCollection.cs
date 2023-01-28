using System.Reflection;
using GenericController.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace GenericController.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddGenericController(this IServiceCollection serviceCollection)
    {
        var mvcBuilder =
            serviceCollection.AddMvc(options => options.Conventions.Add(new GenericControllerRouteConvention()));

        mvcBuilder.ConfigureApplicationPartManager(applicationPartManager =>
            applicationPartManager.FeatureProviders.Add(
                new GenericTypeControllerFeatureProvider(Assembly.GetEntryAssembly())));

        return serviceCollection;
    }
}