## Breadcrumb
#### Component \<BSBreadcrumb\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter | Type                       | Valid                      | Remarks/Output                                     | 
|-----------|----------------------------|----------------------------|----------------------------------------------------|
| Divider   | string                     | string                     |                                                    | {.table-striped}  
| BasePath  | string                     | string                     | Turns on auto generate using supplied base path    |
| Labels    | Dictionary<string, string> | Dictionary<string, string> | Custom Labels for your paths see example           |
| MaxItems  | int                        | int                        | Maximum number of items to show in the breadcrumb. |

:::

{.mt-4}
#### Component \<BSBreadcrumbItem\>
:::

| Parameter | Type   | Valid  | Remarks/Output | 
|-----------|--------|--------|----------------|
| IsActive  | string | string | `.active`      | {.table-striped}  
| Url       | string | string | href=Url       |

:::

BlazorStrap can handle creating your breadcrumbs for you. 
Simply supply a `BasePath`. This lets the component know to generate the breadcrumbs for you and where to start
By default we automatically capitalize the first letter of each word. You can supply alternative labels by setting the Labels parameter.
All paths are case sensitive your dictionary keys should be set to match the case as well.

### Automatic

{{sample=V5/Components/Breadcrumb/Breadcrumb0}}

Or you can do it yourself, using our components.

### Example

{{sample=V5/Components/Breadcrumb/Breadcrumb1}}

### Dividers

{{sample=V5/Components/Breadcrumb/Breadcrumb2}}


It’s also possible to use an **embedded** SVG icon. Apply it via Bootstraps CSS custom property, or use the Sass variable.

{{sample=V5/Components/Breadcrumb/Breadcrumb3}}

You can remove the divider by passing in a empty string to the `Divider` parameter

{{sample=V5/Components/Breadcrumb/Breadcrumb4}}
