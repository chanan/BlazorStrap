## Alerts
#### Component \<BSAlerts\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter     | Type           | Valid          | Remarks/Output                   | 
|---------------|----------------|----------------|----------------------------------|
| Color         | Enum           | `BSColor`      | `.alert-[]`                      | {.table-striped}  
| HasIcon       | bool           | true/false     | `.d-flex`  `.align-items-center` |
| IsDismissible | bool           | true/false     | `.alert-dismissible`             |
| Heading       | int            | 1-6            | `<h1>` to `<h6>`                 |
| Header        | RenderFragment | RenderFragment | Nested Content                   |               
| Content       | RenderFragment | RenderFragment | Nested Content                   | 

:::

### Example

{{sample=Components/Alerts/Alerts1}}

### Live Example

{{sample=Components/Alerts/Alerts2}}

### Links

{{sample=Components/Alerts/Alerts3}}

### Heading

{{sample=Components/Alerts/Alerts4}}

### Icons

{{sample=Components/Alerts/Alerts5}}

### Dismissing

{{sample=Components/Alerts/Alerts6}}
