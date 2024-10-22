## DataGrid
#### Component \<BSDataGrid\>
See [Tables](V4/content/tables) for additional parameters

:::{.bd-callout .bd-callout-warning}
**Warning:** This component is still in development and may change.
:::

#### Component \<BSDataGrid\>
:::

| Parameter            | Type                         | Valid                        | Remarks/Output                                     | 
|----------------------|------------------------------|------------------------------|----------------------------------------------------|
| Items                | IEnumerable<T>               | IEnumerable<T>               | Dot not use with ItemsProvider                     | {.table-striped}
| ItemsProvider        | GridItemsProvider<TGridItem> | GridItemsProvider<TGridItem> | Dot not use with Items                             |
| Pagination           | PaginationState              | PaginationState              | Pagination settings                                |
| IsVirtualized        | bool                         | true/false                   | Enable virtualization                              |
| Columns              | RenderFragment?              | RenderFragment               | Outlet for the columns                             |
| IsMultiSort          | bool                         | true/false                   | Allows Control + Click on items to sort            |
| MultiSortClass       | string                       | string                       | Class for the multi sort icon                      |
| RowClassFunc         | Func<TGridItem, string>      | Func<TGridItem, string>      | Allows passing a class pre item row                |
| RowStyleFunc         | Func<TGridItem, string>      | Func<TGridItem, string>      | Allows passing styles pre item row                 |
| RowClass             | string                       | string                       | Allows passing a class to all rows                 |
| RowStyle             | string                       | string                       | Allows passing styles to all rows                  |
| FilterClass          | string                       | string                       | Class for the filter icon                          |
| MenuClass            | string                       | string                       | Class for the menu icon                            |
| DataGridClass        | string                       | string                       | Class for the DataGrid                             |
| VirtualItemHeight    | int                          | int                          | Height of the virtualized grid row                 |
| VirtualOverscanCount | int                          | int                          | Number of rows to render outside the viewable area |

:::

#### Component Base \<ColumnBase\>
:::


| Parameter             | Type                                           | Valid                                          | Remarks/Output                           | 
|-----------------------|------------------------------------------------|------------------------------------------------|------------------------------------------|
| VirtualPlaceholder    | RenderFragment<PlaceholderContext>             | RenderFragment<PlaceholderContext>             | Placeholder for the virtualized grid row | {.table-striped}
| IsSortable            | bool                                           | true/false                                     | Enables sorting on the column            |
| IsFilterable          | bool                                           | true/false                                     | Enables filtering on the column          |
| Title                 | string                                         | string                                         | Title of the column                      |
| CustomSort            | Func<SortData<TGridItem>, SortData<TGridItem>> | Func<SortData<TGridItem>, SortData<TGridItem>> | Custom sort function                     |
| InitialSorted         | bool                                           | true/false                                     | Initial sorted column. Only one is valid |
| InitialSortDescending | bool                                           | true/false                                     | Initial sort direction                   |
| Class                 | string                                         | string                                         | Class for the column                     |
| Style                 | string                                         | string                                         | Style for the column                     |
| ClassFunc             | Func<TGridItem, string>                        | Func<TGridItem, string>                        | Class function for the column            |
| StyleFunc             | Func<TGridItem, string>                        | Func<TGridItem, string>                        | Style function for the column            |
| MaxTextWith           | int                                            | int                                            | Max width of the text then ...           |


:::

#### Component \<PropertyColumn\>
:::


| Parameter     | Type                                  | Valid                                 | Remarks/Output                 | 
|---------------|---------------------------------------|---------------------------------------|--------------------------------|
| Property      | Expression<Func<TGridItem, TProp>>    | Expression<Func<TGridItem, TProp>>    | Property to bind to the column | {.table-striped}
| ColumnOptions | RenderFragment<IColumnHeaderAccessor> | RenderFragment<IColumnHeaderAccessor> | Column options dropdown        |

:::

#### Component \<TemplateColumn\>
:::


| Parameter     | Type                                  | Valid                                 | Remarks/Output                 | 
|---------------|---------------------------------------|---------------------------------------|--------------------------------|
| Header        | RenderFragment<IColumnHeaderAccessor> | RenderFragment<IColumnHeaderAccessor> | Header template                | {.table-striped}
| Content       | RenderFragment<TGridItem>             | RenderFragment<TGridItem>             | Content template               |
| Footer        | RenderFragment                        | RenderFragment                        | Footer template                |
| Property      | Expression<Func<TGridItem, object>>   | Expression<Func<TGridItem, object>>   | Property to bind to the column | 
| ColumnOptions | RenderFragment<IColumnHeaderAccessor> | RenderFragment<IColumnHeaderAccessor> | Column options dropdown        |

:::

:::{.bd-callout .bd-callout-info}
**Note:** Left and Right placements are not supported. They will default back to bottom placement.
:::

### Pagination & Virtualization (In-memory)

{{sample=V4/Components/DataGrid/DataGrid1}}

### Pagination & Virtualization (EF Core In-memory)

{{sample=V4/Components/DataGrid/DataGrid2}}

### Remote Data

{{sample=V4/Components/DataGrid/DataGrid3}}

### Remote Virtualization

{{sample=V4/Components/DataGrid/DataGrid4}}

### Methods / Events
:::

| Name              | Type   | Return Value | Remarks                      |
|-------------------|--------|--------------|------------------------------|
| RefreshItemsAsync | Method | Task         | Calls for a refresh on items |
:::