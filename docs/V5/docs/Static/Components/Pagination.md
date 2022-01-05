## Pagination

#### Component \<BSPagination\>
See [shared](layout/shared) for additional parameters    

:::

| Parameter | Type | Valid   | Remarks/Output        | 
|-----------|------|---------|-----------------------|
| Align     | Enum | Align   | `.justify-content-[]` | {.table-striped .p-2}
| Color     | Enum | BSColor | `.bg-[]`              | 
| Size      | Enum | Size    | `.pagination-[]`      |

:::

#### Component \<BSPaginationItem\>

:::

| Parameter  | Type   | Valid      | Remarks/Output   | 
|------------|--------|------------|------------------|
| Color      | Enum   | BSColor    | `.bg-[]`         | {.table-striped .p-2} 
| IsActive   | bool   | true/false | `.active`        |
| IsDisabled | bool   | true/false | `.disabled`      |
| Url        | string | string     | `<a herf="Url">` |

:::

### Example

{{sample=Components/Pagination/Pagination1}}

For more examples of buttons see Bootstrap's official documentation