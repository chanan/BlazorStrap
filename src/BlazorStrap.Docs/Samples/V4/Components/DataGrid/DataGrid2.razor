@implements IDisposable
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
@using Microsoft.EntityFrameworkCore
@inject IDbContextFactory<AppDbContext> dbFactory

<div class="input-group">
    <span class="input-group-text">Items per page</span>
    <select class="form-select" aria-label="Items Per Page" @bind="@_pagination.ItemsPerPage">
        <option>5</option>
        <option>10</option>
        <option>20</option>
        <option>50</option>
    </select>
    <span class="input-group-text">Pagination Placement</span>
    <select class="form-select" aria-label="Pagination Placement" @bind="_pagination.Placement">
        <option value="@Placement.Top">Top</option>
        <option value="@Placement.TopStart">TopStart</option>
        <option value="@Placement.TopEnd">TopEnd</option>
        <option value="@Placement.Left">Left - Not Supported</option>
        <option value="@Placement.Bottom">Bottom</option>
        <option value="@Placement.BottomStart">BottomStart</option>
        <option value="@Placement.BottomEnd">BottomEnd</option>
    </select>

</div>
<BSDataGrid IsStriped="true" IsSmall="true" Items="@_db.Employees.Include(x => x.NameObject)" IsMultiSort="true" Pagination="_pagination">
    <Columns>
        <PropertyColumn Property="e => e.Id" IsSortable="true" IsFilterable="true" CustomSort="_nameSort" >
            <ColumnOptions>
                <div class="mx-2">
                    <BSInput InputType="InputType.Text" @bind-Value="context.Filter" DebounceInterval="200" UpdateOnInput="true" placeholder="Filter..." TValue="string"/>
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="e => e.NameObject.FirstName" IsSortable="true" IsFilterable="true">
            <ColumnOptions>
                <div class="mx-2">
                    <BSInput InputType="InputType.Text" bind-Value="context.Filter" placeholder="Filter..." TValue="string"/>
                </div>
            </ColumnOptions>
        </PropertyColumn>
        <PropertyColumn Property="e => e.NameObject.LastName" IsSortable="true"/>
        <PropertyColumn Property="e => e.Email" IsSortable="true"/>
    </Columns>
</BSDataGrid>

@code {
    private PaginationState _pagination = new PaginationState() { ItemsPerPage = 10 };
    private AppDbContext? _db;
    private string _filter = string.Empty;

    //This is just a auto fill class for the table for sample data.
    private readonly Table2Model _sampleData = new Table2Model();
    private bool _isLoaded;

    private Func<SortData<Employee>, SortData<Employee>> _nameSort = data =>
    {
        data.Ordered = data.Descending ? data.Query.OrderByDescending(q => q.Name) : data.Query.OrderBy(q => q.Name);
        return data;
    };

    protected override void OnInitialized()
    {
        _db = dbFactory.CreateDbContext();
        if (!_db.Employees.Any() && !_isLoaded)
        {
            _db.Employees.AddRange(_sampleData.DataSet);
            _db.SaveChanges();
            _isLoaded = true;
        }
    }

    public void Dispose()
    {
        _db?.Dispose();
    }
}