## Tables
#### Component \<BSTable\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type | Valid      | Remarks/Output      | 
|--------------|------|------------|---------------------|
| Color        | Enum | BSColor    | `.table-[]`         | {.table-striped .p-2}  
| IsBordered   | bool | true/false | `.table-bordered`   |
| IsBorderLess | bool | true/false | `.table-borderless` |
| IsCaptionTop | bool | true/false | `.caption-top`      |
| IsStriped    | bool | true/false | `.table-striped`    |

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

{{sample=Content/Tables/Tables1}}

#### Component \<BSDataTable<TValue>\> (Version  >= 5.0.105-Preview1)
See [shared](layout/shared) for additional parameters    
:::

| Parameter        | Type                   | Valid                                                        | Remarks/Output                  | 
|------------------|------------------------|--------------------------------------------------------------|---------------------------------|
| DataSet          | Func                   | int, string, bool, string, string, Task<IEnumerable<TValue>> |                                 | {.table-striped .p-2}  
| TotalRecords     | Func                   | int                                                          |                                 |
| Body             | RenderFragment<TValue> | RenderFragment                                               |                                 |
| Header           | RenderFragment         | RenderFragment                                               |                                 |
| Footer           | RenderFragment         | RenderFragment                                               |                                 |
| NoData           | RenderFragment         | RenderFragment                                               | Displayed when dataset is empty |
| PaginationTop    | bool                   | bool                                                         |                                 |
| PaginationBottom | bool                   | bool                                                         | Default                         |
| RowsPerPage      | int                    | int                                                          | Default 20                      |
| StartPage        | int                    | int                                                          | Default 1                       |
| Refresh          | Method                 |                                                              | Can be invoked with @ref        |

#### Component \<BSDataTableHead\> (Version  >= 5.0.105-Preview1)
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type   | Valid       | Remarks/Output | 
|--------------|--------|-------------|----------------|
| Column       | string | column name |                | {.table-striped .p-2} 
| Sortable     | bool   | bool        |                |
| ColumnFilter | bool   | bool        |                |

### Data Table Example

{{sample=Content/Tables/Tables2}}