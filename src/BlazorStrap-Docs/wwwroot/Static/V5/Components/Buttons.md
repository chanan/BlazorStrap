## Buttons

#### Component \<BSButton\>
See [shared](layout/shared) for additional parameters   

:::

| Parameter  | Type          | Valid          | Remarks/Output               | 
|------------|---------------|----------------|------------------------------|
| Size       | Enum          | Size           | `btn-[]`                     | {.table-striped}
| IsActive   | bool          | true/false     | `.active`                    |                
| IsDisabled | bool          | true/false     | `disabled`                   |                
| IsLink     | bool          | true/false     | `.btn-link`                  |                
| IsOutlined | bool          | true/false     | `.btn-outline-[]`            |                
| IsReset    | bool          | true/false     | Reset Button                 |                
| IsSubmit   | bool          | true/false     | Submit Button                |
| Target     | string        | string         | `data-blazorstrap` of target |
| OnClick    | EventCallback | MouseEventArgs |                              |
:::

# Examples

{{sample=V5/Components/Buttons/Buttons1}}

{{sample=V5/Components/Buttons/Buttons2}}

For more examples of buttons see Bootstrap's official documentation