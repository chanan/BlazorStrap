using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using BlazorStrap.Shared.Components.Common;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components.Web.Virtualization;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract partial class ColumnBase<TGridItem> : ComponentBase, IDisposable, IColumnBase<TGridItem>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [CascadingParameter] public ColumnState<TGridItem>? ColumnState { get; set; }
    
    //Not used but keeps hot reload from breaking
    [Parameter] public RenderFragment? ChildContent { get; set; } 
    [Parameter] public RenderFragment<PlaceholderContext>? VirtualPlaceholder { get; set; }
    [Parameter] public bool IsSortable { get; set; }
    [Parameter] public string? Title { get; set; } 
    [Parameter] public Func<SortData<TGridItem>, SortData<TGridItem>>? CustomSort { get; set; }
    [Parameter] public bool InitialSorted { get; set; }
    [Parameter] public bool InitialSortDescending { get; set; } = false;
    [Parameter] public string? Class { get; set; } = string.Empty;
    [Parameter] public string? Style { get; set; } = string.Empty;
    [Parameter] public Func<TGridItem, string>? ClassFunc { get; set; }
    [Parameter] public Func<TGridItem, string>? StyleFunc { get; set; }
    
    [Parameter] public bool IsFilterable { get; set; }
    [Parameter] public int MaxTextWidth { get; set; }
    
    public virtual RenderFragment<IColumnHeaderAccessor>? ColumnOptions { get; set; }
    public int SortOrder { get; set; }
    public IColumnHeaderAccessor? ColumnHeaderAccessor;
    
    public string PropertyPath { get; internal set; }= string.Empty;
    
    public virtual RenderFragment<TGridItem>? Content { get; set; }
    public virtual RenderFragment<IColumnHeaderAccessor>? Header { get; set; }
    public virtual RenderFragment? Footer { get; set; }
    private bool _isInitialized;
    public void CreateColumnHeaderAccessor()
    {
        if(ColumnState?.DataGrid is null || _isInitialized) return;
        var columnHeaderAccessor = new ColumnHeaderAccessor();
        if (ColumnState is not null)
        {
            columnHeaderAccessor.IsFilteredFunc = () => ColumnState.DataGrid.ColumnFilters.Any(x => x.Property == PropertyPath);
            columnHeaderAccessor.IsSortedFunc = () => ColumnState.IsSorted(Id);
            columnHeaderAccessor.IsSortedDescendingFunc = () => ColumnState.IsSortedDescending(Id);
            columnHeaderAccessor.RefreshDataTableAsync = ColumnState.DataGrid.RefreshDataAsync;
            columnHeaderAccessor.FilterButtonClickedAsync = FilterButtonClickedAsync;
            var columnFilter = ColumnState.DataGrid.ColumnFilters.FirstOrDefault(x => x.Property == PropertyPath);
            columnHeaderAccessor.GetFilterFunc = () => ColumnState.DataGrid.ColumnFilters.FirstOrDefault(x => x.Property == PropertyPath)?.Value ?? string.Empty;
            columnHeaderAccessor.SetFilterFunc = (x) =>
            {
                if (ColumnState.DataGrid.ColumnFilters.All(q => q.Property != PropertyPath))
                {
                    var newColumnFilter = new ColumnFilter<TGridItem>(PropertyPath, Operator.Contains, null);
                    ColumnState.DataGrid.ColumnFilters.Add(newColumnFilter);
                }
                var columnFilter = ColumnState.DataGrid.ColumnFilters.FirstOrDefault(x => x.Property == PropertyPath);
          
                if (columnFilter is not null)
                {
                    columnFilter.Value = x;
                }
                return x;
            };            
            ColumnHeaderAccessor = columnHeaderAccessor;   
        }
        _isInitialized = true;
    }
    
    protected Task FilterButtonClickedAsync()
    {
        if (string.IsNullOrEmpty(PropertyPath)) throw new InvalidOperationException("PropertyPath is unknown. You ");
        if (ColumnState is null) return Task.CompletedTask;
        if (ColumnState.DataGrid.ColumnFilters.All(q => q.Property != PropertyPath))
        {
            var columnFilter = new ColumnFilter<TGridItem>(PropertyPath, Operator.Contains, null);
            ColumnState.DataGrid.ColumnFilters.Add(columnFilter);
        }

        return Task.CompletedTask;
    }
    
    public string? SortClassBuilder => new CssBuilder()
        .AddClass("sort-multi", ColumnState!.SortOrder(Id) > 0)
        .AddClass("grid-header-button")
        .AddClass("sort-by", IsSortable && ! ColumnState!.IsSorted(Id))
        .AddClass("sort", IsSortable && ColumnState!.IsSorted(Id))
        .AddClass("sort-desc", IsSortable && ColumnState!.IsSorted(Id) && ColumnState!.IsSortedDescending(Id))
        .Build().ToNullString();
    
    public string? ClassBuilder => new CssBuilder()
        .AddClass(Class)
        .Build().ToNullString();
    
    public void Dispose() => ColumnState?.RemoveColumn(this);
    public abstract void CellContent(RenderTreeBuilder __builder, TGridItem gridItem);
    public abstract void BuildHeader(RenderTreeBuilder __builder, IColumnHeaderAccessor columnHeaderAccessor);
    
}