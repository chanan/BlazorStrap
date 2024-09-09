## Range
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

### Overview

{{sample=V4/Forms/Range/Range1}}

### Disabled

{{sample=V4/Forms/Range/Range2}}

### Min and max

{{sample=V4/Forms/Range/Range3}}

### Steps

{{sample=V4/Forms/Range/Range4}}