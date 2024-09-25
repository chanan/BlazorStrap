## Offcanvas
#### Component \<BSOffCanvas\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter             | Type						| Valid          | Remarks/Output													| 
|-----------------------|---------------------------|----------------|------------------------------------------------------------------|
| Color                 | Enum						| BSColor        | `.bg-[]`															| { .table-striped}
| DisableEscapeKey      | bool                      | true/false     | Disables the escape key from closing the offcanvas               |
| BodyClass             | Enum						| BSColor        | `.alert-[]`														|
| Placement             | Enum						| Placement      | `.offcanvas-[]`													|
| DisableBackdropClick  | bool						| true/false     |																	|
| HeaderClass           | bool						| true/false     | `.d-flex` `.align-items-center`									|
| ShowBackdrop          | bool						| true/false     |																	|
| AllowScroll           | bool						| true/false     |																	|
| ButtonClass           | int						| 1-6            |																	|
| Header                | RenderFragment			| RenderFragment | Nested Content													|
| Content               | RenderFragment			| RenderFragment | Nested Content													|
| OnClick               | EventCallback				| EventCallback  | EventCallback													|
| ContentAlwaysRendered | bool                      | bool           | default=false. Hides content for component when not show if false |

:::

:::{.bd-callout .bd-callout-info}
**Note: Sometimes you need addition child content inside this component. Such as `\<BSForm\>`. We now provide `\<BSOffCanvasHeader\>`, `\<BSOffCanvasContent\>` to be used in place of the render fragments.
:::

### Example

{{sample=V5/Components/OffCanvas/OffCanvas1}}

### Placement
{{sample=V5/Components/OffCanvas/OffCanvas2}}

{{sample=V5/Components/OffCanvas/OffCanvas3}}

{{sample=V5/Components/OffCanvas/OffCanvas4}}

### Backdrop
{{sample=V5/Components/OffCanvas/OffCanvas5}}

### Offcanvas requiring more complex content

{{sample=V5/Components/OffCanvas/OffCanvas6}}

### HtmlAlwaysRendered true
By default content html for componets is not rendered when the element isn't shown. If you want to show the html when not shown set HtmlAlwaysRendered to true. This will cause the content html of component to be rendered when shown and hidden.

{{sample=V5/Components/OffCanvas/OffCanvas7}}
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
