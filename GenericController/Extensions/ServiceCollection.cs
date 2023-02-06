using System.Reflection;
using GenericController.Data;
using GenericController.Utils;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GenericController.Extensions;

public static class ServiceCollection
{
    /// <summary>
    /// Adds and configures the GenericControllerBase
    /// </summary>
    /// <typeparam name="TDbContext">The type of context to be supplied to the default repository implementation</typeparam>
    public static IServiceCollection AddGenericController<TDbContext>(this IServiceCollection serviceCollection)
        where TDbContext : DbContext
    {
        var entryAssembly = Assembly.GetEntryAssembly();
        var mvcBuilder =
            serviceCollection.AddMvcWithControllerConvention(new GenericControllerRouteConvention());

        mvcBuilder.AddControllerFeatureProvider(new GenericTypeControllerFeatureProvider(entryAssembly));

        serviceCollection.AddDbContext<TDbContext>();
        serviceCollection.AddRepository();

        return serviceCollection;
    }

    /// <summary>
    /// Calls IServiceCollection.AddMvc() and passes the specified convention
    /// </summary>
    /// <param name="convention">The IControllerModelConvention to be add when calling IServiceCollection.AddMvc()</param>
    private static IMvcBuilder AddMvcWithControllerConvention(this IServiceCollection serviceCollection,
        IControllerModelConvention convention)
    {
        return serviceCollection.AddMvc(options => options.Conventions.Add(convention));
    }

    /// <summary>
    /// Calls the IMvcBuilder.ConfigureApplicationPartManager() and passes the specified controller feature provider
    /// </summary>
    /// <param name="controllerFeatureProvider">IApplicationFeatureProvider to be added</param>
    private static IMvcBuilder AddControllerFeatureProvider(this IMvcBuilder mvcBuilder,
        IApplicationFeatureProvider controllerFeatureProvider)
    {
        return mvcBuilder.ConfigureApplicationPartManager(applicationPartManager =>
            applicationPartManager.FeatureProviders.Add(controllerFeatureProvider));
    }


    /// <summary>
    /// Registers the default implementation of IRepository
    /// </summary>
    private static IServiceCollection AddRepository(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped(typeof(IRepository<,>), typeof(Repository<,>));
        return serviceCollection;
    }

    /// <summary>
    /// Registers the specified DbContext as a substitute for EntityFramework's DbContext
    /// </summary>
    /// <typeparam name="TDbContext">The type of context to be the substitution of the EFCore's DbContext</typeparam>
    private static IServiceCollection AddDbContext<TContext>(this IServiceCollection serviceCollection)
        where TContext : DbContext
    {
        serviceCollection.AddScoped(typeof(DbContext), typeof(TContext));
        return serviceCollection;
    }
}