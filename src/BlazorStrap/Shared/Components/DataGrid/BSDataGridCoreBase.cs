using BlazorStrap.Shared.Components.Content;
using BlazorStrap.Shared.Components.DataGrid.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BlazorStrap.Shared.Components.DataGrid;

[CascadingTypeParameter(nameof(TGridItem))]
public abstract class BSDataGridCoreBase<TGridItem> : BSTableBase , IBSDataGridBase<TGridItem>
{
    #region Blazor Properties
    protected MoveRenderLast MoveRenderLastRef = null!;
    protected Virtualize<(int,TGridItem)>? VirtualizeRef { get; set; } = null;
    [Parameter] public RenderFragment? Columns { get; set; }
    [Parameter] public GridItemsProvider<TGridItem>? ItemsProvider { get; set; }
    [Parameter] public bool IsVirtualized { get; set; } = false;
    [Parameter] public IQueryable<TGridItem>? Items { get; set; }
    [Parameter] public bool Paginated { get; set; } = false;

    [Parameter] public string? RowClass { get; set; }

    [Parameter] public Func<TGridItem, string>? RowClassFunc { get; set; }

    [Parameter] public string? RowStyle { get; set; }

    [Parameter] public Func<TGridItem, string>? RowStyleFunc { get; set; }
    
    [Parameter] public bool IsMultiSort { get; set; } = false;
    [Parameter] public ColumnState<TGridItem> ColumnState { get; set; } = null!;
    [Parameter] public string MultiSortClass { get; set; } = "badge bg-info text-dark";
    
    protected RenderFragment? BodyTemplate { get; set; }
    protected RenderFragment? FooterTemplate { get; set; }
    protected RenderFragment? HeaderTemplate { get; set; }
    #endregion

    #region Initialization
    private object? _lastItemsProvider;
    private bool _initialized;

    protected BSDataGridCoreBase()
    {
        HeaderTemplate = RenderHeader;
        FooterTemplate = RenderFooter;
        BodyTemplate = RenderBody;
    }
    

    protected override Task OnParametersSetAsync()
    {
        if(Items is not null && ItemsProvider is not null) throw new NullReferenceException("Both Items and ItemsProvider cannot be set. Only one can be set");

        var newItemsProvider = Items ?? (object?)ItemsProvider;
        var itemsProviderChanged = newItemsProvider != _lastItemsProvider;
        if (itemsProviderChanged)
        {
            _lastItemsProvider = newItemsProvider;
        }
        var mustRefresh = itemsProviderChanged;
        return (ColumnState.Columns.Any() && mustRefresh) ? 
            RefreshDataCoreAsync() :
            Task.CompletedTask;
    }

    #endregion
    
    /// <summary>
    /// Do not modify this property. It is used to store the items that are displayed in the grid.
    /// </summary>
    protected ICollection<TGridItem> DisplayedItems = new List<TGridItem>();
    private CancellationTokenSource? _pendingDataLoadCancellationTokenSource;
    public void ClearSort(Guid id)
    {
        var sortColumn = ColumnState.SortColumns.FirstOrDefault(x => x.Id == id);
        if (sortColumn == null ) return;
        sortColumn.Sorted = false;
        sortColumn.Descending = sortColumn.Column.InitialSortDescending;
    }
    
