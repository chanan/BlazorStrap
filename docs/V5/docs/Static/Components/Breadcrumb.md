## Breadcrumb
#### Component \<BSBreadcrumb\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter | Type   | Valid  | Remarks/Output | 
|-----------|--------|--------|----------------|
| Divider   | string | string |                | {.table-striped}  

:::

{.mt-4}
#### Component \<BSBreadcrumbItem\>
:::

| Parameter | Type   | Valid  | Remarks/Output | 
|-----------|--------|--------|----------------|
| IsActive  | string | string | `.active`      | {.table-striped}  
| Url       | string | string | href=Url       |

:::

### Example

{{sample=Components/Breadcrumb/Breadcrumb1}}

### Dividers

{{sample=Components/Breadcrumb/Breadcrumb2}}


It’s also possible to use an **embedded** SVG icon. Apply it via Bootstraps CSS custom property, or use the Sass variable.

{{sample=Components/Breadcrumb/Breadcrumb3}}

You can remove the divider by passing in a empty string to the `Divider` parameter

{{sample=Components/Breadcrumb/Breadcrumb4}}
