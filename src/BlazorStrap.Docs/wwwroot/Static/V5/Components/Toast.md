## Toast

#### Component \<BSToast\>
See [shared](layout/shared) for additional parameters    

:::

| Parameter    | Type           | Valid          | Remarks/Output                         | 
|--------------|----------------|----------------|----------------------------------------|
| Color        | Enum           | BSColor        | `.bg-[]`                               | {.table-striped .p-2}
| IsActive     | bool           | true/false     | `.active`                              |
| ContentClass | string         | string         |                                        |
| HeaderClass  | string         | string         |                                        |
| HasIcon      | bool           | true/false     | Adds Icon Based on color               |
| Content      | RenderFragment | RenderFragment | Nested Content                         |
| Header       | RenderFragment | RenderFragment | Nested Content                         |
| OnClick      | EventCallback  | MouseEventArgs |                                        |

:::

### Example

{{sample=V5/Components/Toast/Toast1}}

### Without header
{{sample=V5/Components/Toast/Toast2}}


### Toaster Example
#### Component \<BSToaster\>
| Parameter    | Type    | Valid            | Remarks/Output                                  | 
|--------------|---------|------------------|-------------------------------------------------|
| Position     | Enum    | CssPosition      | sets position fixed, static, absolute, relative | {.table-striped .p-2}
| WrapperClass | string  | css class string | Adds your class(es) to wrapper                  | 
| WrapperStyle | string? | style string     | Adds your styles to wrapper                     |
| ZIndex       | int     | int              | sets z-index style                              |

<BSToaster/> should be placed before you `@Body` in your layout. Exact placement depends on your requirements for where you want the toasts to show up. 

{{sample=V5/Components/Toast/Toast3}}
