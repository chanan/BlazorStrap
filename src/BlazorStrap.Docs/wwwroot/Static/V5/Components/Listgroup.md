## List Group

#### Component \<BSListGroup\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type | Valid      | Remarks/Output              | 
|--------------|------|------------|-----------------------------|
| Size         | Enum | Size       | `.list-group-horizontal-[]` | {.table-striped}
| IsFlush      | bool | true/false | `.list-group-flush`         |
| IsHorizontal | bool | true/false | `.list-group-horizontal`    |
| IsNumbered   | bool | true/false | `.list-group-numbered`      |

:::

#### Component \<BSListGroupItem\>
:::

| Parameter      | Type          | Valid          | Remarks/Output              | 
|----------------|---------------|----------------|-----------------------------|
| Color          | Enum          | BSColor        | `.list-group-item-[]`       | {.table-striped}
| Size           | Enum          | Size           | `.list-group-horizontal-[]` |
| IsButton       | bool          | true/false     | `.list-group-item-action`   |
| PreventDefault | bool          | true/false     | Event PreventDefault        |
| OnClick        | EventCallback | MouseEventArgs |                             |
| Url            | string        | string         | href = Url                  |

:::

### Example

{{sample=V5/Components/ListGroup/ListGroup1}}

For more examples of List Groups see Bootstrap's official documentation