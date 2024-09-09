## Collapse
#### Component \<BSCollapse\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type           | Valid          | Remarks/Output     | 
|--------------|----------------|----------------|--------------------|
| DefaultShown | bool           | true/false     |                    | {.table-striped}
| IsInNavbar   | bool           | true/false     | `.navbar-collapse` |
| IsList       | bool           | true/false     | `<ul>`             |
| ISHorizontal | bool           | true/false     | `.collapse-horizontal` |
| Content      | RenderFragment | RenderFragment | Nested Content     |
| Toggler      | RenderFragment | RenderFragment | Nested Content     |

:::

#### Component \<BSCarouselCaption\>
No Setting parameters

### Example

{{sample=V4/Components/Collapse/Collapse1}}

### Example with Toggler

{{sample=V4/Components/Collapse/Collapse2}}

### Multiple targets

{{sample=V4/Components/Collapse/Collapse3}}

### Horizontal 

{{sample=V4/Components/Collapse/Collapse4}}

### Methods / Events
TValue = BSCollapse
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