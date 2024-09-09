<!--\\-->
@using System.ComponentModel
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
<!--//-->
<div class="@BS.Input_Group mb-2">
    <span class="@BS.Input_Group_Text">Custom Filter</span>
    <BSInput InputType="InputType.Text" placeholder="By name or Email" Value="_customFilter" ValueChanged="(string e) => CustomFilter(e)" UpdateOnInput="true" />
</div>

<BSDataTable Items="_employees" TotalItems="_count" OnChange="OnChange" PaginationBottom="true" StartPage="_startPage" RowsPerPage="20" Context="item" @ref="_customFilterRef">
    <!--\\-->
    <Header>
        <BSDataTableHead TValue="Employee" Sortable="true" Column="@(nameof(Employee.Id))">Id</BSDataTableHead>
        <BSDataTableHead TValue="Employee" Sortable="true" Column="@(nameof(Employee.Name))" ColumnFilter="true">Name</BSDataTableHead>
        <BSDataTableHead TValue="Employee" Sortable="true" Column="@(nameof(Employee.Email))" ColumnFilter="true">Email</BSDataTableHead>
    </Header>
    <Body>
        <BSDataTableRow Color="item.RowColor">
            <BSTD>
                @item.Id
            </BSTD>
            <BSTD>
                @item.Name
            </BSTD>
            <BSTD>
                @item.Email
            </BSTD>
        </BSDataTableRow>

    </Body>
    <!--//-->
</BSDataTable>

@code
{
    private string? _customFilter;
    private BSDataTable<Employee> _customFilterRef = new BSDataTable<Employee>();
    private readonly int _startPage = 2;
    private readonly Table2Model _model = new Table2Model();
    private int _count = 0;
    private IEnumerable<Employee> _employees = new List<Employee>();
    protected override void OnInitialized()
    {
        _count = _model.DataSet.Count();
        _employees = _model.DataSet.Skip(_startPage * 20).Take(20);
    }

    private void OnChange(DataRequest dataRequest)
    {
        _count = _model.DataSet.Count();
        if (dataRequest.FilterColumnProperty != null && dataRequest.Filter != null)
        {
            _employees = _model.DataSet.Where(q =>
                (q.Name.ToLower().Contains(dataRequest.Filter) && nameof(q.Name) == dataRequest.FilterColumn) ||
                (q.Email.ToLower().Contains(dataRequest.Filter) && nameof(q.Email) == dataRequest.FilterColumn)
                ).ToList();
            _count = _employees.Count();
        }
        else if (dataRequest.SortColumnProperty != null)
        {
            if (dataRequest.Descending)
                _employees = _model.DataSet.OrderByDescending(x => dataRequest.SortColumnProperty.GetValue(x)).Skip(dataRequest.Page * 20).Take(20);
            else
                _employees = _model.DataSet.OrderBy(x => dataRequest.SortColumnProperty.GetValue(x)).Skip(dataRequest.Page * 20).Take(20);
        }
        else
        {
            _employees = _model.DataSet.Skip(dataRequest.Page * 20).Take(20);
        }
        StateHasChanged();
    }
    private void CustomFilter(string e)
    {
        _customFilter = e;
        _customFilterRef.Page = 1;
        if (!string.IsNullOrEmpty(_customFilter))
        {
            _employees = _model.DataSet.Where(q => q.Name.ToLower().Contains(_customFilter.ToLower()) || q.Email.ToLower().Contains(_customFilter.ToLower())).ToList();
            _count = _employees.Count();
        }
        else
        {
            _employees = _model.DataSet.Take(20);
            _count = _employees.Count();
        }
        StateHasChanged();
    }
}