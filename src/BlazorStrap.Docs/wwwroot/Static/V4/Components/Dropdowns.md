## Dropdowns
#### Component \<Dropdowns\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter         | Type           | Valid          | Remarks/Output                               | 
|-------------------|----------------|----------------|----------------------------------------------|
| AllowItemClick    | bool           | true/false     | Allows Inside Clicks                         | {.table-striped}
| AllowOutsideClick | bool           | true/false     | Allows Outside Clicks                        |
| Demo              | bool           | true/false     | Shows Content only                           |
| IsDark            | bool           | true/false     | `dropdown-menu-dark`                         |
| IsManual          | bool           | true/false     | Manual Close                                 |
| IsDiv             | bool           | true/false     | Makes the dropdown menu a div and use popper |
| IsStatic          | bool           | true/false     | Disables dynamic positioning                 |
| IsMouseOver       | bool           | true/false     | Opens and closes when mouse is over          |
| Offset            | string         | #,#            | Dropdown offset                              |
| ShownAttribute    | string         | string         | Adds attribute when shown                    |
| Target            | string         | string         | `data-blazorstrap` of target                 |
| Toggler           | RenderFragment | RenderFragment | Nested Content                               |
| Content           | RenderFragment | RenderFragment | Nested Content                               |
| PopperOptions     | object         | dynamic object | Sets additional popper.js options.			 |
| IsDivClass		| string         | string         | Adds extra classes to outer div when using   |

:::

:::

| Parameter      | Type          | Valid          | Remarks/Output                               | 
|----------------|---------------|----------------|----------------------------------------------|
| IsActive       | bool?         | true/false     | `.active` if not set we will add it for you. | {.table-striped}
| IsDivider      | bool          | true/false     | `<hr class="dropdown-divider"/>`             |
| Header         | int           | 1-6            | `<h1>` to `<h6>`                             |
| IsDisabled     | bool          | true/false     | `.disabled`                                  |
| IsButton       | bool          | true/false     | `<button>`                                   |
| IsSubmenu      | bool          | true/false     | Outputs correctly for a nested DropDown      |
| IsText         | bool          | true/false     | `dropdown-item-text`                         |
| SubmenuClass   | string        | string         | Default `.dropdown-submenu`                  |
| OnClick        | EventCallback | MouseEventArgs |                                              |
| PreventDefault | string        | string         | Prevents Default for onclick                 |
| Url            | string        | string         | `<a href="Url">`                             |

:::

Dropdown Items

### Single button

{{sample=V4/Components/Dropdowns/Dropdowns1}}

{{sample=V4/Components/Dropdowns/Dropdowns1a}}

{{sample=V4/Components/Dropdowns/Dropdowns2}}

{{sample=V4/Components/Dropdowns/Dropdowns3}}

### Split button

{{sample=V4/Components/Dropdowns/Dropdowns4}}

### Sizing
Button dropdowns work with buttons of all sizes, including default and split dropdown buttons.

{{sample=V4/Components/Dropdowns/Dropdowns5}}

{{sample=V4/Components/Dropdowns/Dropdowns6}}

### Dark

{{sample=V4/Components/Dropdowns/Dropdowns7}}

And putting it to use in a navbar:
{{sample=V4/Components/Dropdowns/Dropdowns8}}

### Directions
{.mt-4}
### Dropup

{{sample=V4/Components/Dropdowns/Dropdowns9}}

### Dropright

{{sample=V4/Components/Dropdowns/Dropdowns10}}

### Dropleft

{{sample=V4/Components/Dropdowns/Dropdowns11}}

### Menu items
Default type of menu items are `<a>` You can use `IsButton` to return a `<button>`

{{sample=V4/Components/Dropdowns/Dropdowns12}}

{{sample=V4/Components/Dropdowns/Dropdowns13}}

### Active
Add `IsActive` to items in the dropdown to style them as active.

{{sample=V4/Components/Dropdowns/Dropdowns14}}

### Disabled
Add `IsDisabled` to items in the dropdown to style them as disabled.

{{sample=V4/Components/Dropdowns/Dropdowns15}}

### Menu alignment
Dropdowns in `<BSButtonGroup>` are treated as popovers and can be aligned using DropdownPlacement

{{sample=V4/Components/Dropdowns/Dropdowns16}}

### Responsive alignment
If you want to use responsive alignment, disable dynamic positioning by adding the `IsStatic` parameter and use the responsive variation classes.
To align right the dropdown menu with the given breakpoint or larger, add .dropdown-menu{-sm|-md|-lg|-xl|-xxl}-end.

{{sample=V4/Components/Dropdowns/Dropdowns17}}

To align left the dropdown menu with the given breakpoint or larger, add `.dropdown-menu-end` and `.dropdown-menu{-sm|-md|-lg|-xl|-xxl}-start`.

{{sample=V4/Components/Dropdowns/Dropdowns18}}

### Alignment options

{{sample=V4/Components/Dropdowns/Dropdowns19}}

### Headers

{{sample=V4/Components/Dropdowns/Dropdowns20}}

### Dividers

{{sample=V4/Components/Dropdowns/Dropdowns21}}

### Text
Place any freeform text within a dropdown menu with text and use [spacing utilities](https://getbootstrap.com/docs/5.1/utilities/spacing). Note that you’ll likely need additional sizing styles to constrain the menu width.

{{sample=V4/Components/Dropdowns/Dropdowns22}}

### Forms

{{sample=V4/Components/Dropdowns/Dropdowns23}}

{{sample=V4/Components/Dropdowns/Dropdowns24}}

### Dropdown options

{{sample=V4/Components/Dropdowns/Dropdowns25}}