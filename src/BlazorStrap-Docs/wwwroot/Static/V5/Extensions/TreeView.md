## TreeView
#### Component \<BSTree\>
[BlazorStrap.Extensions.Treeview](https://www.nuget.org/packages/BlazorStrap.Extensions.TreeView/)    

:::

| Parameter			  | Type						| Valid          | Remarks/Output                  | 
|---------------------|-----------------------------|----------------|---------------------------------|
| IsExpanded    	  | bool						| true/false     | First Node is shown             | {.table-striped}  
| IsMultiSelect		  | bool						| true/false     | More then one item is selectable|
| IsDoubleClickToOpen | bool						| true/false     | Double click to open            |
| OnSelect			  | EventCallback<BSTreeItem>   | func           |                                 |
| OnUnselect	      | EventCallback<BSTreeItem>   | func           |                                 |
| ActiveItemAdded     | EventCallback<BSTreeItem>   | func           | Obsolete use OnSelect           |
| ActiveItemRemoved   | EventCallback<BSTreeItem>   | func           | Obsolete use OnUnselect         |

::: 

#### Component \<BSTreeItem\>
:::

| Parameter			| Type           | Valid          | Remarks/Output							| 
|-------------------|----------------|----------------|---------------------------------		|
| Id				| string         | string         | your given id for the node item			| {.table-striped}  
| IsDefaultActive   | bool           | true/false     | Sets active once						|
| IsAlwaysActive    | bool           | true/false     | Keeps item active						|
| IsOpen			| bool           | true/false     | `.show`									|
| TextLabel			| string?        | string         | Text Only Label							|
| Class				| string?        | string         | Custom classes							| 
| OnClick			| EventCallback  | func           | MouseEventArgs							|
| OnDblClick		| EventCallback  | func           | MouseEventArgs							|
| Action			| RenderFragment?| RenderFragment | Nested Content							|
| ChildContent		| RenderFragment?| RenderFragment | Nested Content							|
| Label				| RenderFragment?| RenderFragment | Nested Content							|

::: 

##### Example

{{sample=V5/Extensions/TreeView}}

##### Dynamic Example with Multi-Select

{{sample=V5/Extensions/TreeView2}}

#### Methods \<BSTree\>
:::

| Method					| Return Type	| Parameters    | Remarks/Output				| 
|---------------------------|---------------|---------------|-------------------------------|
| SelectAsync				| Task			| string		| Selects item by id			| {.table-striped}  
| SelectAsync				| Task			| BSTreeItem    | Selects item by BSTreeItem	|
| UnselectAsync				| Task          | string		| Unselects item by id			|
| UnselectAsync				| Task          | BSTreeItem    | Unselects item by BSTreeItem	|
| ClearSelectionAsync    	| Task          | BSTreeItem    | Clears Selection				|


::: 
