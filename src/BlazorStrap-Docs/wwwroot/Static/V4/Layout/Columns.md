## Columns
Layout compoents can be mixed with normal HTML tags and Bootstrap classes safely. For more details about containers see https://getbootstrap.com/docs/5.1/layout/containers/
#### Component \<BSCol\>
See [shared](layout/shared) for additional parameters    
:::

| Parameter    | Type  | Valid      | Remarks/Output | 
|--------------|-------|------------|----------------|
| Auto         | bool  | true/false | .col-auto      | {.table-striped}
| Align        | Align | Enum       | .align-self-#  |
| Column       | int   | 1-12       | .col-#         |
| ColumnSmall  | int   | 1-12       | .col-sm-#      |
| ColumnMedium | int   | 1-12       | .col-md-#      |
| ColumnLarge  | int   | 1-12       | .col-lg-#      |
| ColumnXL     | int   | 1-12       | .col-xl-#      |
| OrderFirst   | bool  | true/false | .order-first   |
| OrderLast    | bool  | true/false | .order-last    |
| Order        | int   | 1-12       | .order-#       |
| OrderSmall   | int   | 1-12       | .order-sm-#    |
| OrderMedium  | int   | 1-12       | .order-md-#    |
| OrderLarge   | int   | 1-12       | .order-lg-#    |
| OrderXL      | int   | 1-12       | .order-xl-#    |
| Offset       | int   | 0-12       | .offset-#      |
| OffsetSmall  | int   | 0-12       | .offset-sm-#   |
| OffsetMedium | int   | 0-12       | .offset-md-#   |
| OffsetLarge  | int   | 0-12       | .offset-lg-#   |
| OffsetXL     | int   | 0-12       | .offset-xl-#   |

:::

### Vertical alignment

{{sample=V4/Layout/Columns/Alignment;bd-example-row bd-example-row-flex-cols}}

{{sample=V4/Layout/Columns/Alignment2;bd-example-row bd-example-row-flex-cols}}

### Horizontal alignment

{{sample=V4/Layout/Columns/Horizontal;bd-example-row}}

### Column wrapping

{{sample=V4/Layout/Columns/Wrapping;bd-example-row}}

### Column breaks

{{sample=V4/Layout/Columns/ColBreaks;bd-example-row}}

{{sample=V4/Layout/Columns/ColBreaks2;bd-example-row}}

### Reordering

#### Order Property

{{sample=V4/Layout/Columns/Order1;bd-example-row}}

{{sample=V4/Layout/Columns/Order2;bd-example-row}}

### Offsetting columns

{{sample=V4/Layout/Columns/Offsetting1;bd-example-row}}

{{sample=V4/Layout/Columns/Offsetting2;bd-example-row}}

### Margin utilities

{{sample=V4/Layout/Columns/ColMargin;bd-example-row}}

### Standalone column classes

{{sample=V4/Layout/Columns/ColStandalone;bd-example-row}}