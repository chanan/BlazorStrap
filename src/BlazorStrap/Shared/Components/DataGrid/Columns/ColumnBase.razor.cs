using BlazorComponentUtilities;
using BlazorStrap.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace BlazorStrap.Shared.Components.DataGrid.Columns;

public abstract partial class ColumnBase<TGridItem> : ComponentBase, IDisposable, IColumnBase<TGridItem>
{
    public Guid Id { get; set; } = Guid.NewGuid();
    
    [CascadingParameter] public ColumnState<TGridItem>? ColumnState { get; set; }
    
    //Not used but keeps hot reload from breaking
    [Parameter] public RenderFragment? ChildContent { get; set; } 
    [Parameter] public bool IsSortable { get; set; }
    [Parameter] public string? Title { get; set; } 
    [Parameter] public Func<SortData<TGridItem>, SortData<TGridItem>>? CustomSort { get; set; }
    [Parameter] public bool DefaultSort { get; set; } = false;
    [Parameter] public bool InitialSortDescending { get; set; } = false;

    [Parameter] public string? Class { get; set; } = string.Empty;
    [Parameter] public string? Style { get; set; } = string.Empty;
    [Parameter] public Func<TGridItem, string>? ClassFunc { get; set; }
    [Parameter] public Func<TGridItem, string>? StyleFunc { get; set; }
    [Parameter] public RenderFragment<IColumnBase<TGridItem>>? ColumnOptions { get; set; }
    [Parameter] public int FilterOrder { get; set; }

    private string _filter = string.Empty;
    public string Filter
    {
        get => _filter;
        set
        {
            if (_filter == value) return;
                _filter = value;
            ColumnState?.OnStateChange?.Invoke();
        }
    }
    public int SortOrder { get; set; }

    //public delegate Func<SortData<TItem>, Task<SortData<TItem>>> SortData();
  //  [Parameter] public SortData CustomSort { get; set; }
    
    public virtual RenderFragment? Header { get; set; }
    public virtual RenderFragment<TGridItem>? Content { get; set; }
    public virtual RenderFragment? Footer { get; set; }
    
    internal string PropertyPath = string.Empty;
    
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
    
}