namespace BlazorStrap;

public delegate ValueTask<DataGridResponce<TGridItem>> GridItemsProvider<TGridItem>(
    DataGridRequest<TGridItem> request);