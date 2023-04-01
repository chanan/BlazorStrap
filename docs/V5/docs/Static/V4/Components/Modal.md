## Modal
#### Component \<BSModal\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter         | Type                      | Valid          | Remarks/Output                                                                     | 
|-------------------|---------------------------|----------------|------------------------------------------------------------------------------------|
| FullScreenSize    | Enum                      | Side           | `.modal-fullscreen-[]-down`                                                        | {.table-striped}
| AllowScroll       | bool                      | true/false     | Allows Body Scrolling                                                              | 
| ModalColor        | BSColor                   | BSColor        |                                                                                    |
| Size              | Enum                      | Size           | `.modal-[]`                                                                        |
| IsCentered        | bool                      | true/false     | `.modal-dialog-centered`                                                           |
| IsFullScreen      | bool                      | true/false     | `.modal-fullscreen`                                                                |
| IsScrollable      | bool                      | true/false     | `.modal-dialog-scrollable`                                                         |
| HasCloseButton    | bool                      | true/false     | Includes `.btn-close`                                                              |
| ShowBackdrop      | bool                      | true/false     |                                                                                    |
| ButtonClass       | string                    | string         | custom class for the close button                                                  |
| ContentClass      | string                    | string         | custom class for `modal-body`                                                      |
| DialogClass       | string                    | string         | custom class for `modal-dialog`                                                    |
| HeaderClass       | string                    | string         | custom class for `modal-header`                                                    |
| IsStaticBackdrop  | bool                      | true/false     | Ignores backdrop clicks                                                            |
| Header            | RenderFragment            | RenderFragment | Nested Content                                                                     |
| Content           | RenderFragment            | RenderFragment | Nested Content                                                                     |
| Footer            | RenderFragment\<BSModal\> | RenderFragment | Nested Content BSModal is assigned by a self reference you do not need to pass it. |
| HideOnSubmit      | bool                      | true/false     | Hides modal on BSForm submit.                                                      |
| HideOnValidSubmit | bool                      | true/false     | Hides modal on Valid BSForm submit.                                                |

:::

:::{.bd-callout .bd-callout-info}
**Note: Sometimes you need addition child content inside this component. Such as `\<BSForm\>`. We now provide `\<BSModalHeader\>`, `\<BSModalFooter\>`, `\<BSModalContent\>` to be used in place of the render fragments.
:::

### Live Example

{{sample=V4/Components/Modal/Modal1}}

### Static backdrop

{{sample=V4/Components/Modal/Modal2}}

### Scrolling long content
When modals become too long for the user’s viewport or device, they scroll independent of the page itself. Try the demo below to see what we mean.

{{sample=V4/Components/Modal/Modal3}}

You can also scroll content in the modals body

{{sample=V4/Components/Modal/Modal4}}

### Vertically centered

{{sample=V4/Components/Modal/Modal5}}

### Tooltips and popovers

{{sample=V4/Components/Modal/Modal6}}

### Toggle between modals

{{sample=V4/Components/Modal/Modal7}}

### Optional sizes

{{sample=V4/Components/Modal/Modal8}}

### Model requiring more complex content

{{sample=V4/Components/Modal/Modal9}}

### Methods / Events
TValue = BSModal
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
