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
`Value="@("X")"` is so the demo has a type and compile with adding code blocks. You will want to use `@bind-Value="YourRealVar""`

### Example

{{sample=V5/Forms/FormControls/FormControls1}}

### Sizing

{{sample=V5/Forms/FormControls/FormControls2}}

### Disabled

{{sample=V5/Forms/FormControls/FormControls3}}

### Readonly

{{sample=V5/Forms/FormControls/FormControls4}}

### Readonly plain text

{{sample=V5/Forms/FormControls/FormControls5}}

{{sample=V5/Forms/FormControls/FormControls6}}

### Color

{{sample=V5/Forms/FormControls/FormControls8}}

### Datalists

{{sample=V5/Forms/FormControls/FormControls9}}

### Live
Note TimeOnly and DataOnly are not in .net 5.0 

{{sample=V5/Forms/FormControls/FormControls10}}