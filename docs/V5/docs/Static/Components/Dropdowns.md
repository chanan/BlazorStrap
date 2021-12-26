## Dropdowns
#### Component \<Dropdowns\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter         | Type           | Valid          | Remarks/Output               | 
|-------------------|----------------|----------------|------------------------------|
| AllowItemClick    | bool           | true/false     | Allows Inside Clicks         | {.table-striped}
| AllowOutsideClick | bool           | true/false     | Allows Outside Clicks        |
| Demo              | bool           | RenderFragment | Nested Content               |
| IsDark            | bool           | RenderFragment | Nested Content               |
| IsManual          | bool           | true/false     | Manual Close                 |
| IsStatic          | bool           | true/false     | Disables dynamic positioning |
| Offset            | string         | #,#            | Dropdown offset              |
| ShownAttribute    | string         | string         | Adds attribute when shown    |
| Target            | string         | string         | `data-blazorstrap` of target |
| Toggler           | RenderFragment | RenderFragment | Nested Content               |
| Content           | RenderFragment | RenderFragment | Nested Content               |

:::

### Single button

{{sample=Components/Dropdowns/Dropdowns1}}

{{sample=Components/Dropdowns/Dropdowns2}}

{{sample=Components/Dropdowns/Dropdowns3}}

### Split button

{{sample=Components/Dropdowns/Dropdowns4}}

### Sizing
Button dropdowns work with buttons of all sizes, including default and split dropdown buttons.

{{sample=Components/Dropdowns/Dropdowns5}}

{{sample=Components/Dropdowns/Dropdowns6}}

### Dark

{{sample=Components/Dropdowns/Dropdowns7}}

And putting it to use in a navbar:
{{sample=Components/Dropdowns/Dropdowns8}}

### Directions
{.mt-4}
### Dropup

{{sample=Components/Dropdowns/Dropdowns9}}

### Dropright

{{sample=Components/Dropdowns/Dropdowns10}}

### Dropleft

{{sample=Components/Dropdowns/Dropdowns11}}

### Menu items
Default type of menu items are `<a>` You can use `IsButton` to return a `<button>`

{{sample=Components/Dropdowns/Dropdowns12}}

{{sample=Components/Dropdowns/Dropdowns13}}

### Active
Add `IsActive` to items in the dropdown to style them as active.

{{sample=Components/Dropdowns/Dropdowns14}}

### Disabled
Add `IsDisabled` to items in the dropdown to style them as disabled.

{{sample=Components/Dropdowns/Dropdowns15}}

### Menu alignment
Dropdowns in `<BSButtonGroup>` are treated as popovers and can be aligned using DropdownPlacement

{{sample=Components/Dropdowns/Dropdowns16}}

### Responsive alignment
If you want to use responsive alignment, disable dynamic positioning by adding the `IsStatic` parameter and use the responsive variation classes.
To align right the dropdown menu with the given breakpoint or larger, add .dropdown-menu{-sm|-md|-lg|-xl|-xxl}-end.

{{sample=Components/Dropdowns/Dropdowns17}}

To align left the dropdown menu with the given breakpoint or larger, add `.dropdown-menu-end` and `.dropdown-menu{-sm|-md|-lg|-xl|-xxl}-start`.

{{sample=Components/Dropdowns/Dropdowns18}}

### Alignment options

{{sample=Components/Dropdowns/Dropdowns19}}

### Headers

{{sample=Components/Dropdowns/Dropdowns20}}

### Dividers

{{sample=Components/Dropdowns/Dropdowns21}}

### Text
Place any freeform text within a dropdown menu with text and use [spacing utilities](https://getbootstrap.com/docs/5.1/utilities/spacing). Note that you’ll likely need additional sizing styles to constrain the menu width.

{{sample=Components/Dropdowns/Dropdowns22}}

### Forms

{{sample=Components/Dropdowns/Dropdowns23}}

{{sample=Components/Dropdowns/Dropdowns24}}

### Dropdown options

{{sample=Components/Dropdowns/Dropdowns25}}