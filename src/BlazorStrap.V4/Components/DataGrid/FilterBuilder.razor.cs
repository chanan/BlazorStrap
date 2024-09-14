using BlazorStrap.V4;
using BlazorStrap.V4.Internal.Do.Not.Use;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V4.Components.DataGrid;

public partial class FilterBuilder<TGridItem> : ComponentBase, IDisposable
{
    [Parameter] public BSDataGridCore<TGridItem> DataGrid { get; set; }
    private BSCollapse _ref;
    private bool _shown;
    protected override void OnInitialized()
    {
        DataGrid.OnColumnFilterClicked += OnColumnFilterClicked;
    }

    private Task OnColumnFilterClicked()
    {
        return _ref.ShowAsync();
    }


    // private Task OnValueChange(string value)
    // {
    //     Filter.ValueAsString = value;
    //   //  return DataGrid.RefreshDataAsync();;
    // }

   
    private ICollection<string> EnumToCollection<T>()
    {
        return Enum.GetNames(typeof(T)).ToList();
    }
    public void Dispose()
    {
        DataGrid.OnColumnFilterClicked -= OnColumnFilterClicked;
    }

    private Task AddFilter()
    {
        return DataGrid.AddFilterAsync();
    }
    private Task RemoveFilter(IColumnFilterInternal<TGridItem> filter)
    {
        DataGrid.ColumnFilters.Remove(filter);
        return DataGrid.RefreshDataAsync();
    }
    private async Task ClearFilters()
    {
        DataGrid.ColumnFilters.Clear();
        await _ref.HideAsync();
        await  DataGrid.RefreshDataAsync();
    }
    
    private Task OnValueChange<T>(IColumnFilterInternal<T> filter, string value)
    {
        filter.ValueAsString = value;
        return DataGrid.RefreshDataAsync();
    }

    private Task OnOperatorChange<T>(IColumnFilterInternal<T> filter,Operator @operator)
    {
        filter.Operator = @operator;
        return DataGrid.RefreshDataAsync();
    }

    private Task OnPropertyChange<T>(IColumnFilterInternal<T> filter, string property)
    {
        filter.Property = property;
        return DataGrid.RefreshDataAsync();
    }
}