## Action Links

#### Component \<BSLink\>
See [shared](layout/shared) for additional parameters   

:::

| Parameter  | Type          | Valid          | Remarks/Output                                | 
|------------|---------------|----------------|-----------------------------------------------|
| Size       | Enum          | Size           | `btn-[]`                                      | {.table-striped}
| IsActive   | bool?         | true/false     | `.active`  if not set we will add it for you. |                
| IsDisabled | bool          | true/false     | `disabled`                                    |                
| IsButton   | bool          | true/false     | `.btn-link`                                   |                
| IsOutlined | bool          | true/false     | `.btn-outline-[]`                             |                
| IsReset    | bool          | true/false     | Reset Button                                  |                
| IsSubmit   | bool          | true/false     | Submit Button                                 |
| Target     | string        | string         | `data-blazorstrap` of target                  |
| Url        | string        | string         | href = Url                                    |
| OnClick    | EventCallback | MouseEventArgs |                                               |

:::

# Examples

{{sample=V5/Components/Link/Link1}}

{{sample=V5/Components/Link/Link2}}