    public Task ApplySortAsync(MouseEventArgs e, Guid id)
    {
        var sortColumns = ColumnState.SortColumns.FirstOrDefault(x => x.Id == id);
        if (sortColumns == null) return Task.CompletedTask;
        if (IsMultiSort && e.CtrlKey)
        {
            var count = ColumnState.SortColumns.Count(x => x.Sorted);
            if (!sortColumns.Sorted)
            {
                sortColumns.Order = count + 1;
                sortColumns.Column.SortOrder = count + 1;
                sortColumns.Descending = sortColumns.Column.InitialSortDescending;
            }
            //TODO: Set class so user knows it is sorted
            
            sortColumns.Descending = !sortColumns.Descending;
            sortColumns.Sorted = true;
        }
        else
        {
            foreach (var sortColumn in ColumnState.SortColumns)
            {
                if (sortColumn.Id == id)
                {
                    sortColumn.Order = 1;
                    sortColumn.Column.SortOrder = 1;
                    if (sortColumn.Sorted)
                    {
                        sortColumn.Descending = !sortColumn.Descending;
                    }
                    else
                    {
                        sortColumn.Descending = sortColumn.Column.InitialSortDescending;
                    }

                    sortColumn.Sorted = true;
                }
                else
                {
                    sortColumn.Sorted = false;
                    sortColumn.Column.SortOrder = 1;
                    sortColumn.Order = 1;
                    sortColumn.Descending = sortColumn.Column.InitialSortDescending;
                }
            }
        }

        return RefreshDataAsync();
    }

    private async Task RefreshDataAsync()
    {
        await RefreshDataCoreAsync();
        await InvokeAsync(StateHasChanged);
    }
    private async Task RefreshDataCoreAsync()
    {
        
        if(_pendingDataLoadCancellationTokenSource is not null)
        {
            _pendingDataLoadCancellationTokenSource.Cancel();
        }
        var currentCancellationTokenSource = _pendingDataLoadCancellationTokenSource = new CancellationTokenSource();
        
        if(IsVirtualized && VirtualizeRef is not null)
        {
            await VirtualizeRef!.RefreshDataAsync();
            _pendingDataLoadCancellationTokenSource = null;
        }
        else
        {
            var request = new DataGridRequest<TGridItem>(
                startIndex: 0,
                count: null,
                sortColumns: ColumnState.SortColumns,
                filterColumns: ColumnState.FilterColumns,
                cancellationToken: currentCancellationTokenSource.Token
            );
            var responce = await FetchItemsAsync(request);
            if(!currentCancellationTokenSource.IsCancellationRequested)
            {
                DisplayedItems = responce.Items;
                _pendingDataLoadCancellationTokenSource = null;
            }
        }
    }

    private async ValueTask<DataGridResponce<TGridItem>> FetchItemsAsync(DataGridRequest<TGridItem> request)
    {
        
        if (ItemsProvider is not null)
        {
            return await ItemsProvider(request);
        }
        else if (Items is not null)
        {
            var totalItemCount = Items.Count();
            //TODO: Apply filters here
            var responce = request.ApplySort(Items, ColumnState.SortColumns).Skip(request.StartIndex);
            if(request.Count.HasValue)
            {
                responce = responce.Take(request.Count.Value);
            }

            var responceItems = responce.ToArray();
            return DataGridResponce.Create(responceItems, totalItemCount);
        }

        return DataGridResponce.Create(Array.Empty<TGridItem>(), 0);
    }

    public async ValueTask<ItemsProviderResult<(int,TGridItem)>> VirtualizedProvider(ItemsProviderRequest request)
    {
        await Task.Delay(100);
        
        if(Items == null && ItemsProvider == null) throw new NullReferenceException("Both Items and ItemsProvider cannot be null. One or the other must be set");
        if(ItemsProvider != null && Items != null) throw new NullReferenceException("Both Items and ItemsProvider cannot be set. Only one can be set");
        if (ItemsProvider != null)
        {
            //TODO: Get items here
            //TODO: Remove gobal Items call
            if (Items is null) throw new NullReferenceException("Items cannot be null");
            var items = Items.Skip(request.StartIndex).Take(request.Count).ToList();
            return new ItemsProviderResult<(int, TGridItem)>(
                items: items.Select((x, i) => (request.StartIndex + i, x)),
                totalItemCount: Items.Count()
            );
        }

        return default;
    }
    #region Abstract Methods

    protected abstract void RenderBody(RenderTreeBuilder __builder);
    protected abstract void RenderFooter(RenderTreeBuilder __builder);
    protected abstract void RenderHeader(RenderTreeBuilder __builder);
    protected abstract void RenderRow(RenderTreeBuilder __builder, int rowIndex, TGridItem item);

    #endregion

}