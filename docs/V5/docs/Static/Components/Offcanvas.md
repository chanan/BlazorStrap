## Offcanvas
#### Component \<BSOffCanvas\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter            | Type           | Valid          | Remarks/Output                  | 
|----------------------|----------------|----------------|---------------------------------|
| Color                | Enum           | BSColor        | `.bg-[]`                        | { .table-striped}
| BodyClass            | Enum           | BSColor        | `.alert-[]`                     |
| Placement            | Enum           | Placement      | `.offcanvas-[]`                 |
| DisableBackdropClick | bool           | true/false     |                                 |
| HeaderClass          | bool           | true/false     | `.d-flex` `.align-items-center` |
| ShowBackdrop         | bool           | true/false     |                                 |
| AllowScroll          | bool           | true/false     |                                 |
| ButtonClass          | int            | 1-6            | `<h1>` to `<h6>`                |
| Header               | RenderFragment | RenderFragment | Nested Content                  |
| Content              | RenderFragment | RenderFragment | Nested Content                  |
| OnClick              | EventCallback  | EventCallback  | EventCallback                   |

:::

### Example

{{sample=Components/OffCanvas/OffCanvas1}}

### Placement
{{sample=Components/OffCanvas/OffCanvas2}}

{{sample=Components/OffCanvas/OffCanvas3}}

{{sample=Components/OffCanvas/OffCanvas4}}

### Backdrop
{{sample=Components/OffCanvas/OffCanvas5}}
