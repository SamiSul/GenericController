using Microsoft.EntityFrameworkCore;

namespace GenericController.Utils;

public static class EntityFrameworkCore<TEntity> where TEntity : class
{
    public static IQueryable<TEntity> GenerateQueryWithInclude(IQueryable<TEntity> queryable, string includes)
    {
        var splitIncludes = includes.Split(",");

        return splitIncludes.Aggregate(queryable, (current, include) => current.Include(include));
    }
}