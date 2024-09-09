using BlazorStrap.Shared.Components.DataGrid.Models;

namespace BlazorStrap;

public delegate ValueTask<DataGridResponce<TGridItem>> GridItemsProvider<TGridItem>(
    DataGridRequest<TGridItem> request);