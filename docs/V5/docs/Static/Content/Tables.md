## Tables
#### Component \<BSTable\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type | Valid      | Remarks/Output      | 
|--------------|------|------------|---------------------|
| Color        | Enum | BSColor    | `.table-[]`         | {.table-striped .p-2}  
| IsBordered   | bool | true/false | `.table-bordered`   |
| IsBorderLess | bool | true/false | `.table-borderless` |
| IsCaptionTop | bool | true/false | `.caption-top`      |
| IsStriped    | bool | true/false | `.table-striped`    |

:::
#### Component \<BSTR\>
:::

| Parameter | Type | Valid      | Remarks/Output  | 
|-----------|------|------------|-----------------|
| AlignRow  | Enum | AlignRow   | `.align-[]`     | {.table-striped .p-2}  
| Color     | Enum | BSColor    | `.table-[]]`    |
| IsActive  | bool | true/false | `.table-active` |

:::
#### Component \<BSTD\>
:::

| Parameter | Type    | Valid      | Remarks/Output  | 
|-----------|---------|------------|-----------------|
| AlignRow  | Enum    | AlignRow   | `.align-[]`     | {.table-striped .p-2}  
| Color     | Enum    | BSColor    | `.table-[]]`    |
| IsActive  | bool    | true/false | `.table-active` |
| ColSpan   | string? | 0-x        | ColSpan         |

:::

#### Component \<BSTBody\> \<BSTFoot\> \<BSTHead\>
Shared Parameters only

### Example with parameter tester

{{sample=Content/Tables/Tables1}}