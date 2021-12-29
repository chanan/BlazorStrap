## Form control
#### Component \<BSInput\>
See [shared](forms/shared) for additional parameters
**`*`** indicates required Parameters
    
:::

| Parameter     | Type | Valid      | Remarks/Output                    | 
|---------------|------|------------|-----------------------------------|
| InputSize     | Enum | Size       | .form-select-# or .form-control-# | {.table-striped .p-2}
| `*` InputType | Enum | InputType  | Input Type=#                      |
| IsPlainText   | bool | true/false | .form-control-plaintext           |
| IsReadonly    | bool | true/false | readonly                          |
| NoColorClass  | bool | true/false | Removes `.form-control-color`     |

:::
`@("value")` is not required it's a line declaration of a string to make the demo work

### Example

{{sample=Forms/FormControls/FormControls1}}

### Sizing

{{sample=Forms/FormControls/FormControls2}}

### Disabled

{{sample=Forms/FormControls/FormControls3}}

### Readonly

{{sample=Forms/FormControls/FormControls4}}

### Readonly plain text

{{sample=Forms/FormControls/FormControls5}}

{{sample=Forms/FormControls/FormControls6}}

### Color

{{sample=Forms/FormControls/FormControls8}}

### Datalists

{{sample=Forms/FormControls/FormControls9}}

### Live
Note TimeOnly and DataOnly are not in .net 5.0 

{{sample=Forms/FormControls/FormControls10}}