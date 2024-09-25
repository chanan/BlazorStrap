## Tooltips

#### Component \<BSTooltip\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter             | Type           | Valid          | Remarks/Output														| 
|-----------------------|----------------|----------------|---------------------------------------------------------------------|
| Placement             | Enum           | Placement      | Placement															| {.table-striped}
| Target                | string		 | string         | DataIdOfTarget					   									|   
| PopperOptions         | object         | dynamic object | Sets additional popper.js options.									|
:::

### Examples

{{sample=V5/Components/Tooltips/Tooltips1}}

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