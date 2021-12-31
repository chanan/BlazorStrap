## Toast
:::{.bd-callout .bd-callout-info}
**Work inprogress** Documentation is still being written for this component
:::
#### Component \<BSToast\>
See [shared](layout/shared) for additional parameters    

:::

| Parameter    | Type           | Valid          | Remarks/Output | 
|--------------|----------------|----------------|----------------|
| Color        | Enum           | BSColor        | `.bg-[]`       | {.table-striped .p-2}
| IsActive     | bool           | true/false     | `.active`      |
| ContentClass | string         | string         |                |
| HeaderClass  | string         | string         |                |
| Content      | RenderFragment | RenderFragment | Nested Content |
| Header       | RenderFragment | RenderFragment | Nested Content |
| OnClick      | EventCallback  | MouseEventArgs |                |

:::

### Example

{{sample=Components/Toast/Toast1}}

### Without header
{{sample=Components/Toast/Toast2}}

### Toaster Example
<BSToaster/> should be placed before you `@Body` in your layout. Exact placement depends on your requirements for where you want the toasts to show up. 

{{sample=Components/Toast/Toast3}}
