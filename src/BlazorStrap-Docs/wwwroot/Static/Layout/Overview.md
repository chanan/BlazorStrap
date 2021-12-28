## Overview

See [shared](layout/shared) for parameters    

The following is a list of general layout Components and helpers provided.
  
Our layout components are intended helpers to write their html counterpart. Without the need to remember classes. The is nothing preventing you from writing their html counterpart. 
If you ever need a class we did not provide in your element you can insert in with the `Class` parameter. We provide all bootstrap class as static string use `BS.` class

* `<BSContainer>` is equivalent to `<div class="container">` 
* `<BSRow>` is equivalent to `<div class="row">`
* `<BSCol>` is equivalent to `<div class="col">`
* `<BSColBreak>`  is equivalent to `<div class="w-100">`
* `<BSLink>` is equivalent to `<a class="" href=""></a>`
* `<BSTable>` is equivalent to `<table class="table">` but with our formatting parameters
* `<BSTHead>` is equivalent to `<thead>` but with our formatting parameters
* `<BSTBody>` is equivalent to `<tbody>` but with our formatting parameters
* `<BSTR>` is equivalent to `<TR>` but with our formatting parameters
* `<BSTD>` is equivalent to `<TD>` but with our formatting parameters
* `<BSTFoot>` is equivalent to `<tfoot>` but with our formatting parameters
* `<BSCaption>` is equivalent to `<caption>` but with our formatting parameters
* `<BSCard>` is equivalent to <div class="card [foo]">` but with our formatting parameters

----

#### Component \<BSLink\>
:::

| Parameter  | Type          | Valid            | Remarks/Output | 
|------------|---------------|------------------|----------------|
| IsActive   | bool          | true/false       | `.active`      | {.table-striped}
| IsDisabled | bool          | true/false       | `.disabled`    |
| OnClick    | EventCallback | MouseEventArgs   |                |
| Target     | string        | data-blazorstrap |                |
| Url        | string        | string           | `href=Url`     |

:::