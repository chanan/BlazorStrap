@using System.Net.Http.Json
@using System.Text.Json
@using System.Text.Json.Serialization
@using BlazorStrap_Docs.Models
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
@using Microsoft.EntityFrameworkCore
@inject NavigationManager NavigationManager
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
<div class="input-group">
    <span class="input-group-text">Search Product Name</span>
    <BSInput InputType="InputType.Text" @bind-Value="_productName" DebounceInterval="300" UpdateOnInput="true" OnValueChange="(string s) => UpdateGridItemsAsync()" />
</div>
<div>
<BSDataGrid ItemsProvider="_itemsProvider" IsStriped="true" IsSmall="true" Pagination="_pagination" @ref="_dataGrid">
    <Columns>
        <PropertyColumn Property="e => e.ProprietaryName" Title="Product Name" Class="col-3" MaxTextWidth="20" IsSortable="true" IsFilterable="true"/>
        <PropertyColumn Property="e => e.ProductType" Title="Product Type" Class="col-3" IsSortable="true" IsFilterable="true"/>
        <PropertyColumn Property="e => e.MarketingStartDate" Title="Start" Class="col-3" IsSortable="true"/>
        <PropertyColumn Property="e => e.ApplicationNumberOrCitation" Title="Application/Citation" Class="col-3" IsSortable="true"/>
    </Columns>
</BSDataGrid>
</div>
@code {
    private GridItemsProvider<FdaNsde> _itemsProvider;
    private PaginationState _pagination = new PaginationState() { ItemsPerPage = 10 };
    private AppDbContext? _db;
    private BSDataGrid<FdaNsde> _dataGrid;
    private string? _productName;

    protected override void OnInitialized()
    {
        _itemsProvider = async request =>
        {
            var sort = request.SortColumns.FirstOrDefault(x => x.Sorted);
            var filterList = request.FilterColumns;
            var filterQuery = "";
            if(!string.IsNullOrEmpty(_productName))
            {
                filterQuery += " AND proprietary_name:[\"" + _productName + "\" TO \"" + GetNextChar(_productName) + "\"]";
            }
            
            if(filterList.Count > 0)
            {
                foreach (var filter in filterList)
                {
                    if (!string.IsNullOrEmpty(filter.Value))
                    {
                        filterQuery += " AND " + GetJsonName(filter.Property) + ":[\"" + filter.Value + "\" TO \"" + GetNextChar(filter.Value) + "\"]";
                    }
                }
            }
            var sortQuery = "";
            if (sort != null)
            {
                var order = sort.Descending ? "des" : "asc";
                var sortProperty = GetJsonName(sort.PropertyPath);
                sortQuery = $"{sortProperty}:{order}";
            }

            var url = NavigationManager.GetUriWithQueryParameters("https://api.fda.gov/other/nsde.json", new Dictionary<string, object?>
            {
                {"skip", request.StartIndex},
                {"limit", request.Count},
                {"sort", sortQuery},
                {"search", "marketing_start_date:[20210401 TO 20220101]" + filterQuery }
                
            });
            var httpClient = new HttpClient();
            var httpRequest = await httpClient.GetAsync(url);
            if(!httpRequest.IsSuccessStatusCode)
            {
                return DataGridResponce.Create(new List<FdaNsde>(), 0);
            }
            var data = await httpRequest.Content.ReadFromJsonAsync<FdaNsdeResult>();
            return DataGridResponce.Create(data.Results, data.Meta.Results.Total);
        };
    }

    private static string GetJsonName(string property)
    {
        return property switch
        {
            "ProprietaryName" => "proprietary_name",
            "ProductType" => "product_type",
            "MarketingStartDate" => "marketing_start_date",
            "ApplicationNumberOrCitation" => "application_number_or_citation",
            _ => ""
        };
    }
    
    private Func<SortData<Employee>, SortData<Employee>> _nameSort = data =>
    {
        data.Ordered = data.Descending ? data.Query.OrderByDescending(q => q.Name) : data.Query.OrderBy(q => q.Name);
        return data;
    };

    private static string GetNextChar(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "A";
        }
        var lastChar = value[^1];
        if (lastChar == 'Z')
        {
            return value + "A";
        }
        return value[..^1] + (char)(lastChar + 1);
    }
    private Task UpdateGridItemsAsync()
    {
        return _dataGrid.RefreshItemsAsync();
    }

}