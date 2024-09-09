## Popover

#### Component \<BSPopover\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter             | Type           | Valid          | Remarks/Output														| 
|-----------------------|----------------|----------------|---------------------------------------------------------------------|
| HeaderColor           | Enum           | `BSColor`      | `.bg-[]`															| {.table-striped}
| Placement             | Enum           | Placement      | Placement															|
| MouseOver             | bool           | true/false     |																		|
| Header                | RenderFragment | RenderFragment | Nested Content														|
| Target                | string		   | string       | DataIdOfTarget														|   
| Content               | RenderFragment | RenderFragment | Nested Content														|   
| ContentAlwaysRendered | bool           | bool           | default=false. Hides content for component when not show if false	|
| PopperOptions         | object         | dynamic object | Sets additional popper.js options.									|
| NoClickEvent          | bool           | true/false     | default=false. Disables click event on target                       |
:::

### Example

{{sample=V5/Components/Popover/Popover1}}

### Dynamic Example
:::{.bd-callout .bd-callout-info}
Note `MouseOver` parameter will not work here
:::

{{sample=V5/Components/Popover/Popover2}}

### HtmlAlwaysRendered false
By default content html for componets is always rendered to limit the rendering required when showing. If you want to hide the html when not shown set HtmlAlwaysRendered to false. This will cause the content html of component to be rendered when shown and removed when hidden. This may cause a slight delay when showing. 

{{sample=V5/Components/Popover/Popover3}}

### Confirmation Popover

:::{.bd-callout .bd-callout-warning}
** Note: Added in 5.2.102-Preview3
:::

{{sample=V5/Components/Popover/Popover4}}

### Methods / Events
TValue = BSPopover
:::

| Name												   | Type   | Return Value | Remarks                                         |
|------------------------------------------------------|--------|--------------|-------------------------------------------------|
| ToggleAsync										   | Method |              | Toggles                                         |
| ShowAsync											   | Method |              | Shows                                           |
| ToggleAsync(target,content,placement,header= null)   | Method |              | Dynamical Toggles Popover `>= 5.0.105-Preview4` |
| ShowAsync(target,content,placement,header= null)     | Method |              | Dynamical Shows Popover   `>= 5.0.105-Preview4` |
| HideAsync											   | Method |              | Hides                                           |
| OnShow											   | Event  | TValue       | Raised when starting to show                    |
| OnShown											   | Event  | TValue       | Raised when shown                               |
| OnHide											   | Event  | TValue       | Raised when starting to hide                    |
| OnHidden											   | Event  | TValue       | Raised when hidden                              |
:::