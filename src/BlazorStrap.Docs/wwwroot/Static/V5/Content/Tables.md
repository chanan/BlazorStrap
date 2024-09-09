## Tables
#### Component \<BSTable\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter              | Type   | Valid      | Remarks/Output        | 
|------------------------|--------|------------|-----------------------|
| Color                  | Enum   | BSColor    | `.table-[]`           | {.table-striped .p-2}  
| IsBordered             | bool   | true/false | `.table-bordered`     |
| IsBorderLess	          | bool   | true/false | `.table-borderless`   |
| IsHoverable            | bool   | true/false | `.table-hover`        |
| IsCaptionTop	          | bool   | true/false | `.caption-top`        |
| IsDark                 | bool   | true/false | `.table-dark`         |
| IsSmall                | bool   | true/false | `.table-sm`           |
| IsStriped              | bool   | true/false | `.table-striped`      |
| IsResponsive           | bool   | true/false | `.table-responsive`   |
| ResponsiveSize         | bool   | true/false | `.table-responsive-#` |
| ResponsiveWrapperClass | string | string     | Adds string to class  |

:::
#### Component \<BSTR\>
:::

| Parameter | Type | Valid      | Remarks/Output  | 
|-----------|------|------------|-----------------|
| AlignRow  | Enum | AlignRow   | `.align-[]`     | {.table-striped .p-2}  
| Color     | Enum | BSColor    | `.table-[]]`    |
| IsActive  | bool | true/false | `.table-active` |

:::
#### Component \<BSTD\>
:::

| Parameter | Type    | Valid      | Remarks/Output  | 
|-----------|---------|------------|-----------------|
| AlignRow  | Enum    | AlignRow   | `.align-[]`     | {.table-striped .p-2}  
| Color     | Enum    | BSColor    | `.table-[]]`    |
| IsActive  | bool    | true/false | `.table-active` |
| ColSpan   | string? | 0-x        | ColSpan         |

:::

#### Component \<BSTBody\> \<BSTFoot\> \<BSTHead\>
Shared Parameters only

### Example with parameter tester

{{sample=V5/Content/Tables/Tables1}}

### Responsive Tables Example

{{sample=V5/Content/Tables/Tables1b}}

#### Component \<BSDataTable<TValue>\> (Version  >= 5.0.105-Preview2)
See [shared](layout/shared) for additional parameters    
:::

| Parameter              | Type                       | Valid                                                        | Remarks/Output                                               | 
|------------------------|----------------------------|--------------------------------------------------------------|--------------------------------------------------------------|
| Items                  | IEnumerable<TValue>        | IEnumerable<TValue>                                          | Not Required using FetchData. <br/>StateHasChanged Required  | {.table-striped .p-2}  
| TotalItems             | int                        | int                                                          | Not Required using FetchData. <br/>StateHasChanged Required  |
| OnChange               | EventCallback<DataRequest> | Method                                                       | Not Required using FetchData. <br/>StateHasChanged Required  |
| FetchItems             | Func                       | DataRequest, Task<(IEnumerable<TValue>,int)>                 | return (IEnumerable<TValue> data, TotalItems for pagination) |
| Body                   | RenderFragment<TValue>     | RenderFragment                                               |                                                              |
| Header                 | RenderFragment             | RenderFragment                                               |                                                              |
| Footer                 | RenderFragment             | RenderFragment                                               |                                                              |
| NoData                 | RenderFragment             | RenderFragment                                               | Displayed when dataset is empty                              |
| PaginationTop          | bool                       | bool                                                         |                                                              |
| PaginationBottom       | bool                       | bool                                                         | Default                                                      |
| RowsPerPage            | int                        | int                                                          | Default 20                                                   |
| StartPage              | int                        | int                                                          | Default 1                                                    |
| Refresh                | Method                     |                                                              | Can be invoked with @ref                                     |
| (Obsolete)DataSet      | Func                       | int, string, bool, string, string, Task<IEnumerable<TValue>> | Version >= 5.0.105-Preview1 will be removed in release       | {.table-striped .p-2}  
| (Obsolete)TotalRecords | Func                       | int                                                          | Version >= 5.0.105-Preview1 will be removed in release       |

#### Component \<BSDataTableHead\> (Version  >= 5.0.105-Preview1)
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type   | Valid       | Remarks/Output                      | 
|--------------|--------|-------------|-------------------------------------|
| Column       | string | column name |                                     | {.table-striped .p-2} 
| Sortable     | bool   | bool        |                                     |
| ColumnFilter | bool   | bool        |                                     |
| FilterSize   | Size   | Size        | The size of the column filter input |

#### Component \<BSDataTableRow\> (Version  >= 5.0.105-Preview1)
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type   | Valid       | Remarks/Output | 
|--------------|--------|-------------|----------------|
| IsHidden     | bool   | bool        |                | {.table-striped .p-2} 

### Data Table Example using FetchData

{{sample=V5/Content/Tables/Tables2}}

### Data Table Example using OnChange event

{{sample=V5/Content/Tables/Tables3}}


## Custom Filters
### Data Table Example using FetchData and Custom Filter

{{sample=V5/Content/Tables/Tables4}}

### Data Table Example using OnChange event and Custom Filter

{{sample=V5/Content/Tables/Tables5}}