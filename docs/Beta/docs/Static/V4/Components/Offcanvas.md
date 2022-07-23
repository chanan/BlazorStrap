## Offcanvas
#### Component \<BSOffCanvas\>
:::{.bd-callout .bd-callout-warning}
**Note: OffCanvas** is a backported component from Bootstrap 5 you must include. 
\<link href="_content/BlazorStrap.V4/offcanvas.css" rel="stylesheet" \/> 
or your custom version of it.
:::

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
| ButtonClass          | int            | 1-6            |                                 |
| Header               | RenderFragment | RenderFragment | Nested Content                  |
| Content              | RenderFragment | RenderFragment | Nested Content                  |
| OnClick              | EventCallback  | EventCallback  | EventCallback                   |

:::

:::{.bd-callout .bd-callout-info}
**Note: Sometimes you need addition child content inside this component. Such as `\<BSForm\>`. We now provide `\<BSOffCanvasHeader\>`, `\<BSOffCanvasContent\>` to be used in place of the render fragments.
:::

### Example

{{sample=V4/Components/OffCanvas/OffCanvas1}}

### Placement
{{sample=V4/Components/OffCanvas/OffCanvas2}}

{{sample=V4/Components/OffCanvas/OffCanvas3}}

{{sample=V4/Components/OffCanvas/OffCanvas4}}

### Backdrop
{{sample=V4/Components/OffCanvas/OffCanvas5}}

### Offcanvas requiring more complex content

{{sample=V4/Components/OffCanvas/OffCanvas6}}

### Methods / Events
TValue = BSOffCanvas
:::

| Name        | Type   | Return Value | Remarks                      |
|-------------|--------|--------------|------------------------------|
| ToggleAsync | Method |              | Toggles                      |
| ShowAsync   | Method |              | Shows                        |
| HideAsync   | Method |              | Hides                        |
| OnShow      | Event  | TValue       | Raised when starting to show |
| OnShown     | Event  | TValue       | Raised when shown            |
| OnHide      | Event  | TValue       | Raised when starting to hide |
| OnHidden    | Event  | TValue       | Raised when hidden           |
:::
