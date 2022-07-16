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

{{sample=V5_1/Forms/Checkbox/Checkbox1}}

### Indeterminate

{{sample=V5_1/Forms/Checkbox/Checkbox2;d-none}}

### Disabled

{{sample=V5_1/Forms/Checkbox/Checkbox3}}

### Radios

{{sample=V5_1/Forms/Checkbox/Checkbox4}}

### Disabled radios

{{sample=V5_1/Forms/Checkbox/Checkbox5}}

### Switchs

{{sample=V5_1/Forms/Checkbox/Checkbox6}}

### Inline

{{sample=V5_1/Forms/Checkbox/Checkbox7}}

### Toggle buttons

{{sample=V5_1/Forms/Checkbox/Checkbox8}}

### Radio toggle buttons

{{sample=V5_1/Forms/Checkbox/Checkbox9}}

### Outlined styles

{{sample=V5_1/Forms/Checkbox/Checkbox10}}
