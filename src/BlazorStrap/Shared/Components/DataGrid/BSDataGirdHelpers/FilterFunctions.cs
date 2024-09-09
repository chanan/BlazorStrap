using System.Linq.Expressions;

namespace BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;

internal static class FilterFunctions
{
    internal static void SetFilter<TItem>(this Guid id, string value, List<FilterColumn<TItem>> filterColumns)
    {
        var column = filterColumns.FirstOrDefault(x => x.Id == id);
        if (column == null) return;
        column.Value = value;
    }
    
    internal static IQueryable<TGridItem> GetFilters<TGridItem>(this IQueryable<TGridItem> items, List<FilterColumn<TGridItem>> filterColumns)
    {
        var filteredItems = items;
        foreach (var column in filterColumns)
        {
            if (string.IsNullOrWhiteSpace(column.Value)) continue;
            filteredItems = filteredItems.Where(WherePredicate<TGridItem>(column.PropertyPath, column.Value));
        }
        return filteredItems;
    }
    
    private static Expression<Func<TItem, bool>> WherePredicate<TItem>(string propertyPath, string filterValue)
    {
        var parameter = Expression.Parameter(typeof(TItem), "x");
        Expression property = parameter;
    
        foreach (var member in propertyPath.Split('.'))
        {
            property = Expression.Property(property, member);
        }
    
        var constant = Expression.Constant(filterValue);
        var body = Expression.Equal(property, constant);
        var lambda = Expression.Lambda<Func<TItem, bool>>(body, parameter);
        return lambda;
    }
}