using System.Reflection;
using GenericController.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.Extensions.DependencyInjection;

namespace GenericController.Extensions;

public static class ServiceCollection
{
    public static IServiceCollection AddGenericController(this IServiceCollection serviceCollection)
    {
        var mvcBuilder =
            serviceCollection.AddMvcWithControllerConvention(new GenericControllerRouteConvention());

        mvcBuilder.AddControllerFeatureProvider(new GenericTypeControllerFeatureProvider(Assembly.GetEntryAssembly()));


        return serviceCollection;
    }

    private static IMvcBuilder AddMvcWithControllerConvention(this IServiceCollection serviceCollection,
        IControllerModelConvention convention)
    {
        return serviceCollection.AddMvc(options => options.Conventions.Add(convention));
    }

    private static IMvcBuilder AddControllerFeatureProvider(this IMvcBuilder mvcBuilder,
        IApplicationFeatureProvider<ControllerFeature> controllerFeatureProvider)
    {
        return mvcBuilder.ConfigureApplicationPartManager(applicationPartManager =>
            applicationPartManager.FeatureProviders.Add(controllerFeatureProvider));
    }
    
}