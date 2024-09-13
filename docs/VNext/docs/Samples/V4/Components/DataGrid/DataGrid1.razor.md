@using BlazorComponentUtilities
@using BlazorStrap_Docs.SamplesHelpers.Content.Tables
@using BlazorStrap.Extensions
@using BlazorStrap.V4
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
<div>
<BSDataGrid IsStriped="true" IsSmall="true" Items="_employees.AsQueryable()" IsMultiSort="true" @ref="_dataGrid" Pagination="_pagination" IsVirtualized="true">
    <Columns>
        <TemplateColumn IsSortable="true" Property="employee => employee.Id">
            <Header>
                <button class="grid-header-button">Id</button>
            </Header>
            <Content>@context.Id</Content>
        </TemplateColumn>
        <PropertyColumn Property="e => e.NameObject.FirstName" IsSortable="true"/>
        <PropertyColumn Property="e => e.NameObject.LastName" IsSortable="true"/>
        <PropertyColumn Property="e => e.Email" IsSortable="true"/>
    </Columns>
</BSDataGrid>
</div>