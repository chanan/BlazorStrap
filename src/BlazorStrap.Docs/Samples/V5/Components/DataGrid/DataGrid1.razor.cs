using BlazorComponentUtilities;
using BlazorStrap_Docs.SamplesHelpers.Content.Tables;
using BlazorStrap;
using BlazorStrap.Extensions;
using BlazorStrap.V5;
using BlazorStrap.V5.DataGrid;

namespace BlazorStrap_Docs.Samples.V5.Components.DataGrid;

public partial class DataGrid1
{
    private PaginationState _pagination = new PaginationState() { ItemsPerPage = 10 };
    private BSDataGrid<Employee> _dataGrid;
    private readonly Table2Model _sampleData = new Table2Model();
    private ICollection<Employee> _employees;

    private Func<SortData<Employee>, SortData<Employee>> _nameSort = data =>
    {
        data.Ordered = data.Descending ? data.Query.OrderByDescending(q => q.Name) : data.Query.OrderBy(q => q.Name);
        return data;
    };

    protected override void OnInitialized()
    {
        _employees = _sampleData.DataSet;
    }

    private string? _filterClass => new CssBuilder()
        .AddClass("system-uicons--filter")
        .Build().ToNullString();

    private string? _menuClass => new CssBuilder()
        .AddClass("system-uicons--menu")
        .Build().ToNullString();
}