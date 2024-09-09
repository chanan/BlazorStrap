## Wizard
#### Component \<BSWizard\>
[BlazorStrap.Extensions.Treeview](https://www.nuget.org/packages/BlazorStrap.Extensions.TreeView/)    

:::

| Parameter			  | Type						| Valid          | Remarks/Output                  | 
|---------------------|-----------------------------|----------------|---------------------------------|
| IsExpanded    	  | bool						| true/false     | First Node is shown             | {.table-striped}  
| IsMultiSelect		  | bool						| true/false     | More then one item is selectable|
| IsDoubleClickToOpen | bool						| true/false     | Double click to open            |
| ActiveItemAdded     | EventCallback<BSTreeItem>   | func           |                                 |
| ActiveItemAdded     | EventCallback<BSTreeItem>   | func           |                                 |

::: 

#### Component \<BSWizardItem\>
:::

| Parameter    | Type           | Valid          | Remarks/Output                  | 
|--------------|----------------|----------------|---------------------------------|
| Id           | string         | string         | your given id for the node item | {.table-striped}  
| IsActive     | bool           | true/false     | `.active`                       |
| IsOpen       | bool           | true/false     | `.show`                         |
| TextLabel    | string?        | string         | Text Only Label                 |
| Class        | string?        | string         | Custom classes                  | 
| Action       | RenderFragment?| RenderFragment | Nested Content                  |
| ChildContent | RenderFragment?| RenderFragment | Nested Content                  |
| Label        | RenderFragment?| RenderFragment | Nested Content                  |

::: 

##### Example

{{sample=V5/Extensions/Wizard}}
