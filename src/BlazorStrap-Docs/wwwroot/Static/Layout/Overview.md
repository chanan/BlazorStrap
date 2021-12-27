## Page Heading

See [shared](layout/shared) for parameters    

The following is a list of general layout Components and helpers provided.
  
Our layout classes are intended helper components to write there html counterpart. Without the need to remember classes. The is nothing preventing you from writing their html couterpart. 
If you ever need a class we did not provide in your element you can insert in with the `Class` parameter. We provide all bootstrap class as static string use `BS.`

* `<BSDiv>` is equivalent to `<div>` with our shared parameters
* `<BSContainer>` is equivalent to `<div class="container">` 
* `<BSRow>` is equivalent to `<div class="row">`
* `<BSCol>` is equivalent to `<div class="col">`
* `<BSColBreak>`  is equivalent to `<div class="w-100">`
* `<BSLink>` is equivalent to `<a class="" href=""></a>`

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