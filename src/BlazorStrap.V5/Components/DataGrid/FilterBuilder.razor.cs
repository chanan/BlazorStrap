using BlazorStrap.Shared.Components.DataGrid.Columns;
using BlazorStrap.V5.Internal.Do.Not.Use;
using Microsoft.AspNetCore.Components;

namespace BlazorStrap.V5.Components.DataGrid;

public partial class FilterBuilder<TGridItem>
{
    private EventCallback<object> OnValueChangedCallback => EventCallback.Factory.Create<object>(this, OnValueChange);
    [Parameter] public BSDataGridCore<TGridItem> DataGrid { get; set; }
    protected override void OnInitialized()
    {
        DataGrid.ColumnFilters.Add(Filter);
    }

    public ColumnFilter<int> Filter { get; set; } = new ColumnFilter<int>("Id", Operator.Contains, 1001);

    private async Task OnValueChange(object o)
    {
        await InvokeAsync(StateHasChanged);
        await DataGrid.RefreshDataAsync();;
    }
}