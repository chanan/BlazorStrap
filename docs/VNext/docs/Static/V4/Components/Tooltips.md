## Tooltips

#### Component \<BSTooltip\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter             | Type           | Valid          | Remarks/Output												     | 
|-----------------------|----------------|----------------|------------------------------------------------------------------|
| Placement             | Enum           | Placement      | Placement													     | {.table-striped}
| Target                | string		 | string         | DataIdOfTarget												     |   
| ContentAlwaysRendered | bool           | bool           | default=false. Hides content for component when not show if false |
:::

### Examples

{{sample=V4/Components/Tooltips/Tooltips1}}

### HtmlAlwaysRendered false
By default content html for componets is always rendered to limit the rendering required when showing. If you want to hide the html when not shown set HtmlAlwaysRendered to false. This will cause the content html of component to be rendered when shown and removed when hidden. This may cause a slight delay when showing.

{{sample=V4/Components/Tooltips/Tooltips2}}

### Methods / Events
TValue = BSTooltip
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