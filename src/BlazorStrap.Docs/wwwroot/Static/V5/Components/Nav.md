## Nav & Tabs
#### Component \<BSNav\>
See [shared](layout/shared) for additional parameters

:::

| Parameter   | Type | Valid      | Remarks/Output        | 
|-------------|------|------------|-----------------------|
| Justify     | Enum | Justify    | `.justify-content-[]` | {.table-striped}
| IsFill      | bool | true/false | `.nav-fill`           | 
| IsNav       | bool | true/false | `<Nav>`               |
| IsPill      | bool | true/false | `.nav-pills`          |
| IsTabs      | bool | true/false | `.nav-tabs`           |
| IsVertical  | bool | true/false | `.flex-column`        |
| IsJustified | bool | true/false | `.nav-justified`      |
| NoNav       | bool | true/false | Removes `.nav`        |
| NoNavbarNav | bool | true/false | Removes `.navbar-nav` |

:::

#### Component \<BSNavItem\>
See [shared](layout/shared) for additional parameters

:::

| Parameter      | Type           | Valid          | Remarks/Output                               | 
|----------------|----------------|----------------|----------------------------------------------|
| IsActive       | bool?          | true/false     | `.active` if not set we will add it for you. | {.table-striped}
| IsDisabled     | bool           | true/false     | `.disabled`                                  |
| IsDropdown     | bool           | true/false     | `.dropdown`                                  |
| NoNavItem      | bool           | true/false     | Removes `.nav-item`                          |
| PreventDefault | bool           | true/false     | Prevents default onclick                     |
| Url            | bool           | true/false     | href = Url                                   |
| Target         | string         | string         | `data-blazorstrap` of target                 |
| TabContent     | RenderFragment | RenderFragment | Nested Content                               |
| TabLabel       | RenderFragment | RenderFragment | Nested Content                               |
| OnClick        | EventCallback  | MouseEventArgs |                                              |

:::

#### Component \<BSTabWrapper\>

#### Component \<BSTabRender\>


### Base nav

{{sample=V5/Components/Nav/Nav1}}

Use `IsNav` to output as a non list

{{sample=V5/Components/Nav/Nav2}}

### Horizontal alignment

{{sample=V5/Components/Nav/Nav3}}

{{sample=V5/Components/Nav/Nav4}}

### Vertical

{{sample=V5/Components/Nav/Nav5}}

### Tabs

{{sample=V5/Components/Nav/Nav6}}

### Tabs with content

{{sample=V5/Components/Nav/Nav7}}

### Tabs with Wrapper and Render

{{sample=V5/Components/Nav/Nav15}}

:::{.bd-callout .bd-callout-info}
**Note:** BSTabWrapper prevents BSNav from rendering the TabContent. This is the only reason it's used here.
:::

### Tab Custom Render location.

{{sample=V5/Components/Nav/Nav16}}

### Pills

{{sample=V5/Components/Nav/Nav8}}

### Fill and justify

{{sample=V5/Components/Nav/Nav9}}

{{sample=V5/Components/Nav/Nav10}}

### Working with flex

{{sample=V5/Components/Nav/Nav11}}

### Tabs with dropdowns

{{sample=V5/Components/Nav/Nav12}}

### Pills with dropdowns

{{sample=V5/Components/Nav/Nav13}}

### Submenu Example

{{sample=V5/Components/Nav/Nav14}}

### Programmatically changing active tab

{{sample=V5/Components/Nav/Nav17}}