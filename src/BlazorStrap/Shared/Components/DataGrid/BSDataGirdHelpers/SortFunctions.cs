using System.Linq.Expressions;
using BlazorStrap.Shared.Components.DataGrid.Models;

namespace BlazorStrap.Shared.Components.DataGrid.BSDataGirdHelpers;

internal static class SortFunctions
{
    internal static IOrderedQueryable<TGridItem>? SortColumns<TGridItem>(this IQueryable<TGridItem> sortedItems, ICollection<SortColumn<TGridItem>> sortColumns)
    {
        IOrderedQueryable<TGridItem>? orderedItems = null; 
        //Sort by the columns that are not custom sorted
        foreach (var sortColumn in sortColumns.OrderBy(x => x.Order))
        {
            if(!sortColumn.Sorted) continue;
            var columnBase = sortColumn.Column;
            if (columnBase?.CustomSort != null)
            {
                var customSort = columnBase.CustomSort(new SortData<TGridItem>(sortedItems, sortColumn.Descending));
                if (customSort.Ordered != null)
                {
                    sortColumn.Sorted = true;
                    sortColumn.Descending = customSort.Descending;
                    orderedItems = customSort.Ordered;
                }
            }
            else
            {
                if (orderedItems != null)
                {
                    orderedItems = sortColumn.Descending
                        ? orderedItems.ThenByDescending(OrderByPredicate<TGridItem,object>(sortColumn.PropertyPath))
                        : orderedItems.ThenBy(OrderByPredicate<TGridItem,object>(sortColumn.PropertyPath));
                }
                else
                {
                    orderedItems = sortColumn.Descending
                        ? sortedItems.OrderByDescending(OrderByPredicate<TGridItem,object>(sortColumn.PropertyPath))
                        : sortedItems.OrderBy(OrderByPredicate<TGridItem,object>(sortColumn.PropertyPath));
                }
            }
        }
        
        return orderedItems;
    }
    
    private static Expression<Func<TItem, TKey>> OrderByPredicate<TItem, TKey>(string columnPropertyPath)
    {
        Console.WriteLine(columnPropertyPath);
        var parameter = Expression.Parameter(typeof(TItem), "x");
        Expression property = parameter;

        foreach (var member in columnPropertyPath.Split('.'))
        {
            property = Expression.Property(property, member);
        }

        var lambda = Expression.Lambda<Func<TItem, TKey>>(property, parameter);
        return lambda;
    }
    
}