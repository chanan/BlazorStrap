## Navbar
#### Component \<BSNavbar\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter     | Type | Valid      | Remarks/Output                   | 
|---------------|------|------------|----------------------------------|
| Color         | Enum | BSColor    | `.bg-[]`                         | { .table-striped}
| Expand        | Enum | Size       | `.navbar-expand-[]`              |
| IsDark        | bool | true/false | `.navbar-dark` / `.navbar-light` |
| IsFixedBottom | bool | true/false | `.fixed-bottom`                  |
| IsFixedTop    | bool | true/false | `.fixed-top`                     |
| IsHeader      | bool | true/false | `<Header>`                       |
| IsStickyTop   | bool | true/false | `.sticky-top`                    |

:::

### Example

{{sample=Components/Navbar/Navbar1}}

### Brand
#### Text

{{sample=Components/Navbar/Navbar2}}

#### Image

{{sample=Components/Navbar/Navbar3}}

#### Image with text

{{sample=Components/Navbar/Navbar4}}

### Color schemes
Use `Color` and `IsDark` to change your color scheme. You can also pass in custom classes using `Class`

{{sample=Components/Navbar/Navbar5}}

### Placement

{{sample=Components/Navbar/Navbar6}}

{{sample=Components/Navbar/Navbar7}}

{{sample=Components/Navbar/Navbar8}}

### OffCanvas

{{sample=Components/Navbar/Navbar9}}