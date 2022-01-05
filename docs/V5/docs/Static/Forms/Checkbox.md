## Forms Shared
#### Component \<BSInputCheckbox\> and \<BSInputRadio\>
See [shared](layout/shared) for additional parameters
**`*`** indicates required Parameters

:::

| Parameter        | Type           | Valid          | Remarks/Output            | 
|------------------|----------------|----------------|---------------------------|
| `*` CheckedValue | T              | N/A            |                           | {.table-striped .p-2}
| UnCheckedValue   | T              | N/A            | Used for toggles          |
| Color            | Enum           | BSColor        | Button Color              |
| Size             | Enum           | Size           | `.btn-[]`                 |
| IsOutlined       | bool           | true-false     | `.btn-outline-[]`         |
| IsReadonly       | bool           | true/false     | `readonly`                |
| IsToggle         | bool           | true/false     | Toggle Button             |

:::
`@("value")` is not required it's a line declaration of a string to make the demo work

### Default

{{sample=Forms/Checkbox/Checkbox1}}

### Indeterminate

{{sample=Forms/Checkbox/Checkbox2;d-none}}

### Disabled

{{sample=Forms/Checkbox/Checkbox3}}

### Radios

{{sample=Forms/Checkbox/Checkbox4}}

### Disabled radios

{{sample=Forms/Checkbox/Checkbox5}}

### Switchs

{{sample=Forms/Checkbox/Checkbox6}}

### Inline

{{sample=Forms/Checkbox/Checkbox7}}

### Toggle buttons

{{sample=Forms/Checkbox/Checkbox8}}

### Radio toggle buttons

{{sample=Forms/Checkbox/Checkbox9}}

### Outlined styles

{{sample=Forms/Checkbox/Checkbox10}}
