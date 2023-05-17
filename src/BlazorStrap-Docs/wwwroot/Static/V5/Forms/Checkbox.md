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
| ContainerClass   | string         | string         | custom class to add to `form-check form-switch` |

:::
`@("value")` is not required it's a line declaration of a string to make the demo work

### Default

{{sample=V5/Forms/Checkbox/Checkbox1}}

### Disabled

{{sample=V5/Forms/Checkbox/Checkbox3}}

### Radios

{{sample=V5/Forms/Checkbox/Checkbox4}}

### Disabled radios

{{sample=V5/Forms/Checkbox/Checkbox5}}

### Switchs

{{sample=V5/Forms/Checkbox/Checkbox6}}

### Inline

{{sample=V5/Forms/Checkbox/Checkbox7}}

### Toggle buttons

{{sample=V5/Forms/Checkbox/Checkbox8}}

### Radio toggle buttons

{{sample=V5/Forms/Checkbox/Checkbox9}}

### Outlined styles

{{sample=V5/Forms/Checkbox/Checkbox10}}
