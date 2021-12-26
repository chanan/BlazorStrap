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
`@("value")` is not required it's a line declaration of a string to make the demo work

### Overview

{{sample=Forms/Range/Range1}}

### Disabled

{{sample=Forms/Range/Range2}}

### Min and max

{{sample=Forms/Range/Range3}}

### Steps

{{sample=Forms/Range/Range4}}