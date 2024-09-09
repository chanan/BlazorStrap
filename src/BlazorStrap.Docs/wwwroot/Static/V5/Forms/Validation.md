## Validation
#### Component \<BSInput\>
**`*`** indicates required Parameters
See [shared](forms/shared) for additional parameters

:::

| Parameter       | Type          | Valid       | Remarks/Output                          |
|-----------------|---------------|-------------|-----------------------------------------|
| InputSize       | Enum          | Size        | `.form-select-[]` or `.form-control-[]` | {.table-striped .p-2}
| EditContext     | EditContext   | EditContext |                                         | 
| IsBasic         | T             | N/A         |                                         |
| IsPlainText     | bool          | true/false  | `.form-control-plaintext`               | 
| IsFloating      | bool          | true/false  | `.form-floating`                        | 
| IsRow           | bool          | true/false  | `.row`                                  | 
| Model           | T             | N/A         |                                         |
| OnInvalidSubmit | EventCallback | EditContext |                                         |
| OnSubmit        | EventCallback | EditContext |                                         |
| OnValidSubmit   | EventCallback | EditContext |                                         |

:::

### Example

{{sample=V5/Forms/Validation/ValidationMain}}
