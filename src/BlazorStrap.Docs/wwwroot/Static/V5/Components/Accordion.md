## Accordion
#### Component \<BSAccordion\>
:::

| Parameter | Type           | Valid          | Remarks/Output      | 
|-----------|----------------|----------------|---------------------|
| Flush     | bool           | true/false     | Removes Side Border | {.table-striped}

:::
See [shared](layout/shared) for additional parameters

#### Component \<BSAccordionItem\>
:::

| Parameter    | Type           | Valid          | Remarks/Output                  | 
|--------------|----------------|----------------|---------------------------------|
| AlwaysOpen   | bool           | true/false     | `.d-flex` `.align-items-center` | {.table-striped}  
| DefaultShown | bool           | true/false     | `.alert-dismissible`            |
| Header       | RenderFragment | RenderFragment | Nested Content                  |               
| Content      | RenderFragment | RenderFragment | Nested Content                  |
| HeaderClass  | string         | CSS Class      | Additional CSS Classes          |
| ContextClass | string         | CSS Class      | Additional CSS Classes          |

:::

### Example

{{sample=V5/Components/Accordion/Accordion1}}

### Flush

{{sample=V5/Components/Accordion/Accordion2}}

### Always open & default shown

{{sample=V5/Components/Accordion/Accordion3}}

### Default shown false first item

{{sample=V5/Components/Accordion/Accordion4}}

### Methods / Events
TValue = BSAccordionItem
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