## Select
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

{{sample=V5/Forms/Select/Select1}}

### Sizing

{{sample=V5/Forms/Select/Select2}}

The 'multiple' attribute is also supported.

{{sample=V5/Forms/Select/Select3}}

As is the 'size' attribute.

{{sample=V5/Forms/Select/Select4}}

### Disabled
Since 'readonly' is not valid for select we automatically assign it's true value to 'disabled'

{{sample=V5/Forms/Select/Select5}}
