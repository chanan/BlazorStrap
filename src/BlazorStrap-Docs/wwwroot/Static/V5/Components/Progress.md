## Progress

#### Component \<BSProgress\>
See [shared](layout/shared) for additional parameters    

Include one or more `BSProgressBar` inside.

When using multiple bars, their total percentage should not exceed 100%.

#### Component \<BSProgressBar\>
:::

| Parameter  | Type   | Valid      | Remarks/Output           | 
|------------|--------|------------|--------------------------|
| Color      | Enum   | BSColor    | `.bg-[]`                 | {.table-striped .p-2}
| IsStriped  | bool   | true/false | `.progress-bar-striped`  |
| IsAnimated | bool   | true/false | `.progress-bar-animated` |
| Value      | double | min..max   | current value            |
| Min        | double |            | min value (default 0)    |
| Max        | double |            | max value (default 100)  |

:::

### Example
{{sample=V5/Components/Progress/Progress1}}

### Dynamic
{{sample=V5/Components/Progress/Progress2}}
