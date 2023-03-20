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
| NoNavbarClass | bool | true       | Removes `.navbar`                |

:::

### Example

{{sample=V5/Components/Navbar/Navbar1}}

### Example Nav popper

{{sample=V5/Components/Navbar/Navbar10}}

### Brand
#### Text

{{sample=V5/Components/Navbar/Navbar2}}

#### Image

{{sample=V5/Components/Navbar/Navbar3}}

#### Image with text

{{sample=V5/Components/Navbar/Navbar4}}

### Color schemes
Use `Color` and `IsDark` to change your color scheme. You can also pass in custom classes using `Class`

{{sample=V5/Components/Navbar/Navbar5}}

### Placement

{{sample=V5/Components/Navbar/Navbar6}}

{{sample=V5/Components/Navbar/Navbar7}}

{{sample=V5/Components/Navbar/Navbar8}}

### OffCanvas

{{sample=V5/Components/Navbar/Navbar9}}