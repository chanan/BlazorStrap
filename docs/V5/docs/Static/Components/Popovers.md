## Popover

#### Component \<BSPopover\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter   | Type           | Valid          | Remarks/Output | 
|-------------|----------------|----------------|----------------|
| HeaderColor | Enum           | `BSColor`      | `.bg-[]`       | {.table-striped}
| Placement   | Enum           | Placement      | Placement      |
| MouseOver   | bool           | true/false     |                |
| Header      | RenderFragment | RenderFragment | Nested Content |
| Target      | RenderFragment | RenderFragment | Nested Content |
| Content     | RenderFragment | RenderFragment | Nested Content |   

:::

### Example

{{sample=Components/Popover/Popover1}}

### Methods / Events
TValue = BSPopover
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