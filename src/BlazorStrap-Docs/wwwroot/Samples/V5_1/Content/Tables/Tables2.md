<!--\\-->
@using System.ComponentModel
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
<!--//-->
<BSDataTable FetchItems="FetchItems" PaginationBottom="true" StartPage="_startPage" RowsPerPage="20" Context="item">
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
</BSDataTable>

@code
{
    private readonly int _startPage = 2;
    private readonly Table2Model _model = new Table2Model();

    private async Task<(IEnumerable<Employee>, int)> FetchItems(DataRequest dataRequest)
    {
        var count = _model.DataSet.Count();
        if (dataRequest.FilterColumnProperty != null && dataRequest.Filter != null)
        {
            var data = _model.DataSet.Where(q =>
                (q.Name.ToLower().Contains(dataRequest.Filter) && nameof(q.Name) == dataRequest.FilterColumn) ||
                (q.Email.ToLower().Contains(dataRequest.Filter) && nameof(q.Email) == dataRequest.FilterColumn)
                ).ToList();
            count = data.Count();
            return (data, count);
        }
        if (dataRequest.SortColumnProperty != null)
        {
            if (dataRequest.Descending)
                return (await _model.DataSet.OrderByDescending(x => dataRequest.SortColumnProperty.GetValue(x)).Skip(dataRequest.Page * 20).Take(20).ToListAsync(), count);
            return (await _model.DataSet.OrderBy(x => dataRequest.SortColumnProperty.GetValue(x)).Skip(dataRequest.Page * 20).Take(20).ToListAsync(), count);
        }
        return (await _model.DataSet.Skip(dataRequest.Page * 20).Take(20).ToListAsync(),count);
    }
}