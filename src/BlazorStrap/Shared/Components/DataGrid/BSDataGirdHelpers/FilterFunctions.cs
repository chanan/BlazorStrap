using System.Linq.Expressions;

namespace BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;

internal static class FilterFunctions
{
    internal static IQueryable<TGridItem> FiltersColumns<TGridItem>(this IQueryable<TGridItem> items, ICollection<IColumnFilter<TGridItem>> columnFilters)
    {
        var filteredItems = items;
        foreach (var filter in columnFilters)
        {
            var filterValue = filter.Value;
            if (filterValue is not null || filter.Operator == Operator.IsNotEmpty || filter.Operator == Operator.IsEmpty)
            {
                //if value is typeof string or nullable string continue
                if (filterValue is string or null && (filter.Operator is Operator.GreaterThan or Operator.GreaterThanOrEqual or Operator.LessThan or Operator.LessThanOrEqual )) continue;
               
             
                var propertyExpression = ExpressionHelper.GetExpression<TGridItem>(filter.Property);
                var property = propertyExpression.Body;
                var propertyType = ExpressionHelper.GetPropertyType<TGridItem>(filter.Property);
                
                // Convert the filter value to the correct type
                var constant = Expression.Constant(Convert.ChangeType(filterValue, propertyType), propertyType);
                var castedProperty = Expression.Convert(property, propertyType);
                Expression body = filter.Operator switch
                {
                    Operator.Equal => Expression.Equal(castedProperty, constant),
                    Operator.NotEqual => Expression.NotEqual(castedProperty, constant),
                    Operator.StartsWith => Expression.Call(castedProperty, typeof(string).GetMethod("StartsWith", new[] { typeof(string) })!, constant),
                    Operator.EndsWith => Expression.Call(castedProperty, typeof(string).GetMethod("EndsWith", new[] { typeof(string) })!, constant),
                    Operator.Contains => Expression.Call(castedProperty, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant),
                    Operator.NotContains => Expression.Not(Expression.Call(castedProperty, typeof(string).GetMethod("Contains", new[] { typeof(string) })!, constant)),
                    Operator.IsEmpty => Expression.OrElse(
                        Expression.Equal(castedProperty, Expression.Constant(null, propertyType)),
                        Expression.Equal(castedProperty, Expression.Constant(string.Empty, propertyType))
                    ),
                    Operator.IsNotEmpty => Expression.AndAlso(
                        Expression.NotEqual(castedProperty, Expression.Constant(null, propertyType)),
                        Expression.NotEqual(castedProperty, Expression.Constant(string.Empty, propertyType))
                    ),
                    Operator.GreaterThan => Expression.GreaterThan(castedProperty, constant),
                    Operator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(castedProperty, constant),
                    Operator.LessThan => Expression.LessThan(castedProperty, constant),
                    Operator.LessThanOrEqual => Expression.LessThanOrEqual(castedProperty, constant),
                    _ => throw new ArgumentOutOfRangeException()
                };
                var lambda = Expression.Lambda<Func<TGridItem, bool>>(body, propertyExpression.Parameters);
                filteredItems = filteredItems.Where(lambda);   
            }
        }     
        return filteredItems;
    }
}
// Move this later to do more unit testing on internal functions
public static class UnitTest
{
    public static IQueryable<TGridItem> FilterFunctions_FiltersColumns<TGridItem>(this IQueryable<TGridItem> items, ICollection<IColumnFilter<TGridItem>> columnFilters)
    {
        return FilterFunctions.FiltersColumns(items, columnFilters);
    }
}