using System.Collections;
using System.Linq.Expressions;
using DataEntity.Models;
using ProjectBase.Core;
using X.PagedList;

namespace ProjectBase.Services.Helpers;

public class CollectionUtils
{
    public static IEnumerable<T> ApplyPagination <T>(IEnumerable<T> query,int page, int pageSize)
    {
        if (query == null)
            throw new ArgumentNullException(nameof(query));
            
        if (page < 1)
            throw new ArgumentException("Page must be greater than or equal to 1", nameof(page));
            
        if (pageSize < 1)
            throw new ArgumentException("Page size must be greater than or equal to 1", nameof(pageSize));
            
        return query.Skip((page - 1) * pageSize).Take<T>(pageSize);
    }
    
    public static IEnumerable<T> ApplySorting<T>(IEnumerable<T> source, string sortBy)
    {
        if (string.IsNullOrEmpty(sortBy)) return source;

        var applySorting = source.ToList();
        try
        {
            var sortExpression = sortBy.Split('-');
            var propertyName = sortExpression[0];
            var descending = sortExpression.Length > 1 && sortExpression[1].Equals(Constants.SortingDirection.Descending, StringComparison.OrdinalIgnoreCase);

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.PropertyOrField(parameter, propertyName);
            var lambda = Expression.Lambda<Func<T, object>>(
                Expression.Convert(property, typeof(object)),
                parameter
            );

            return descending
                ? applySorting.OrderByDescending(lambda.Compile())
                : applySorting.OrderBy(lambda.Compile());
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error applying sorting: {ex.Message}");
            return applySorting;
        }
    }
}